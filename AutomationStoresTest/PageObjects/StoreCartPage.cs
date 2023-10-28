using OpenQA.Selenium;
using AutomationStoresTest.TestData;

namespace AutomationStoresTest.PageObjects
{
    public class StoreCartPage
    {


        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        public StoreCartPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement Txt_Subtotal => _webDriver.FindElement(By.XPath("//table[@id='totals_table']/tbody/tr/td[2]/span"));
        private IWebElement Txt_TotalIncludingShipping => _webDriver.FindElement(By.XPath("//span[@class='bold totalamout']"));

        private IList<IWebElement> Txt_AllProductNamesInCart => _webDriver.FindElements(By.XPath("//*[@id='cart']/div/div[1]/table/tbody/tr/td[2]/a"));

        private IList<IWebElement> Txt_AllProductUnitPriceInCart => _webDriver.FindElements(By.XPath("//*[@id='cart']/div/div[1]/table/tbody/tr/td[4]"));

        private IList<IWebElement> Txt_AllProductQtyInCart => _webDriver.FindElements(By.XPath("//*[@id[contains(.,'cart_quantity')]]"));

        private IList<IWebElement> Txt_AllProductTotalPriceInCart => _webDriver.FindElements(By.XPath("//*[@id='cart']/div/div[1]/table/tbody/tr/td[6]"));

        private IWebElement Txt_CartEmptyElement => _webDriver.FindElement(By.XPath("//div[@class='contentpanel']"));
        

        public MultipleProductDetails getAllProdctDetailsInCart()
        {
            MultipleProductDetails multipleProductDetails = new MultipleProductDetails();
            multipleProductDetails.MultiProductDetails = new Dictionary<int, SingleProductDetails>();
            for (int i = 0; i < Txt_AllProductNamesInCart.Count; i++)
                if (Txt_AllProductNamesInCart.Count == 0)
                {
                    Assert.Fail("No Products Found in Cart");
                }
                else
                {
                    {
                        SingleProductDetails singleProductDetails = new SingleProductDetails();
                        singleProductDetails.ProductName = Txt_AllProductNamesInCart[i].Text;
                        singleProductDetails.ProductUnitPrice = Txt_AllProductUnitPriceInCart[i].Text;
                        singleProductDetails.ProductQty = Convert.ToInt32(Txt_AllProductQtyInCart[i].GetAttribute("value"));
                        singleProductDetails.ProductTotalPrice = Txt_AllProductTotalPriceInCart[i].Text;
                        multipleProductDetails.MultiProductDetails.Add(i, singleProductDetails);
                    }

                }

            return multipleProductDetails;
        }

        public SingleProductDetails getSingleProdctDetailsInCartByName(string productName)
        {

            SingleProductDetails singleProductDetails = new SingleProductDetails();
            IWebElement productNameElement = _webDriver.FindElement(By.XPath($"//form[@id='cart']//a[text()='{productName}']"));
            IWebElement productUnitPriceElement = _webDriver.FindElement(By.XPath($"//form[@id='cart']//a[text()='{productName}']/following::td[2]"));
            IWebElement productQtyElement= _webDriver.FindElement(By.XPath($"//form[@id='cart']//a[text()='{productName}']/following::td[3]/div/input"));
            IWebElement productTotalPriceElement= _webDriver.FindElement(By.XPath($"//form[@id='cart']//a[text()='{productName}']/following::td[4]"));
            singleProductDetails.ProductName = productNameElement.Text;
            singleProductDetails.ProductUnitPrice = productUnitPriceElement.Text;
            singleProductDetails.ProductQty = Convert.ToInt32(productQtyElement.GetAttribute("value"));
            singleProductDetails.ProductTotalPrice = productTotalPriceElement.Text;
            return singleProductDetails;
        }

        public void deleteProductByName(string productName)
        {
            IWebElement productDeleteElement = _webDriver.FindElement(By.XPath($"//form[@id='cart']//a[text()='{productName}']/following::td[5]/a"));
            productDeleteElement.Click();
        }

        public string getCartEmptyElementText()
        {
            return Txt_CartEmptyElement.Text;
        }

    }

}
