using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Exceptions;
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
            try
            {
                _submitApplicationStrategyFactory.SetStrategy(application.Product);
            }
            catch(UnsupportedProductTypeException exception)
            {
                //// TODO: add logging here
                throw exception;
            }

            var result = _submitApplicationStrategyFactory.ExecuteStrategy(application);

            return result;
        }
    }
}