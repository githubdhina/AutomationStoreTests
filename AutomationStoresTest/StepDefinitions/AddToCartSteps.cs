using AutomationStoresTest.Helper;
using AutomationStoresTest.PageObjects;
using AutomationStoresTest.TestData;
using System.Data;

namespace AutomationStoresTest.StepDefinitions
{
    [Binding]
    public sealed class AddToCartSteps
    {
        //Page Object for Calculator
        private readonly StoreHomePage _storeHomePage;
        private readonly StoreCartPage _storeCartPage;
        private readonly ExcelHelper _excelHelper;
        private readonly SeleniumHelper _seleniumHelper;
        private readonly SingleProductDetails _singleProductDetails;

        public AddToCartSteps(BrowserDriver browserDriver)
        {
            _storeHomePage = new StoreHomePage(browserDriver.Current);
            _storeCartPage = new StoreCartPage(browserDriver.Current);
            _seleniumHelper = new SeleniumHelper(browserDriver.Current);
            _singleProductDetails = new SingleProductDetails();
            _excelHelper = new ExcelHelper();
 
        }


        [When(@"the user adds the (.*) with (.*) with (.*) to cart")]
        public void WhenTheUserAddsNewLadiesHighWedgeHeelToeThongDiamanteFlipFlopSandalsWithToCart(int prodNo, string productName, int qty)
        {
            string[] productUnitAndTotalPrice = _storeHomePage.addProductToCartAndGetProductPrice(productName, qty);
            _singleProductDetails.ProductUnitPrice = productUnitAndTotalPrice[0];
            _singleProductDetails.ProductTotalPrice= productUnitAndTotalPrice[1];
            _singleProductDetails.ProductQty = qty;
            _singleProductDetails.ProductName = productName;
        }


        [Then(@"the (.*) should be added to the cart with (.*) and (.*) and (.*)")]
        public void ThenTheNewLadiesHighWedgeHeelToeThongDiamanteFlipFlopSandalsShouldBeAddedToTheCart(string productName, int qty, string expectedTotalPrice, string expectedUnitPrice)
        {
            // Validate the product details before adding to cart and after adding to cart matches using Context Injection
            SingleProductDetails actualSingleProductDetails = _storeCartPage.getSingleProdctDetailsInCartByName(productName);
            Assert.AreEqual(_singleProductDetails.ProductName, actualSingleProductDetails.ProductName);
            Assert.AreEqual(_singleProductDetails.ProductQty, actualSingleProductDetails.ProductQty);
            Assert.AreEqual(_singleProductDetails.ProductUnitPrice, actualSingleProductDetails.ProductUnitPrice);

            //Validate product details match the expected values in scenarior
            Assert.AreEqual(productName, actualSingleProductDetails.ProductName);
            Assert.AreEqual(qty, actualSingleProductDetails.ProductQty);
            Assert.AreEqual(expectedUnitPrice, actualSingleProductDetails.ProductUnitPrice);
            Assert.AreEqual(expectedTotalPrice, actualSingleProductDetails.ProductTotalPrice);
        }

        [When(@"the user deletes the (.*) from cart")]
        public void WhenTheUserDeletesTheWomensHighHeelPointToeStilettoSandalsAnkleStrapCourtShoesFromCart(string productName)
        {
            _storeCartPage.deleteProductByName(productName);
        }

        [Then(@"the (.*) should be deleted")]
        public void ThenTheFiorellaPurplePeepToesShouldBeDeleted(string productName)
        {
            Assert.IsTrue(_storeCartPage.getCartEmptyElementText().Contains("Your shopping cart is empty!"));
        }


    }
}
