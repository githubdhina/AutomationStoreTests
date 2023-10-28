using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace AutomationStoresTest.Helper
{
    public class ExtentReportHelper
    {

        private static ExtentReports? extent;
        private static ExtentTest? test;
        private static DateTime reportDateAndTime = DateTime.Now;
        private static string dateInString = String.Format("{0:yyyy_MM_dd_HH_mm_ss}", reportDateAndTime);
        public static string BaseDriectory = System.AppDomain.CurrentDomain.BaseDirectory;

        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;

        public ExtentReportHelper(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }
        public void HtmlReportBeforeScenario()
        {
            extent = GetExtent();
            test = extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
            test.AssignCategory(_featureContext.FeatureInfo.Title);
        }


        public void HtmlReportAfterEachScenarioStep()
        {
            if (_scenarioContext.TestError != null)
            {
                var error = _scenarioContext.TestError;
                var errormessage = "<pre>" + error.Message + "</pre>";
                var errorStack = "<pre>" + error.StackTrace + "</pre>";
                test.Log(Status.Fail, _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString() + " " + _scenarioContext.StepContext.StepInfo.Text + _scenarioContext.StepContext.StepInfo.Table);
                test.Log(Status.Error, errormessage);
                test.Log(Status.Error, errorStack);

            }

            else
            {
                test.Log(Status.Pass, _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString() + " " + _scenarioContext.StepContext.StepInfo.Text);
            }
        }

        public static void HtmlReportAfterScenario()
        {
            extent.Flush();
        }

        public void HtmlReportAfterTests(TestContext testContext)
        {

            if (testContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                test.Log(Status.Fail, testContext.TestName);

            }

            else
            {
                test.Log(Status.Pass, testContext.TestName);
            }
            extent.Flush();
        }

        public static void EndHTMLReporting()
        {
            extent.Flush();

        }

        public static void ExtentCustomInfoLogger(string TextToLog)
        {

            test.Log(Status.Info, TextToLog);

        }

        public static void ExtentCustomPassLogger(string TextToLog)
        {

            test.Log(Status.Pass, TextToLog);

        }

        public static void ExtentCustomWarnLogger(string TextToLog)
        {
            test.Log(Status.Warning, TextToLog);
        }

        private static ExtentReports GetExtent()
        {
            bool reportLocationexists = System.IO.Directory.Exists(BaseDriectory);
            if (!reportLocationexists)
            {
                Directory.CreateDirectory(BaseDriectory);
            }

            if (extent != null)
                return extent;
            else
                extent = new ExtentReports();

            var HTMLReport = new ExtentSparkReporter(BaseDriectory + @"\AutomationStoreTests" + dateInString + ".html");
            extent.AttachReporter(HTMLReport);
            return extent;
        }
    }
}
