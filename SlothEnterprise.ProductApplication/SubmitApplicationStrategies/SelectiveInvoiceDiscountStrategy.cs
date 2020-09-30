using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.SubmitApplicationStrategies
{
    public class SelectiveInvoiceDiscountStrategy : ISubmitApplicationStrategy
    {
        private readonly ISelectInvoiceService _selectInvoiceService;

        public SelectiveInvoiceDiscountStrategy(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }

        public int Execute(ISellerApplication application)
        {
            var product = application.Product as SelectiveInvoiceDiscount;
            
            return _selectInvoiceService.SubmitApplicationFor(application.CompanyData.Number.ToString(), product.InvoiceAmount, product.AdvancePercentage);
        }
    }
}