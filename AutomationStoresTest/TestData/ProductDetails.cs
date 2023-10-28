using AutomationStoresTest.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationStoresTest.TestData
{
    public class MultipleProductDetails
    {
        public IDictionary<int, SingleProductDetails> MultiProductDetails { get; set; }
    }

    public class SingleProductDetails
    {
        public string ProductName { get; set; }
        public string ProductUnitPrice { get; set; }
        public int ProductQty { get; set; }
        public string ProductTotalPrice { get; set; }
    }
}
