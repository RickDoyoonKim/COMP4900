﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 15.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace CodedUITestProject
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public partial class UIMap
    {
        
        /// <summary>
        /// RecordedMethod1
        /// </summary>
        public void RecordedMethod1()
        {
            #region Variable Declarations
            WinEdit uIBILLINGDASHBOARDEdit = this.UIHomePageImpactWebsitWindow.UIBILLINGDASHBOARDHyperlink.UIBILLINGDASHBOARDEdit;
            WinEdit uISTARTANALYSISEdit = this.UIHomePageImpactWebsitWindow.UISTARTANALYSISHyperlink.UISTARTANALYSISEdit;
            WinEdit uIPAYMENTHISTORYEdit = this.UIHomePageImpactWebsitWindow.UIPAYMENTHISTORYHyperlink.UIPAYMENTHISTORYEdit;
            WinEdit uIDetailsEdit = this.UIHomePageImpactWebsitWindow.UIDetailsButton.UIDetailsEdit;
            WinEdit uIBacktoListEdit = this.UIHomePageImpactWebsitWindow.UIBacktoListHyperlink.UIBacktoListEdit;
            WinEdit uIIMPACTWEBSITEEdit = this.UIHomePageImpactWebsitWindow.UIIMPACTWEBSITEHyperlink.UIIMPACTWEBSITEEdit;
            #endregion

            // Click 'BILLING DASHBOARD' text box
            Mouse.Click(uIBILLINGDASHBOARDEdit, new Point(41, 8));

            // Click 'START ANALYSIS' text box
            Mouse.Click(uISTARTANALYSISEdit, new Point(31, 14));

            // Click 'PAYMENT HISTORY' text box
            Mouse.Click(uIPAYMENTHISTORYEdit, new Point(54, 5));

            // Click 'Details' text box
            Mouse.Click(uIDetailsEdit, new Point(7, 1));

            // Click 'Back to List' text box
            Mouse.Click(uIBacktoListEdit, new Point(36, 10));

            // Click 'IMPACT WEBSITE' text box
            Mouse.Click(uIIMPACTWEBSITEEdit, new Point(220, 1));
        }
        
        #region Properties
        public UIHomePageImpactWebsitWindow UIHomePageImpactWebsitWindow
        {
            get
            {
                if ((this.mUIHomePageImpactWebsitWindow == null))
                {
                    this.mUIHomePageImpactWebsitWindow = new UIHomePageImpactWebsitWindow();
                }
                return this.mUIHomePageImpactWebsitWindow;
            }
        }
        #endregion
        
        #region Fields
        private UIHomePageImpactWebsitWindow mUIHomePageImpactWebsitWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIHomePageImpactWebsitWindow : WinWindow
    {
        
        public UIHomePageImpactWebsitWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "Home Page - Impact Website - Mozilla Firefox";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "MozillaWindowClass";
            this.WindowTitles.Add("Home Page - Impact Website - Mozilla Firefox");
            this.WindowTitles.Add("Billing - Impact Website - Mozilla Firefox");
            this.WindowTitles.Add("Index - Impact Website - Mozilla Firefox");
            this.WindowTitles.Add("PaymentHistory - Impact Website - Mozilla Firefox");
            this.WindowTitles.Add("Details - Impact Website - Mozilla Firefox");
            #endregion
        }
        
        #region Properties
        public UIBILLINGDASHBOARDHyperlink UIBILLINGDASHBOARDHyperlink
        {
            get
            {
                if ((this.mUIBILLINGDASHBOARDHyperlink == null))
                {
                    this.mUIBILLINGDASHBOARDHyperlink = new UIBILLINGDASHBOARDHyperlink(this);
                }
                return this.mUIBILLINGDASHBOARDHyperlink;
            }
        }
        
        public UISTARTANALYSISHyperlink UISTARTANALYSISHyperlink
        {
            get
            {
                if ((this.mUISTARTANALYSISHyperlink == null))
                {
                    this.mUISTARTANALYSISHyperlink = new UISTARTANALYSISHyperlink(this);
                }
                return this.mUISTARTANALYSISHyperlink;
            }
        }
        
        public UIPAYMENTHISTORYHyperlink UIPAYMENTHISTORYHyperlink
        {
            get
            {
                if ((this.mUIPAYMENTHISTORYHyperlink == null))
                {
                    this.mUIPAYMENTHISTORYHyperlink = new UIPAYMENTHISTORYHyperlink(this);
                }
                return this.mUIPAYMENTHISTORYHyperlink;
            }
        }
        
        public UIDetailsButton UIDetailsButton
        {
            get
            {
                if ((this.mUIDetailsButton == null))
                {
                    this.mUIDetailsButton = new UIDetailsButton(this);
                }
                return this.mUIDetailsButton;
            }
        }
        
        public UIBacktoListHyperlink UIBacktoListHyperlink
        {
            get
            {
                if ((this.mUIBacktoListHyperlink == null))
                {
                    this.mUIBacktoListHyperlink = new UIBacktoListHyperlink(this);
                }
                return this.mUIBacktoListHyperlink;
            }
        }
        
        public UIIMPACTWEBSITEHyperlink UIIMPACTWEBSITEHyperlink
        {
            get
            {
                if ((this.mUIIMPACTWEBSITEHyperlink == null))
                {
                    this.mUIIMPACTWEBSITEHyperlink = new UIIMPACTWEBSITEHyperlink(this);
                }
                return this.mUIIMPACTWEBSITEHyperlink;
            }
        }
        #endregion
        
        #region Fields
        private UIBILLINGDASHBOARDHyperlink mUIBILLINGDASHBOARDHyperlink;
        
        private UISTARTANALYSISHyperlink mUISTARTANALYSISHyperlink;
        
        private UIPAYMENTHISTORYHyperlink mUIPAYMENTHISTORYHyperlink;
        
        private UIDetailsButton mUIDetailsButton;
        
        private UIBacktoListHyperlink mUIBacktoListHyperlink;
        
        private UIIMPACTWEBSITEHyperlink mUIIMPACTWEBSITEHyperlink;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIBILLINGDASHBOARDHyperlink : WinHyperlink
    {
        
        public UIBILLINGDASHBOARDHyperlink(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinHyperlink.PropertyNames.Name] = "BILLING DASHBOARD";
            this.WindowTitles.Add("Home Page - Impact Website - Mozilla Firefox");
            #endregion
        }
        
        #region Properties
        public WinEdit UIBILLINGDASHBOARDEdit
        {
            get
            {
                if ((this.mUIBILLINGDASHBOARDEdit == null))
                {
                    this.mUIBILLINGDASHBOARDEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIBILLINGDASHBOARDEdit.SearchProperties[WinEdit.PropertyNames.Name] = "BILLING DASHBOARD";
                    this.mUIBILLINGDASHBOARDEdit.WindowTitles.Add("Home Page - Impact Website - Mozilla Firefox");
                    #endregion
                }
                return this.mUIBILLINGDASHBOARDEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIBILLINGDASHBOARDEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UISTARTANALYSISHyperlink : WinHyperlink
    {
        
        public UISTARTANALYSISHyperlink(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinHyperlink.PropertyNames.Name] = "START ANALYSIS";
            this.WindowTitles.Add("Billing - Impact Website - Mozilla Firefox");
            #endregion
        }
        
        #region Properties
        public WinEdit UISTARTANALYSISEdit
        {
            get
            {
                if ((this.mUISTARTANALYSISEdit == null))
                {
                    this.mUISTARTANALYSISEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUISTARTANALYSISEdit.SearchProperties[WinEdit.PropertyNames.Name] = "START ANALYSIS ";
                    this.mUISTARTANALYSISEdit.WindowTitles.Add("Billing - Impact Website - Mozilla Firefox");
                    #endregion
                }
                return this.mUISTARTANALYSISEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUISTARTANALYSISEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIPAYMENTHISTORYHyperlink : WinHyperlink
    {
        
        public UIPAYMENTHISTORYHyperlink(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinHyperlink.PropertyNames.Name] = "PAYMENT HISTORY";
            this.WindowTitles.Add("Index - Impact Website - Mozilla Firefox");
            #endregion
        }
        
        #region Properties
        public WinEdit UIPAYMENTHISTORYEdit
        {
            get
            {
                if ((this.mUIPAYMENTHISTORYEdit == null))
                {
                    this.mUIPAYMENTHISTORYEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIPAYMENTHISTORYEdit.SearchProperties[WinEdit.PropertyNames.Name] = "PAYMENT HISTORY";
                    this.mUIPAYMENTHISTORYEdit.WindowTitles.Add("Index - Impact Website - Mozilla Firefox");
                    #endregion
                }
                return this.mUIPAYMENTHISTORYEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIPAYMENTHISTORYEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIDetailsButton : WinButton
    {
        
        public UIDetailsButton(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinButton.PropertyNames.Name] = " Details";
            this.WindowTitles.Add("PaymentHistory - Impact Website - Mozilla Firefox");
            #endregion
        }
        
        #region Properties
        public WinEdit UIDetailsEdit
        {
            get
            {
                if ((this.mUIDetailsEdit == null))
                {
                    this.mUIDetailsEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIDetailsEdit.SearchProperties[WinEdit.PropertyNames.Name] = " Details ";
                    this.mUIDetailsEdit.WindowTitles.Add("PaymentHistory - Impact Website - Mozilla Firefox");
                    #endregion
                }
                return this.mUIDetailsEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIDetailsEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIBacktoListHyperlink : WinHyperlink
    {
        
        public UIBacktoListHyperlink(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinHyperlink.PropertyNames.Name] = "Back to List";
            this.WindowTitles.Add("Details - Impact Website - Mozilla Firefox");
            #endregion
        }
        
        #region Properties
        public WinEdit UIBacktoListEdit
        {
            get
            {
                if ((this.mUIBacktoListEdit == null))
                {
                    this.mUIBacktoListEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIBacktoListEdit.SearchProperties[WinEdit.PropertyNames.Name] = "Back to List";
                    this.mUIBacktoListEdit.WindowTitles.Add("Details - Impact Website - Mozilla Firefox");
                    #endregion
                }
                return this.mUIBacktoListEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIBacktoListEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "15.0.26208.0")]
    public class UIIMPACTWEBSITEHyperlink : WinHyperlink
    {
        
        public UIIMPACTWEBSITEHyperlink(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinHyperlink.PropertyNames.Name] = "IMPACT WEBSITE";
            this.WindowTitles.Add("PaymentHistory - Impact Website - Mozilla Firefox");
            #endregion
        }
        
        #region Properties
        public WinEdit UIIMPACTWEBSITEEdit
        {
            get
            {
                if ((this.mUIIMPACTWEBSITEEdit == null))
                {
                    this.mUIIMPACTWEBSITEEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUIIMPACTWEBSITEEdit.SearchProperties[WinEdit.PropertyNames.Name] = "IMPACT WEBSITE";
                    this.mUIIMPACTWEBSITEEdit.WindowTitles.Add("PaymentHistory - Impact Website - Mozilla Firefox");
                    #endregion
                }
                return this.mUIIMPACTWEBSITEEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUIIMPACTWEBSITEEdit;
        #endregion
    }
}
