
using AventStack.ExtentReports;

namespace Elabor8_PracticeTest_Arjun
{
    [Binding]
    public sealed class Hooks
    {
        
        static string configTheme = "standard";
        static string configReportPath = String.Empty;



        [ThreadStatic]
        private static ExtentTest feature;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static ExtentReports extentReport;
        private static readonly string base64ImageType = "base64";

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Initialize Extent report before test starts
            string workingDirectory = Environment.CurrentDirectory;
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(workingDirectory + @"\Results\index.html" );

            switch (configTheme.ToLower())
            {
                case "dark":
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                    break;
                case "standard":
                default:
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
                    break;
            }

            //Attach report to reporter
            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extentReport.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            feature = extentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void InitializeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void CleanUp(ScenarioContext scenarioContext)
        {
            string resultOfImplementation = scenarioContext.ScenarioExecutionStatus.ToString();

            //Pending Status
            if (resultOfImplementation == "UndefinedStep")
            {
                // Log.StepNotDefined();
            }
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepInfo = scenarioContext.StepContext.StepInfo.Text;


            //to check if we missed to implement steps inside method
            string resultOfImplementation = scenarioContext.ScenarioExecutionStatus.ToString();


            if (scenarioContext.TestError == null && resultOfImplementation == "OK")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo);
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo);
                else if (stepType == "And")
                    scenario.CreateNode<And>(stepInfo);
                else if (stepType == "But")
                    scenario.CreateNode<And>(stepInfo);
            }
            else if (scenarioContext.TestError != null)
            {
                Exception? innerException = scenarioContext.TestError.InnerException;
                string? testError = scenarioContext.TestError.Message;

                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo).Fail(innerException, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo).Fail(innerException, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "And")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "But")
                    scenario.CreateNode<Then>(stepInfo).Fail(testError, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());

            }
            else if (resultOfImplementation == "StepDefinitionPending")
            {
                string errorMessage = "Step Definition is not implemented!";

                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "When")
                    scenario.CreateNode<When>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());
                else if (stepType == "But")
                    scenario.CreateNode<Then>(stepInfo).Fail(errorMessage, MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64ImageType).Build());

            }

        }


    }
}
