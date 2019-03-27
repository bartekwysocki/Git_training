﻿// <copyright file="DownloadPage.cs" company="Objectivity Bespoke Software Specialists">
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
using System.IO;
using NLog;
using Objectivity.Test.Automation.Common;
using Objectivity.Test.Automation.Common.Extensions;
using Objectivity.Test.Automation.Common.Helpers;
using Objectivity.Test.Automation.Common.Types;
using Objectivity.Test.Automation.Tests.PageObjects;

namespace TestFramework.ProjectExample.NUnit.PageObjects
{
    public class DownloadPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator downloadPageHeader = new ElementLocator(Locator.XPath, "//h3[.='File Downloader']"),
                                        fileLink = new ElementLocator(Locator.CssSelector, "a[href='download/{0}']");

        public DownloadPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for File Download page to open");
            this.Driver.IsElementPresent(this.downloadPageHeader, BaseConfiguration.ShortTimeout);
        }

        public void SaveFile(string fileName, string newName)
        {
            if (BaseConfiguration.TestBrowser == BrowserType.Firefox
                || BaseConfiguration.TestBrowser == BrowserType.Chrome
                || BaseConfiguration.TestBrowser == BrowserType.RemoteWebDriver)
            {
                this.Driver.GetElement(this.fileLink.Format(fileName), "Click on file").Click();
                FilesHelper.WaitForFileOfGivenName(fileName, this.DriverContext.DownloadFolder);
                FilesHelper.RenameFile(fileName, newName, this.DriverContext.DownloadFolder, FileType.Csv);
            }
            else
            {
               Logger.Info(CultureInfo.CurrentCulture, "Downloading files in browser {0} is not supported", BaseConfiguration.TestBrowser);
            }
        }

        public void SaveFile(string newName)
        {
           if (BaseConfiguration.TestBrowser == BrowserType.Firefox
                || BaseConfiguration.TestBrowser == BrowserType.Chrome
                || BaseConfiguration.TestBrowser == BrowserType.RemoteWebDriver)
            {
                var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder, FileType.Txt);
                this.Driver.GetElement(this.fileLink.Format("ObjectivityTestAutomationCSHarpFramework.txt")).Click();
                FilesHelper.WaitForFileOfGivenType(FileType.Txt, filesNumber, this.DriverContext.DownloadFolder);
                FileInfo file = FilesHelper.GetLastFile(this.DriverContext.DownloadFolder, FileType.Txt);
                FilesHelper.RenameFile(file.Name, newName, this.DriverContext.DownloadFolder, FileType.Txt);
            }
            else
            {
                Logger.Info(CultureInfo.CurrentCulture, "Downloading files in browser {0} is not supported", BaseConfiguration.TestBrowser);
            }
        }

        public void SaveAnyFile()
        {
            if (BaseConfiguration.TestBrowser == BrowserType.Firefox
                || BaseConfiguration.TestBrowser == BrowserType.Chrome
                || BaseConfiguration.TestBrowser == BrowserType.RemoteWebDriver)
            {
                var filesNumber = FilesHelper.CountFiles(this.DriverContext.DownloadFolder);
                this.Driver.GetElement(this.fileLink.Format("ObjectivityTestAutomationCSHarpFramework.txt")).Click();
                FilesHelper.WaitForFile(filesNumber, this.DriverContext.DownloadFolder);
                FileInfo file = FilesHelper.GetLastFile(this.DriverContext.DownloadFolder);
                FilesHelper.RenameFile(BaseConfiguration.ShortTimeout, file.Name, "name_of_file_branch.txt", this.DriverContext.DownloadFolder);
            }
            else
            {
                Logger.Info(CultureInfo.CurrentCulture, "Downloading files in browser {0} is not supported", BaseConfiguration.TestBrowser);
            }
        }

        public string CheckIfScreenShotIsSaved(int screenShotNumber)
        {
            FilesHelper.WaitForFileOfGivenType(FileType.Png, 5, screenShotNumber, this.DriverContext.ScreenShotFolder, true);
            var nameOfFile = FilesHelper.GetLastFile(this.DriverContext.ScreenShotFolder, FileType.Png);

            return nameOfFile.Name;
        }

        public void SaveWebDriverScreenShot()
        {
            this.DriverContext.SaveScreenshot(new ErrorDetail(this.DriverContext.TakeScreenshot(), DateTime.Now, null), this.DriverContext.ScreenShotFolder, this.DriverContext.TestTitle);
        }
    }
}