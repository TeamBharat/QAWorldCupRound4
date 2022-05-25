using BoDi;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow.TestFramework;
using OpenQA.Selenium.Remote;
using System.Runtime.CompilerServices;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using WebDriverManager.DriverConfigs.Impl;

namespace SpecFlowProjectTest.Hooks
{
    [Binding]
    public class HooksBase
    {
        public IObjectContainer _objectContainer;
        public static IConfiguration _configuration;
        public IWebDriver _driver;
        [ThreadStatic]
        public static ExtentTest featureName;
        [ThreadStatic]
        public static ExtentTest scenario;
        private static ExtentReports extent;
        private Utilities utilities;
        public ScenarioContext scenarioContext;
        public FeatureContext featureContext;
        private TimeSpan _timeout;
        private readonly AppSettings appSettings;


        public HooksBase(IObjectContainer objectContainer, ITestRunContext testRunContext, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectContainer = objectContainer;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            appSettings = new AppSettings();
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            string currentUser=System.Environment.UserName;
            ChromeOptions chroptions = new ChromeOptions();
            chroptions.AddArguments("--noerrdialogs");
            chroptions.AddArguments(@"user-data-dir=C:\Users\" + currentUser + @"\AppData\Local\Google\Chrome\User Data");
            chroptions.AddAdditionalCapability("useAutomationExtension", false);
            chroptions.AddArgument("no-sandbox");

            _timeout = TimeSpan.FromMinutes(10);
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
           _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
            _objectContainer.RegisterInstanceAs(_driver);
            var scenerioname = scenarioContext.ScenarioInfo.Title;
            createNode(scenerioname);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void createNode(string scenerioname)
        {
            scenario = featureName.CreateNode<Scenario>(scenerioname);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext) => featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);


        [AfterScenario]
        public void AfterScenario()
        {
            if (_driver != null)
            {
                LogHelper.Write("Quit Web Diver");
                _driver.Quit();
            }
            
        }

        public static ExtentReports InitializeReport()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(baseDir, "TestResults");
            if (extent == null)
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Directory.CreateDirectory(path);
                }
                else
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "index.html");
                var htmlReporter = new ExtentHtmlReporter(path);
                var configPath = Path.Combine(baseDir, "extent-config.xml");
                htmlReporter.LoadConfig(configPath);
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
                return extent;
            }
            else
            {
                return extent;
            }
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            DeleteExecutionProof();
            extent = InitializeReport();
            LogHelper.CreateLogFile();
        }

        [AfterTestRun]
        public static void AfterTestRun() => extent.Flush();

        [MethodImpl(MethodImplOptions.Synchronized)]
        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            String stepName = scenarioContext.StepContext.StepInfo.Text;
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepError = scenarioContext.TestError;

            utilities = new Utilities(_driver);
            if (stepError == null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                    PassScreenShot(stepName);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                    PassScreenShot(stepName);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                    PassScreenShot(stepName);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text);
                    PassScreenShot(stepName);
                }
            }
            else
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    FailScreenShot(stepName);
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    FailScreenShot(stepName);
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    FailScreenShot(stepName);
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    FailScreenShot(stepName);
                }
            }
        }

        private void FailScreenShot(String stepName)
        {
            scenario.AddScreenCaptureFromPath(utilities.FailScreenCapture(stepName));
            
        }

        private void PassScreenShot(String stepName)
        {
            scenario.AddScreenCaptureFromPath(utilities.PassScreenCapture(stepName));
           
        }

        public static void DeleteExecutionProof()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(baseDir, "TestResults");
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    File.Delete(file);
                    Console.WriteLine($"{file} is deleted.");
                }
            }
        }
    }
}
