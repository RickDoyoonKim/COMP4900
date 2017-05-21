using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;


namespace ImpactWebsite.UItests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest_Register
    {
        public CodedUITest_Register()
        {
        }

        /// <summary>
        /// Test Register UI
        /// </summary>
        [TestMethod]
        public void CodedUITestMethod_Register()
        {
            var browser = BrowserWindow.Launch("http://localhost:5243");
            ClickLink(browser, "Register");
            EnterText(browser, "Email", "UItest@email.com");
            EnterText(browser, "FirstName", "First Name");
            EnterText(browser, "LastName", "Last Name");
            EnterText(browser, "CompanyName", "Company Name");
            EnterText(browser, "Password", "Password1!");
            EnterText(browser, "ConfirmPassword", "Password1!");
            ClickButton(browser, "Register");
        }

        /// <summary>
        /// For button click (input type="submit")
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="value"></param>
        void ClickButton(UITestControl parent, string value)
        {
            var button = new HtmlInputButton(parent);
            button.SearchProperties.Add(HtmlInputButton.PropertyNames.ValueAttribute, value);

            Mouse.Click(button);
        }

        /// <summary>
        /// For link click
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="innerText"></param>
        void ClickLink(UITestControl parent, string innerText)
        {
            var link = new HtmlHyperlink(parent);
            link.SearchProperties.Add(HtmlHyperlink.PropertyNames.InnerText, innerText);

            Mouse.Click(link);
        }

        /// <summary>
        /// For edit text
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        void EnterText(UITestControl parent, string id, string value)
        {
            var edit = new HtmlEdit(parent);
            edit.SearchProperties.Add(HtmlEdit.PropertyNames.Id, id);
            edit.Text = value;
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
