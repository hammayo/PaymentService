using Microsoft.Extensions.Logging;
using ClearBank.DemoFramework.Types;

namespace ClearBank.DemoFramework.Services
{
    public sealed class PaymentService : IPaymentService
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IAccountService accountService, ILogger<PaymentService> logger)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public MakePaymentResult MakePayment(MakePaymentRequest? request)
        {
            if (request is null)
            {
                _logger.LogWarning("Payment request was null");
                return new MakePaymentResult { Success = false };
            }

            var account = _accountService.GetAccount(request.DebtorAccountNumber);
            if (account is null)
            {
                _logger.LogWarning("Account not found for account number: {AccountNumber}", request.DebtorAccountNumber);
                return new MakePaymentResult { Success = false };
            }

            var result = ProcessPayment(request, account);

            if (result.Success)
            {
                _accountService.UpdateAccount(account, request);
                _logger.LogInformation(
                    "Payment processed successfully. Scheme: {Scheme}, Amount: {Amount}, Account: {Account}", 
                    request.PaymentScheme, 
                    request.Amount, 
                    request.DebtorAccountNumber);
            }

            return result;
        }

        private static MakePaymentResult ProcessPayment(MakePaymentRequest request, Account account) =>
            request.PaymentScheme switch
            {
                PaymentScheme.Bacs => ProcessBacsPayment(account),
                PaymentScheme.Chaps => ProcessChapsPayment(account),
                PaymentScheme.FasterPayments => ProcessFasterPayment(request, account),
                _ => throw new ArgumentException($"Invalid payment scheme: {request.PaymentScheme}", nameof(request))
            };

        private static MakePaymentResult ProcessBacsPayment(Account account) =>
            new() { Success = account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs) };

        private static MakePaymentResult ProcessChapsPayment(Account account) =>
            new()
            {
                Success = account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
                         account.Status == AccountStatus.Live
            };

        private static MakePaymentResult ProcessFasterPayment(MakePaymentRequest request, Account account) =>
            new()
            {
                Success = account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) &&
                         account.Balance >= request.Amount
            };
    }
}
