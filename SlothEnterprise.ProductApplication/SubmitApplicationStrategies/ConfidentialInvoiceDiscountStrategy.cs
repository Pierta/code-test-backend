using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.SubmitApplicationStrategies
{
    public class ConfidentialInvoiceDiscountStrategy : ISubmitApplicationStrategy
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public ConfidentialInvoiceDiscountStrategy(IConfidentialInvoiceService confidentialInvoiceWebService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
        }

        public int Execute(ISellerApplication application)
        {
            var product = application.Product as ConfidentialInvoiceDiscount;

            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                    new CompanyDataRequest
                    {
                        CompanyFounded = application.CompanyData.Founded,
                        CompanyNumber = application.CompanyData.Number,
                        CompanyName = application.CompanyData.Name,
                        DirectorName = application.CompanyData.DirectorName
                    }, product.TotalLedgerNetworth, product.AdvancePercentage, product.VatRate);

            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }
}