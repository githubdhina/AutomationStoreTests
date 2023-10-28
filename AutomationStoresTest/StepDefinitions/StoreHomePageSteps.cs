using AutomationStoresTest.Helper;
using AutomationStoresTest.PageObjects;
using AutomationStoresTest.TestData;
using System.Data;

namespace AutomationStoresTest.StepDefinitions
{
    [Binding]
    public sealed class StoreHomePageSteps
    {
        //Page Object for Calculator
        private readonly StoreHomePage _storeHomePage;
        private readonly StoreCartPage _storeCartPage;
        private readonly ExcelHelper _excelHelper;
        private readonly SeleniumHelper _seleniumHelper;
        string expectedProductsCategoryWiseDataFile = FileSystemHelper.TestDataBaseFolder + "Exepected_Products_CategoryWise.xlsx";
        public StoreHomePageSteps(BrowserDriver browserDriver)
        {
            _storeHomePage = new StoreHomePage(browserDriver.Current);
            _storeCartPage = new StoreCartPage(browserDriver.Current);
            _seleniumHelper = new SeleniumHelper(browserDriver.Current);
            _excelHelper = new ExcelHelper();
        }

        [Given(@"the user navigates to automation stores home page")]
        public void GivenTheUserNavigatesToAutomationStoresHomePage()
        {
            _storeHomePage.gotoAutomationStoreBaseUrl();

        }

        [When(@"the user selects (.*) from (.*) or (.*)")]
        public void WhenITheUserSelectsShoesFromApparelAccessories(string subCategory, string mainCategory, string saleItem)
        {
            if (saleItem.Equals("No"))
            {
                _storeHomePage.filterProducts(subCategory, mainCategory);
            }
            else
            {
                _storeHomePage.goToSaleItemsPage();
            }
        }

        [Then(@"the correct number of products under (.*) is listed")]
        public void ThenTheCorrectNumberOfProductsUnderShoesIsListed(string subCategory)
        {
            DataTable expectedProductListTable = _excelHelper.ConvertExcelToDataTable(expectedProductsCategoryWiseDataFile, subCategory);
            Assert.AreEqual(expectedProductListTable.Rows.Count, _storeHomePage.getTotalNumberOfProductsInCategory());
            MultipleProductDetails multipleProductDetails = _storeHomePage.getProductsInFilteredCategory();
            for (int i=0; i< expectedProductListTable.Rows.Count; i++)
            {
                SingleProductDetails singleProductDetails = multipleProductDetails.MultiProductDetails[i];
                Assert.AreEqual((expectedProductListTable.Rows[i]["ProductName"].ToString()).ToUpper(), singleProductDetails.ProductName);
            }
        }

        [When(@"the user selects (.*) option")]
        public void WhenITheUserSelectsCurrency(string currency)
        {
            _storeHomePage.clickCurrencyOption(currency);
        }


        [Then(@"the correct number of products under (.*) is listed and price is displayed in selected (.*)")]
        public void ThenTheCorrectNumberOfProductsUnderShoesIsListedAndPriceIsDisplayedInSelectedCurrency(string subCategory, string currency)
        {
            DataTable expectedProductListTable = _excelHelper.ConvertExcelToDataTable(expectedProductsCategoryWiseDataFile, subCategory);
            Assert.AreEqual(expectedProductListTable.Rows.Count, _storeHomePage.getTotalNumberOfProductsInCategory());
            MultipleProductDetails multipleProductDetails = _storeHomePage.getProductsInFilteredCategory();
            for (int i = 0; i < expectedProductListTable.Rows.Count; i++)
            {
                SingleProductDetails singleProductDetails = multipleProductDetails.MultiProductDetails[i];
                Assert.AreEqual((expectedProductListTable.Rows[i]["ProductName"].ToString()).ToUpper(), singleProductDetails.ProductName);
                Assert.AreEqual((expectedProductListTable.Rows[i][currency].ToString()).ToUpper(), singleProductDetails.ProductUnitPrice);

            }
        }



    }
}
