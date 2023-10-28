
using AutomationStoresTest.Helper;
using BoDi;
using OpenQA.Selenium;

namespace AutomationStoresTest.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        public static ExtentReportHelper? ExtentReporter;

        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;

        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            ExtentReporter = new ExtentReportHelper(_scenarioContext, _featureContext);
            
        }

        [BeforeTestRun]
        public static void BeforeTestRun(ObjectContainer testThreadContainer)
        {
            //Initialize a shared BrowserDriver in the global container
            testThreadContainer.BaseContainer.Resolve<BrowserDriver>();
        }

        [BeforeScenario()]
        public void BeforeScenarioSetUp()
        {
            ExtentReporter.HtmlReportBeforeScenario();
           
        }

        [AfterStep]
        public void AfterStep()
        {

            ExtentReporter.HtmlReportAfterEachScenarioStep();

        }

        [AfterScenario()]
        public void AfterScenarioSetUp()
        {
            ExtentReportHelper.HtmlReportAfterScenario();
        }


    }
}