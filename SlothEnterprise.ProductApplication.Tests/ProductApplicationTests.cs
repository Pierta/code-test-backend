using NSubstitute;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Exceptions;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.SubmitApplicationStrategies;
using System.Collections.Generic;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        [Fact]
        public void UnsupportedProductTypeException_ShouldBeThrowed_WhenThereAreNoSpecificStrategy()
        {
            // Arrange           
            var submitApplicationStrategyFactory = new SubmitApplicationStrategyFactory(null, null, null);
            var productApplicationService = new ProductApplicationService(submitApplicationStrategyFactory);
            var application = new SellerApplication()
            {
                Product = new UnexistingProduct()
            };

            // Act & Assert
            Assert.Throws<UnsupportedProductTypeException>(() =>
                productApplicationService.SubmitApplicationFor(application)
            );
        }

        [Theory]
        [InlineData(true, 1, 1)]
        [InlineData(true, null, -1)]
        [InlineData(false, 1, -1)]
        public void ConfidentalInvoiceDiscountStrategy_ShouldReturnExtraResult_DependsOnApplicationResultStatus(bool resultStatus, int? resultAppId, int expectedResult)
        {
            // Arrange
            var confidentialInvoiceService = Substitute.For<IConfidentialInvoiceService>();
            confidentialInvoiceService.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<decimal>())
                .Returns(new TestApplicationResult { Success = resultStatus, ApplicationId = resultAppId });
            var confidentialInvoiceDiscountStrategy = new ConfidentialInvoiceDiscountStrategy(confidentialInvoiceService);
            var application = new SellerApplication
            {
                CompanyData = new SellerCompanyData(),
                Product = new ConfidentialInvoiceDiscount()
            };

            // Act
            var actualResult = confidentialInvoiceDiscountStrategy.Execute(application);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(true, 1, 1)]
        [InlineData(true, null, -1)]
        [InlineData(false, 1, -1)]
        public void BusineesLoansStrategy_ShouldReturnExtraResult_DependsOnApplicationResultStatus(bool resultStatus, int? resultAppId, int expectedResult)
        {
            // Arrange
            var businessLoansService = Substitute.For<IBusinessLoansService>();
            businessLoansService.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>())
                .Returns(new TestApplicationResult { Success = resultStatus, ApplicationId = resultAppId });
            var businessLoansStrategy = new BusinessLoansStrategy(businessLoansService);
            var application = new SellerApplication
            {
                CompanyData = new SellerCompanyData(),
                Product = new BusinessLoans()
            };

            // Act
            var actualResult = businessLoansStrategy.Execute(application);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        public class UnexistingProduct : IProduct
        {
            public int Id { get; set; }
        }

        public class TestApplicationResult : IApplicationResult
        {
            public int? ApplicationId { get; set; }

            public bool Success { get; set; }

            public IList<string> Errors { get; set; }
        }
    }
}