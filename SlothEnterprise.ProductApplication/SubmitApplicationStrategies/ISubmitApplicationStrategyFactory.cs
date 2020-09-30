using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.SubmitApplicationStrategies
{
    public interface ISubmitApplicationStrategyFactory
    {
        void SetStrategy(IProduct product);

        int ExecuteStrategy(ISellerApplication application);
    }
}