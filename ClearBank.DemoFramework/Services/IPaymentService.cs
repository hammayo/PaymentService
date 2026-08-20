using ClearBank.DemoFramework.Types;

namespace ClearBank.DemoFramework.Services;

public interface IPaymentService
{
    MakePaymentResult MakePayment(MakePaymentRequest? request);
}
