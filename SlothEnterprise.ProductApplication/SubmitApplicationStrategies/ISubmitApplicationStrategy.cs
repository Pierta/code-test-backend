using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.SubmitApplicationStrategies
{
    public interface ISubmitApplicationStrategy
    {
        int Execute(ISellerApplication application);
    }
}