using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationStoresTest.Helper
{
    public class SeleniumHelper
    {
        private readonly IWebDriver _webDriver;

        public SeleniumHelper(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void MoveToElement(IWebElement Element)
        {
            new Actions(_webDriver).MoveToElement(Element).Perform();
        }


    }
}
