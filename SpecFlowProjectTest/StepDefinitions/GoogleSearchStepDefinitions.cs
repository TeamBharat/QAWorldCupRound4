using SpecFlowProjectTest.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProjectTest.StepDefinitions
{
    [Binding]
    public class GoogleSearchStepDefinitions
    {

        private readonly SamplePage _samplePage;
        
        public GoogleSearchStepDefinitions(IWebDriver driver)
        {
            _samplePage = new SamplePage(driver);
           
        }
        [Given(@"I navigate to google application")]
        public void GivenINavigateToGoogleApplication()
        {
            _samplePage.NavigateToUrl();
            
        }

        [Given(@"I enter some test")]
        public void GivenIEnterSomeTest()
        {
            Console.WriteLine("a");
        }

        [When(@"I click on Search")]
        public void WhenIClickOnSearch()
        {
            Console.WriteLine("a");
        }

        [Then(@"I verify text")]
        public void ThenIVerifyText()
        {
            Console.WriteLine("a");
        }
    }
}
