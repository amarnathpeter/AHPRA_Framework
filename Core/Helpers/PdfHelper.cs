using System;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenQA.Selenium;
using GemBox.Document;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using Core.Config;
using Microsoft.Win32;
using System.IO;
using iTextSharp.text;

namespace Core.Helpers
{

    /// <summary>
    /// Pdf Helper
    /// </summary>
    public static class PdfHelper
    {
       
        public static void IntializeDownloadLocation()
        {
            Settings.DownloadsLocation = new FileInfo(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + "\\" + "Download";
        }
        public static string IsFolderExist(string folderName)
        {
            bool isExists = System.IO.Directory.Exists(Settings.DownloadsLocation);
            if (!isExists)
                System.IO.Directory.CreateDirectory(Settings.DownloadsLocation);
            return Settings.DownloadsLocation;
        }
        public static string PdfFileCreator(string fileName, int numberOfPages)
        {
            string fullFileName = fileName + ".pdf";
            var output = System.IO.Path.Combine(Settings.DownloadsLocation, fullFileName);
            if (File.Exists(output))
            {
                File.Delete(output);
            }
            var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            using (FileStream fs = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (Document doc = new Document(PageSize.A4, 2, 2, 10, 10))
                {
                    PdfContentByte _pcb;
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                    {
                        doc.Open();
                        doc.NewPage();
                        DictionaryProperties.Details["Pdf Text"] = "Page 1";
                        _pcb = writer.DirectContent;
                        _pcb.SetFontAndSize(bf, 12);
                        _pcb.BeginText();
                        _pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DictionaryProperties.Details["Pdf Text"], 40, 600, 0);
                        _pcb.EndText();
                        doc.NewPage();
                        _pcb.SetFontAndSize(bf, 12);
                        _pcb.BeginText();
                        _pcb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Page 2", 100, 400, 0);
                        _pcb.EndText();
                        doc.Close();
                    }
                }
            }
            return fullFileName;
        }
        /// <summary>
        /// This will get text of all pages from pdf
        /// PathToFile should be the absolute or relative path to the pdf file with Filename.pdf 
        /// for example - D://test//Test.pdf
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        public static string GetAllTextFromPDF(this IWebDriver driver, string pathToFile)
        {
            try
            {
                StringBuilder text = new StringBuilder();
                using (PdfReader reader = new PdfReader(pathToFile))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                }
                return text.ToString();
            }
            catch (Exception e)
            {
                LogHelper.LogException(e);
                throw;
            }
        }


        /// <summary>
        /// This will get text of desired page from pdf
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pathToFile"></param>
        /// <param name="pageN"></param>
        /// <returns></returns>
        public static string GetDefinedPageTextFromPDF(this IWebDriver driver, string pathToFile, int pageN)
        {
            try
            {
                StringBuilder text = new StringBuilder();
                using (PdfReader reader = new PdfReader(pathToFile))
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, pageN));
                return text.ToString();
            }
            catch (Exception e)
            {
                LogHelper.LogException(e);
                throw;
            }
        }

        /// <summary>
        /// This will upload file where input field is available
        /// Please add file name and extension in file path
        /// </summary>
        /// <param name="element"></param>
        /// <param name="filePath"></param>
        public static void UploadDocument(this IWebElement element, string filePath)
        {
            try
            {
                Thread.Sleep(6000);
                element.SendKeys(filePath);
                Thread.Sleep(6000);
            }
            catch (Exception e)
            {
                LogHelper.LogException(e);
                throw;
            }
        }

        /// <summary>
        /// This will get downloaded pdf file from IE cache
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetPdfFromIeCacheWindows(string filename)
        {
            string path = Assembly.GetCallingAssembly().CodeBase;
            string dependencyDirectory = Settings.DependenciesLocation;
            string downloadDirectory = Settings.DownloadsLocation;
            string ProjectPath = path.Substring(0, path.LastIndexOf("/bin"));
            string batFilePath = ProjectPath + dependencyDirectory + "CopyPdfFromIeWindowsCache.bat";
            string downloadPdf = ProjectPath + downloadDirectory + filename;

            downloadPdf = downloadPdf.Replace("file:///", "");

            var psi = new ProcessStartInfo();

            //To hide the dos-style black window that the command prompt usually shows
            psi.CreateNoWindow = true;
            psi.FileName = batFilePath;

            //To Run the commands as administrator
            psi.Verb = "runas";
            psi.Arguments = downloadPdf;
            try
            {
                var process = new Process();
                process.StartInfo = psi;
                process.StartInfo.UseShellExecute = true;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                LogHelper.LogException(e);
                throw e;
            }
            return downloadPdf;
        }

        /// <summary>
        /// This will verify specific text in PDF
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="pattern"></param>
        public static void VerifySpecificTextInPDF(string filepath, string pattern)
        {
            // If using Professional version, put your serial key below.
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            DocumentModel document = DocumentModel.Load(filepath);

            StringBuilder sb = new StringBuilder();

            // Read PDF file's document properties.
            sb.AppendFormat("Author: {0}", document.DocumentProperties.BuiltIn[BuiltInDocumentProperty.Author]).AppendLine();
            sb.AppendFormat("DateContentCreated: {0}", document.DocumentProperties.BuiltIn[BuiltInDocumentProperty.DateLastSaved]).AppendLine();

            // Sample's input parameter.
            Regex regex = new Regex(pattern);

            int row = 0;
            StringBuilder line = new StringBuilder();

            // Read PDF file's text content and match a specified regular expression.
            foreach (Match match in regex.Matches(document.Content.ToString()))
            {
                line.Length = 0;
                line.AppendFormat("Result: {0}: ", ++row);

                // Either write only successfully matched named groups or entire match.
                bool hasAny = false;
                for (int i = 0; i < match.Groups.Count; ++i)
                {
                    string groupName = regex.GroupNameFromNumber(i);
                    Group matchGroup = match.Groups[i];
                    if (matchGroup.Success && groupName != i.ToString())
                    {
                        line.AppendFormat("{0}= {1}, ", groupName, matchGroup.Value);
                        hasAny = true;
                    }
                }

                if (hasAny)
                    line.Length -= 2;
                else
                    line.Append(match.Value);

                sb.AppendLine(line.ToString());
            }

            Console.WriteLine(sb.ToString());
        }


        /// <summary>
        /// This function will set download location in Registry
        /// Pass your required folder location as path from Setting class, i.e., Settings.DownloadLocation
        /// </summary>
        /// <param name="path"></param>
        public static void SetRegistryValue(String path)
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + path;
            string localpath = new Uri(finalpth).LocalPath;
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main", true);
            if (rkey != null)
            {
                rkey.SetValue("Default Download Directory", localpath, RegistryValueKind.String);
            }
        }
    }
}
