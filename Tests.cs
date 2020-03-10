using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

// Sample comment
namespace CodeFirst
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;
 
        [OneTimeSetUp]
        public void Setup()
        {
            this.driver = SogetiUtils.InitDriver("Chrome", "C:\\Users\\jefisher\\Documents\\WebDriver");
        }

        [TestCase("WhoWeAre")]
        [TestCase("WhatWeDo")]
        [TestCase("Explore")]
        [TestCase("JoinOurTeam")]
        [TestCase("Contact")]
        public void HoverOverMenu(string MenuCode)
        {
            SogetiUtils.LoadWebPage(this.driver, "https://www.us.sogeti.com");

            SogetiUSAMainPage SogetiUSAMainPageObj = new SogetiUSAMainPage(driver);
            SogetiUSAMainPageObj.DoMenuHover(SogetiUSAMainPageObj.NavMenuSelectors[MenuCode]);
            Assert.Pass();

            Thread.Sleep(2000); 
        }


        [Test]
        public void ContactUs()
        {
            SogetiUtils.LoadWebPage(this.driver, "https://www.us.sogeti.com");

            SogetiUSAMainPage SogetiUSAMainPageObj = new SogetiUSAMainPage(driver);
            SogetiUSAMainPageObj.DoMenuHoverThenClickMenuLink(SogetiUSAMainPageObj.NavMenuSelectors["Contact"], "Contact us");

            ContactUsPage ContactUsPageObj = new ContactUsPage(driver);
            ContactUsPageObj.SetEntryFieldText("FirstName", "Kelly");
            Assert.AreEqual( ContactUsPageObj.GetEntryFieldText("FirstName"), "Kelly");
            
            ContactUsPageObj.SetEntryFieldText("LastName", "Maroney");
            Assert.AreEqual( ContactUsPageObj.GetEntryFieldText("LastName"), "Maroney");

            ContactUsPageObj.SetEntryFieldText("JobTitle", "Marketing Director");
            Assert.AreEqual( ContactUsPageObj.GetEntryFieldText("JobTitle"), "Marketing Director");

            ContactUsPageObj.SetEntryFieldText("Email", "Kelly.Maroney@us.sogeti.com");
            Assert.AreEqual( ContactUsPageObj.GetEntryFieldText("Email"), "Kelly.Maroney@us.sogeti.com");

            ContactUsPageObj.SetEntryFieldText("PhoneNumber", "555-555-1212");
            Assert.AreEqual( ContactUsPageObj.GetEntryFieldText("PhoneNumber"), "555-555-1212");

            ContactUsPageObj.SetEntryFieldText("Company", "Sogeti USA");
            Assert.AreEqual( ContactUsPageObj.GetEntryFieldText("Company"), "Sogeti USA");

            ContactUsPageObj.SelectDropdownValue("Industry", "Energy");
            Assert.AreEqual( ContactUsPageObj.GetDropdownValue("Industry"), "Energy");
            
            ContactUsPageObj.SelectDropdownValue("Position", "Director");
            Assert.AreEqual( ContactUsPageObj.GetDropdownValue("Position"), "Director");
            
            ContactUsPageObj.SelectDropdownValue("Relationship", "Partner");
            Assert.AreEqual( ContactUsPageObj.GetDropdownValue("Relationship"), "Partner");
            
            ContactUsPageObj.SelectDropdownValue("ThisIsRegarding", "Yes");
            Assert.AreEqual( ContactUsPageObj.GetDropdownValue("ThisIsRegarding"), "Yes");

            ContactUsPageObj.SetEntryFieldText("YourMessage", "The quick brown fox jumped over the lazy dog.\n\nThe quick brown fox jumped over.");
            
            Thread.Sleep(2000);             
        }


        [OneTimeTearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}
