using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.SubmitApplicationStrategies;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService: IProductApplicationService
    {
        private readonly ISubmitApplicationStrategyFactory _submitApplicationStrategyFactory;

        public ProductApplicationService(ISubmitApplicationStrategyFactory submitApplicationStrategyFactory)
        {
            _submitApplicationStrategyFactory = submitApplicationStrategyFactory;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            _submitApplicationStrategyFactory.SetStrategy(application.Product);
            var result = _submitApplicationStrategyFactory.ExecuteStrategy(application);

            return result;
        }
    }
}