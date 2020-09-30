using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Exceptions;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.SubmitApplicationStrategies
{
    public class SubmitApplicationStrategyFactory : ISubmitApplicationStrategyFactory
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly IBusinessLoansService _businessLoansService;

        private ISubmitApplicationStrategy strategy;

        public SubmitApplicationStrategyFactory(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _businessLoansService = businessLoansService;
        }

        public void SetStrategy(IProduct product)
        {
            if (product is SelectiveInvoiceDiscount)
            {
                strategy = new SelectiveInvoiceDiscountStrategy(_selectInvoiceService);
            }
            else
            if (product is ConfidentialInvoiceDiscount)
            {
                strategy = new ConfidentialInvoiceDiscountStrategy(_confidentialInvoiceWebService);
            }
            else
            if (product is BusinessLoans)
            {
                strategy = new BusinessLoansStrategy(_businessLoansService);
            }
            else
            {
                throw new UnsupportedProductTypeException();
            }
        }

        public int ExecuteStrategy(ISellerApplication application)
        {
            return strategy.Execute(application);
        }
    }
}