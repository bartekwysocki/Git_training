﻿// <copyright file="InternetPage.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>
// <license>
//     The MIT License (MIT)
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//     SOFTWARE.
// </license>

using System;
using System.Globalization;
using NLog;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace TestFramework.ProjectExample.NUnit.PageObjects
{
    public class InternetPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator
            linkLocator = new ElementLocator(Locator.CssSelector, "a[href='/{0}']"),
            basicAuthLink = new ElementLocator(Locator.XPath, "//a[contains(text(),'Auth')]");

        public InternetPage(DriverContext driverContext)
            : base(driverContext)
        {
        }

        /// <summary>
        /// Methods for this HomePage
        /// </summary>
        public void OpenHomePage()
        {
            var url = BaseConfiguration.GetUrlValue;
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
        }

        public InternetPage OpenHomePageWithUserCredentials()
        {
            var url = BaseConfiguration.GetUrlValueWithUserCredentials;
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

		  public DownloadPage GoToFileDownloader()
		        {
		            this.Driver.GetElement(this.linkLocator.Format("download")).Click();
		            return new DownloadPage(this.DriverContext);
		        }        public MultipleWindowsPage GoToMultipleWindowsPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("windows")).Click();
            return new MultipleWindowsPage(this.DriverContext);
        }

        public BasicAuthPage GoToBasicAuthPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("basic_auth")).Click();
            return new BasicAuthPage(this.DriverContext);
        }

        public FormAuthenticationPage GoToFormAuthenticationPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("login")).Click();
            return new FormAuthenticationPage(this.DriverContext);
        }

        public ShiftingContentPage GoToShiftingContentPage()
        {
            this.Driver.GetElement(this.linkLocator.Format("shifting_content")).Click();
            return new ShiftingContentPage(this.DriverContext);
        }
    }
}
