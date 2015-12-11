using System;
using NUnit.Framework;
using System.Threading;
using HP.LFT.SDK;
using HP.LFT.SDK.Web;

namespace LeanFtTestProject1
{
    [TestFixture]
    public class OrderCheckbook : UnitTestClassBase
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Setup once per fixture
        }

        [SetUp]
        public void SetUp()
        {
            // Before each test

        }

        
        //Xpath for the following would be:
        // Flowers - //TABLE[@id="dlProducts"]/TBODY[1]/TR[2]/TD[1]/DIV[1]/DIV[3]/DIV[1]/INPUT[1]
        // Other   - //TABLE[@id="dlProducts"]/TBODY[1]/TR[2]/TD[2]/DIV[1]/DIV[3]/DIV[1]/INPUT[1]
        String[] loopStr = new String [] {"Flowers - Rose Quantity must range from 1 to 10 ",
                                      "Other - Buildings Quantity must range from 1 to 10 ",
                                      "Sports - Yacht Quantity must range from 1 to 10 "};
        [Test, TestCaseSource("loopStr")]
        public void Test(string checkName)
        {
            IBrowser browser = BrowserFactory.Launch(BrowserType.Chrome);
            browser.Navigate("http://alm-aob:47001/advantage");

            browser.Describe<IEditField>(new EditFieldDescription
            {
                Type = @"text",
                TagName = @"INPUT",
                Name = @"j_username"
            }).SetValue("jojo");

            browser.Describe<IEditField>(new EditFieldDescription
            {
                Type = @"password",
                TagName = @"INPUT",
                Name = @"j_password"
            }).SetSecure("5669ea5045041e6d5b070cf58af8342cce6a113d9e7def075695ecd11126");

            browser.Describe<IButton>(new ButtonDescription
            {
                ButtonType = @"submit",
                TagName = @"INPUT",
                Name = @"Login"
            }).Click();
            
            // Get to the checks
            browser.Describe<ILink>(new LinkDescription
		{
			TagName = @"A",
			InnerText = @"Order Checkbooks "
		}).Click();

            browser.Describe<IFrame>(new FrameDescription()).Describe<IWebElement>(new WebElementDescription
            {
                TagName = @"TD",
                //InnerText = @"Flowers - Rose Quantity must range from 1 to 10 "
                InnerText = checkName
            }).Describe<ICheckBox>(new CheckBoxDescription
                        {
                            Type = @"checkbox",
                            TagName = @"INPUT"
                        }).Click();

            Thread.Sleep(2000);
            browser.Close();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up after each test
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // Clean up once per fixture
        }
    }
}
