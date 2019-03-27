// <copyright file="HerokuappTestsNUnit.cs" company="Objectivity Bespoke Software Specialists">
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

using System.Collections.Generic;
using NUnit.Framework;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Tests.NUnit;
using Objectivity.Test.Automation.Tests.NUnit.DataDriven;
using TestFramework.ProjectExample.NUnit.PageObjects;

namespace TestFramework.ProjectExample.NUnit.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class HerokuappTestsNUnit : ProjectTestBase
    {
        [Test]
        public void BasicAuthTest()
        {
            var basicAuthPage =
                new InternetPage(this.DriverContext).OpenHomePageWithUserCredentials().GoToBasicAuthPage();

            Verify.That(
                this.DriverContext,
                () =>
                Assert.AreEqual(
                    "Congratulations! You must have the proper credentials.",
                    basicAuthPage.GetCongratulationsInfo));
        }

        [Test]
        [TestCaseSource(typeof(TestData), "Credentials")]
        public void FormAuthenticationPageTest(IDictionary<string, string> parameters)
        {
            InternetPage internetPage = new InternetPage(this.DriverContext);
            internetPage.OpenHomePage();
            internetPage.GoToFormAuthenticationPage();
            var formFormAuthentication = new FormAuthenticationPage(this.DriverContext);
            formFormAuthentication.EnterUserName(parameters["user"]);
            formFormAuthentication.EnterPassword(parameters["password"]);
            formFormAuthentication.LogOn();
            Verify.That(
                this.DriverContext,
                () => Assert.AreEqual(parameters["message"], formFormAuthentication.GetMessage));
        }


        [Test]
        [Category("PhantomJs")]
        [TestCaseSource(typeof(TestData), "LinksSetTestName")]
        public void CountLinksAndSetTestNameAtShiftingContentTest(IDictionary<string, string> parameters)
        {
            InternetPage internetPage = new InternetPage(this.DriverContext);
            internetPage.OpenHomePage();
            internetPage.GoToShiftingContentPage();

            var links = new ShiftingContentPage(this.DriverContext);
            Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        }

        [Test]
        [TestCaseSource(typeof(TestData), "Links")]
        public void CountLinksAtShiftingContentTest(IDictionary<string, string> parameters)
        {
            InternetPage internetPage = new InternetPage(this.DriverContext);
            internetPage.OpenHomePage();
            internetPage.GoToShiftingContentPage();

            var links = new ShiftingContentPage(this.DriverContext);
            Verify.That(this.DriverContext, () => Assert.AreEqual(parameters["number"], links.CountLinks()));
        }

        [Test]
        [Category("PhantomJs")]
        public void MultipleWindowsTest()
        {
            const string PageTitle = "New Window";

            InternetPage internetPage = new InternetPage(this.DriverContext);
            internetPage.OpenHomePage();
            internetPage.GoToMultipleWindowsPage();
            MultipleWindowsPage multipleWindowsPage = new MultipleWindowsPage(this.DriverContext);
            multipleWindowsPage.OpenNewWindowPage();
            NewWindowPage newWindowPage = new NewWindowPage(this.DriverContext);
            Assert.True(newWindowPage.IsPageTile(PageTitle), "wrong page title, should be {0}", PageTitle);
            Assert.True(newWindowPage.IsNewWindowH3TextVisible(PageTitle), "text is not equal to {0}", PageTitle);
        }
    }
}
