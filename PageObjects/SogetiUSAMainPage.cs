using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CodeFirst
{
    public class SogetiUSAMainPage
    {
        IWebDriver driver;
        public Dictionary<string, By> NavMenuSelectors;
                   

        public SogetiUSAMainPage(IWebDriver driver)
        {
            this.driver = driver;
            this.NavMenuSelectors = new Dictionary<string, By>();
            this.NavMenuSelectors.Add("WhoWeAre", By.XPath("//span[text()='Who We Are']"));
            this.NavMenuSelectors.Add("WhatWeDo", By.XPath("//span[text()='What We Do']"));
            this.NavMenuSelectors.Add("Explore", By.XPath("//span[text()='Explore']"));
            this.NavMenuSelectors.Add("JoinOurTeam", By.XPath("//span[text()='Join Our Team']"));
            this.NavMenuSelectors.Add("Contact", By.XPath("//span[text()='Contact']"));
        }

        public void DoMenuHover(By MenuSelector, int HoverSleepTimeInMilliSecs=4000)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(MenuSelector));
            Actions action = new Actions(driver);
            action.MoveToElement(element).Build().Perform();

            if (HoverSleepTimeInMilliSecs > 0) { 
                System.Threading.Thread.Sleep(HoverSleepTimeInMilliSecs);
            }
        }


        public void DoMenuHoverThenClickMenuLink(By MenuSelector, string LinkText, int HoverSleepTimeInMilliSecs=4000)
        {
            IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(MenuSelector));
            Actions action = new Actions(driver);
            action.MoveToElement(element).Build().Perform();

            if (HoverSleepTimeInMilliSecs > 0) {
                System.Threading.Thread.Sleep(HoverSleepTimeInMilliSecs);
            }

            IWait<IWebDriver> wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement subelement = wait2.Until(ExpectedConditions.ElementIsVisible(By.LinkText(LinkText)));
            action.MoveToElement(subelement);
            action.Click().Build().Perform();
        }
    }
}