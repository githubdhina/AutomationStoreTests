using OpenQA.Selenium;
using AutomationStoresTest.TestData;
using AutomationStoresTest.Helper;

namespace AutomationStoresTest.PageObjects
{
    public class StoreHomePage
    {

        private const string AutomationStoreBaseUrl = "https://automationteststore.com/";
        private readonly SeleniumHelper _seleniumHelper;
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        public StoreHomePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _seleniumHelper = new SeleniumHelper(webDriver);
        }

        //Finding elements by ID
        private IWebElement Btn_Home => _webDriver.FindElement(By.XPath("//*[@id='categorymenu']/nav/ul/li[1]/a"));
        private IWebElement Btn_HomeSpecials => _webDriver.FindElement(By.XPath("//*[@id='main_menu_top']/li[1]/a/span"));
        private IWebElement Btn_HomeAccount => _webDriver.FindElement(By.XPath("//*[@id='main_menu']/li[2]/a/span"));
        private IWebElement Btn_HomeCart => _webDriver.FindElement(By.XPath("//*[@id='main_menu']/li[3]/a/span"));
        private IWebElement Btn_HomeCheckOut => _webDriver.FindElement(By.XPath("//*[@id='main_menu']/li[4]/a/span"));
        private IList<IWebElement> Txt_AllProductNameElements => _webDriver.FindElements(By.XPath("//div[@class='fixed_wrapper']//a[@class='prdocutname']"));
        private IList<IWebElement> Txt_AllProductPriceElements => _webDriver.FindElements(By.XPath("//div[@class='fixed_wrapper']//a[@class='prdocutname']/following::div[@class='oneprice']"));

        private IWebElement Input_ProductQty => _webDriver.FindElement(By.Id("product_quantity"));

        private IWebElement Txt_ProductTotalPrice => _webDriver.FindElement(By.XPath("//span[@class='total-price']"));

        private IWebElement Btn_AddProductToCartFinal => _webDriver.FindElement(By.XPath("//a[@onclick[contains(.,'submit')]]"));

        private IWebElement Txt_NumberOfProductsInCategory => _webDriver.FindElement(By.XPath("//form[@class='form-inline pull-left']"));

        private IWebElement Btn_CurrencyOption => _webDriver.FindElement(By.XPath("//a[@class='dropdown-toggle'][1]"));

        private IWebElement Txt_CurrentlyFilteredProduts=> _webDriver.FindElement(By.XPath("//ul[@class='breadcrumb']/li/a"));

        private IWebElement Btn_ConfirmAddToCart => _webDriver.FindElement(By.XPath("//*[@id='main_menu']/li[3]/a/span"));
        public void gotoAutomationStoreBaseUrl()
        {
            if (_webDriver.Url != AutomationStoreBaseUrl)
            {
                _webDriver.Url = AutomationStoreBaseUrl;

            }

        }
        public void filterProducts(string subCategory, string mainCategory)
        {
            IWebElement mainCatetoryElement = _webDriver.FindElement(By.XPath($"//section[@id='categorymenu']//a[contains(text(), '{mainCategory}')]"));
            _seleniumHelper.MoveToElement(mainCatetoryElement);
            IWebElement subCatetoryElement = _webDriver.FindElement(By.XPath($"//div[@class='subcategories']//a[contains(text(), '{subCategory}')]"));
            subCatetoryElement.Click();
        }


        public MultipleProductDetails getProductsInFilteredCategory()
        {
            MultipleProductDetails multipleProductDetails = new MultipleProductDetails();
            multipleProductDetails.MultiProductDetails = new Dictionary<int, SingleProductDetails>();
            for (int i = 0; i < Txt_AllProductNameElements.Count; i++)
            {
                SingleProductDetails singleProductDetails = new SingleProductDetails();
                singleProductDetails.ProductName = Txt_AllProductNameElements[i].Text;
                singleProductDetails.ProductUnitPrice = Txt_AllProductPriceElements[i].Text;
                multipleProductDetails.MultiProductDetails.Add(i, singleProductDetails);
            }
            return multipleProductDetails;
        }

        public string[] addProductToCartAndGetProductPrice(string productName, int productQuantity)
        {
            IWebElement singleProductPriceElement = _webDriver.FindElement(By.XPath($"//div[@class='fixed_wrapper']//a[@class='prdocutname' and contains(text(), '{productName}')]/following::div[@class='oneprice' or @class='pricenew'][1]"));
            string unitPrice = singleProductPriceElement.Text;
            IWebElement productAddToCartElement = _webDriver.FindElement(By.XPath($"//div[@class='fixed_wrapper']//a[@class='prdocutname' and contains(text(), '{productName}')]/following::a[@title='Add to Cart'][1]"));
            productAddToCartElement.Click();
            if (productQuantity > 1)
            {
                Input_ProductQty.Clear();
                Input_ProductQty.SendKeys(productQuantity.ToString());
                Input_ProductQty.Clear();
                Input_ProductQty.SendKeys(productQuantity.ToString());
                Input_ProductQty.SendKeys(Keys.Tab);
                Txt_ProductTotalPrice.Click();
                Thread.Sleep(2000);
            }
            string[] Price = new string[2];
            Price[0] = unitPrice;
            Price[1] = Txt_ProductTotalPrice.Text;
            Btn_AddProductToCartFinal.Click();
            
            return Price;
        }

        public int getTotalNumberOfProductsInCategory()
        {
            string[] splitText = Txt_NumberOfProductsInCategory.Text.Split(new[] { "of " }, StringSplitOptions.None);
            int totalProducts = Convert.ToInt32(splitText[splitText.Length - 1]);
            return totalProducts;
        }

        public void  clickCurrencyOption(string currency)
        {
            _seleniumHelper.MoveToElement(Btn_CurrencyOption);
            IWebElement currencyElement = _webDriver.FindElement(By.XPath($"//ul[@class='dropdown-menu currency']/li/a[text()='{currency}']"));
            currencyElement.Click();
        }

        public void goToSaleItemsPage()
        {
            Btn_HomeSpecials.Click();
        }
    }




}
