using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.SubmitApplicationStrategies
{
    public class BusinessLoansStrategy : ISubmitApplicationStrategy
    {
        private readonly IBusinessLoansService _businessLoansService;

        public BusinessLoansStrategy(IBusinessLoansService businessLoansService)
        {
            _businessLoansService = businessLoansService;
        }

        public int Execute(ISellerApplication application)
        {
            var product = application.Product as BusinessLoans;

            var result = _businessLoansService.SubmitApplicationFor(new CompanyDataRequest
            {
                CompanyFounded = application.CompanyData.Founded,
                CompanyNumber = application.CompanyData.Number,
                CompanyName = application.CompanyData.Name,
                DirectorName = application.CompanyData.DirectorName
            }, new LoansRequest
            {
                InterestRatePerAnnum = product.InterestRatePerAnnum,
                LoanAmount = product.LoanAmount
            });

            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }
}