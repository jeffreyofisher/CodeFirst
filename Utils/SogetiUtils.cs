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
            public Dictionary<string, string> AttrsDict;
            public string Name;
            public SelDescriptor(string aName, By aBySelector, Dictionary<string, string> aAttrsDict)
            {
                Name = aName;
                BySelector = aBySelector;
                AttrsDict = aAttrsDict;
            }
        } 

        public class SelsMgr
        {
            Dictionary<string, SelDescriptor> SelsDict;

            public SelsMgr()
            {
                SelsDict = new Dictionary<string, SelDescriptor>();
            }
            public void AddSelDescriptor(SelDescriptor aSelDesc)
            {
                SelsDict.Add(aSelDesc.Name, aSelDesc);
            }
        } 

        public class POM
        {
            public Dictionary<string, POM> POMTracker = new Dictionary<string, POM>();
            public string Path;
            public SelsMgr SelMgr;
        
            public POM(string aPath, SelsMgr aSelectorsMgr)
            {
                Path = aPath;
                SelMgr = aSelectorsMgr;
                POMTracker.Add(Path, this);
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
