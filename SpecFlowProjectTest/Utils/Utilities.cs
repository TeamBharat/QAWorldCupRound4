﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Excel= Microsoft.Office.Interop.Excel;

namespace SpecFlowProjectTest.Utils
{
    public class Utilities
    {
        private readonly IWebDriver _driver;
        private readonly int _implicitWaitSec = 20;
        private readonly int _conditionWait = 1;

       
        public Utilities(IWebDriver Driver)
        {
            _driver = Driver;
        }

        /// <summary>
        /// The funtion will wait for the element to be clickable
        /// </summary>
        /// <param name="element"></param>
        public void WaitUntilElementClickable(IWebElement element)
        {
            try
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(_conditionWait));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            }catch(Exception e)
            {
                LogHelper.Write("Unable to find element "+e);
            }
        }

        /// <summary>
        /// The funtion will wait for the element to be visible
        /// </summary>
        /// <param name="by"></param>
        public void WaitUntilElementIsVisible(By by)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(_conditionWait));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        /// <summary>
        /// The funtion will wait for the the page to load
        /// </summary>
        public void WaitForPageToLoadJavascript()
        {
            var js = (IJavaScriptExecutor) _driver;
            for (var i = 0; i <= 30; i++)
            {
                if (js.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                {
                    break;
                }
            }
            LogHelper.Write("Page loaded successfully");
        }

         /// <summary>
        /// This method is used for the implicit wait
        /// </summary>
        public void ImplicitWait() => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_implicitWaitSec);

        /// <summary>
        /// This method is used to enter text in the textbox by scrolling to the element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public void EnterValueIntextBox(IWebElement element, String value)
        {
            WaitUntilElementClickable(element);
            ((IJavaScriptExecutor) _driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Clear();
            element.SendKeys(value + Keys.Tab);
        }

        /// <summary>
        /// This method is used to scroll to the element
        /// </summary>
        /// <param name="element"></param>
        public void ScrollToElement(IWebElement element)
        {
            WaitUntilElementClickable(element);
            var js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 2px solid red;");
        }

        /// <summary>
        /// This method is used to get the value of the element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public string GetElementValue(IWebElement element)
        {
            WaitUntilElementClickable(element);
            var js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 2px solid red;");
            return element.GetAttribute("value");
        }

        /// <summary>
        /// This method is used to get the text of the element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public string GetElementText(IWebElement element)
        {
            WaitUntilElementClickable(element);
            var js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "border: 2px solid red;");
            LogHelper.Write("Getting text of " + element + " = " + element.Text);
            return element.Text;
        }

        /// <summary>
        /// This method is used to take the screenshot on pass status
        /// </summary>
        /// <param name="tcName"></param>
        /// <returns></returns>
        public string PassScreenCapture(string tcName)
        {
            string dest = null;
            try
            {
                if (tcName != null)
                {
                    var ss = ((ITakesScreenshot) _driver).GetScreenshot();
                    var sdf = DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    dest = Path.Combine(baseDir, "TestResults", sdf + tcName + "Verified.png");
                    ss.SaveAsFile(dest);
                }
            }
            catch (Exception e)
            {
                LogHelper.Write("Unable to capture screenshot " + e);
            }
            return dest;
        }

        /// <summary>
        /// This method is used to take the screenshot on fail status
        /// </summary>
        /// <param name="tcName"></param>
        /// <returns></returns>
        public string FailScreenCapture(string tcName)
        {
            string dest = null;
            try
            {
                if (tcName != null)
                {
                    var ss = ((ITakesScreenshot) _driver).GetScreenshot();
                    var sdf = DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    dest = Path.Combine(baseDir, "TestResults", sdf + tcName + "Failed.png");
                    ss.SaveAsFile(dest);
                }
            }
            catch (Exception e)
            {
                LogHelper.Write("Unable to capture screenshot " + e);
            }
            return dest;
        }

      
        /// <summary>
        /// This method is used to switch on the iframe
        /// </summary>
        /// <param name="id"></param>
        public void SwitchToFrame(string id)
        {
            try
            {
                _driver.SwitchTo().Frame(id);
                LogHelper.Write("Switched to frame " + id);
            }
            catch (NoSuchFrameException e_)
            {
                LogHelper.Write(e_ + " No Prame present with id" + id);
            }
        }

        /// <summary>
        /// This method is used to click by using javascript
        /// </summary>
        /// <param name="element"></param>
        public void JavaScriptClick(IWebElement element)
        {
            var js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            js.ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// This method is used to mouse over on the element using javascript
        /// </summary>
        /// <param name="webElement"></param>
        public void MouseOver(IWebElement webElement)
        {
            var javaScript = "var evObj = document.createEvent('MouseEvents');" +
                "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                "arguments[0].dispatchEvent(evObj);";
            var executor = _driver as IJavaScriptExecutor;
            executor.ExecuteScript(javaScript, webElement);
        }

        /// <summary>
        /// This method is used to check the value is numeric or not
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsNumeric(string s)
        {
            foreach (var c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This method is used to convert string into int
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public int ConvertIntoInt(string element) => int.Parse(element);

        
        public IWebElement expandRootElementShadow(string shadowelement)
        {
            return  (IWebElement) ((IJavaScriptExecutor) _driver).ExecuteScript(shadowelement);
        }

        public IList<IWebElement> expandRootElementsShadow(string shadowelement)
        {
            return (IList<IWebElement>) ((IJavaScriptExecutor) _driver).ExecuteScript(shadowelement);
        }
       
        public void JavascriptSendkeys(IWebElement element,String value)
        {
            var js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript("arguments[0].value='"+value+"';", element);
        }

        public bool GetWindowHandles(int no)
        {
            IList<string> windowHandles = new List<string>(_driver.WindowHandles);
            if(windowHandles.Count>1)
            {
             _driver.SwitchTo().Window(windowHandles[no]);
                return true;
            }
            return false;
        }

        public void SelectByValue(IWebElement webelement,string value)
        {
            try
            {
                SelectElement selectoption = new SelectElement(webelement);
                selectoption.SelectByValue(value);
            }
            catch (Exception)
            {
                LogHelper.Write(webelement +" dropdown is already selected");
            }
        }

        public void WriteDataToExcel(string filename)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp != null)
            {
                Excel.Workbook excelWorkbook = excelApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets.Add();

                excelWorksheet.Cells[1, 1] = "GroupName";
                excelWorksheet.Cells[1, 2] = "MemberCount";
                excelWorksheet.Cells[3, 1] = "Value3";
                excelWorksheet.Cells[4, 1] = "Value4";

                excelApp.ActiveWorkbook.SaveAs(@"C:\" + filename + ".xls", Excel.XlFileFormat.xlWorkbookNormal);

                excelWorkbook.Close();
                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorksheet);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelWorkbook);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

    }
}
