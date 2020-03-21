using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

using System;
using System.Collections.Generic;


namespace CodeFirst
{
    public class SogetiUtils
    {
        public class SelDescriptor
        {
            public By BySelector;
            public Boolean IsAlwaysPresent;
            public string Name;
            public SelDescriptor(string aName, By BySelector, Boolean IsAlwaysPresent)
            {
                this.Name = aName;
                this.BySelector = BySelector;
                this.IsAlwaysPresent = IsAlwaysPresent;
            }
        } 
        public class SelectorsMgr
        {
            Dictionary<string, SelDescriptor> SelectorsDict;

            public SelectorsMgr()
            {
                this.SelectorsDict = new Dictionary<string, SelDescriptor>();
            }
            public void AddSelDescriptor(SelDescriptor aSelDesc)
            {
                this.SelectorsDict.Add(aSelDesc.Name, aSelDesc);
            }
        } 
        public static void LoadWebPage(IWebDriver driver, string Url, int TimeoutInSecs=10) 
        {
            driver.Url = Url;
            new WebDriverWait(
                driver, 
                TimeSpan.FromSeconds(TimeoutInSecs)
            ).Until(
                d => ((IJavaScriptExecutor) d).ExecuteScript("return document.readyState").Equals("complete")
            );
        }

        public static IWebDriver InitDriver(string DriverType, string DriverDirPath) 
        {
            IWebDriver driver;
            string[] ValidDriverTypes = {"FireFox", "Chrome"};
            
            if (DriverType == "FireFox") {
                driver = new FirefoxDriver(DriverDirPath);
            } else if (DriverType == "Chrome")  {
                driver = new ChromeDriver(DriverDirPath);
            } else {
                throw new System.ArgumentException($"Parameter 'DriverType' was provided the value \"{DriverType}\"; it must be one of: \"{string.Join("\", \"", ValidDriverTypes)}\"!");
            }

            driver.Manage().Window.Maximize();
            _ = driver.Manage().Timeouts().ImplicitWait;
            return driver;
        }

        public static void WaitForUrl(IWebDriver driver, String url, int TimeoutInSecs=10) {
           new WebDriverWait(
               driver,
               TimeSpan.FromSeconds(TimeoutInSecs)
           ).Until(
               ExpectedConditions.UrlToBe(url)
           );
        }  

        public static void WaitForEltToBeVisible(IWebDriver driver, By BySelector, int TimeoutInSecs=10) {
           new WebDriverWait(
               driver,
               TimeSpan.FromSeconds(TimeoutInSecs)
           ).Until(
               ExpectedConditions.ElementIsVisible(BySelector)
           );
        }    
    }
}
