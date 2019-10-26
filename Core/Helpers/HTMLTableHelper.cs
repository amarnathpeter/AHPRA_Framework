using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{

    public static class HtmlTableHelper
    {
        private static List<HtmlTableDataCollection> _tableDatacollections;


        public static List<HtmlTableDataCollection> TableData
        {
            get
            {
                return _tableDatacollections;
            }
        }

        /// <summary>
        /// This will read data from table on web page
        /// </summary>
        /// <param name="table"></param>
        public static void ReadTable(IWebElement table)
        {
            try
            { //Initialize the table
                _tableDatacollections = new List<HtmlTableDataCollection>();

                //Get all the columns from the table
                var columns = table.FindElements(By.TagName("th"));

                //Get all the rows
                var rows = table.FindElements(By.TagName("tr"));

                //Create row index
                int rowIndex = 0;
                foreach (var row in rows)
                {
                    int colIndex = 0;

                    var colDatas = row.FindElements(By.TagName("td"));
                    //Store data only if it has value in row
                    if (colDatas.Count != 0)
                        foreach (var colValue in colDatas)
                        {
                            _tableDatacollections.Add(new HtmlTableDataCollection
                            {
                                RowNumber = rowIndex,
                                ColumnName = columns[colIndex].Text != "" ?
                                             columns[colIndex].Text : colIndex.ToString(),
                                ColumnValue = colValue.Text,
                                ColumnSpecialValues = GetControl(colValue)
                            });

                            //Move to next column
                            colIndex++;
                        }
                    rowIndex++;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        /// <summary>
        /// This will return column value of web  table
        /// </summary>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        private static ColumnSpecialValue GetControl(IWebElement columnValue)
        {
            ColumnSpecialValue columnSpecialValue = null;
            //Check if the control has specfic tags like input/hyperlink etc
            if (columnValue.FindElements(By.TagName("a")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = columnValue.FindElements(By.TagName("a")),
                    ControlType = "hyperLink"
                };
            }
            if (columnValue.FindElements(By.TagName("input")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = columnValue.FindElements(By.TagName("input")),
                    ControlType = "input"
                };
            }

            return columnSpecialValue;
        }

        /// <summary>
        /// This will perform click operation on cell value
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="refColumnName"></param>
        /// <param name="refColumnValue"></param>
        /// <param name="controlToOperate"></param>
        public static void PerformActionOnCell(string columnIndex, string refColumnName, string refColumnValue, string controlToOperate = null)
        {
            foreach (int rowNumber in GetDynamicRowNumber(refColumnName, refColumnValue))
            {
                var cell = (from e in _tableDatacollections
                            where e.ColumnName == columnIndex && e.RowNumber == rowNumber
                            select e.ColumnSpecialValues).SingleOrDefault();

                //Need to operate on those controls
                if (controlToOperate != null && cell != null)
                {
                    //Since based on the control type, the retriving of text changes
                    //created this kind of control
                    if (cell.ControlType == "hyperLink")
                    {
                        var returnedControl = (from c in cell.ElementCollection
                                               where c.Text == controlToOperate
                                               select c).SingleOrDefault();

                        returnedControl?.Click();
                    }
                    if (cell.ControlType == "input")
                    {
                        var returnedControl = (from c in cell.ElementCollection
                                               where c.GetAttribute("value") == controlToOperate
                                               select c).SingleOrDefault();

                        returnedControl?.Click();
                    }

                }
                else
                {
                    cell.ElementCollection?.First().Click();
                }
            }
        }

        /// <summary>
        /// This will return number of rows in table
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <returns></returns>
        private static IEnumerable GetDynamicRowNumber(string columnName, string columnValue)
        {
            //dynamic row
            foreach (var table in _tableDatacollections)
            {
                if (table.ColumnName == columnName && table.ColumnValue == columnValue)
                    yield return table.RowNumber;
            }
        }

        /// <summary>
        /// This will perform hover on column header
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static string PerformHoveronColHeader(IWebElement table, int columnIndex)
        {
            //Get all the columns
            var colDatas = table.FindElements(By.TagName("td"));
            IWebElement obj = colDatas[columnIndex];
            var value = obj.GetAttribute("title");
            return value;
        }

        /// <summary>
        /// This will perform hover and click on column header
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colName"></param>
        public static void PerformHoverClickonHeader(IWebElement table, string colName)
        {
            var headerDatas = table.FindElements(By.TagName("th"));
            IWebElement header = headerDatas.Where(x => x.Text == colName).FirstOrDefault();
            header.Click();
        }

        /// <summary>
        /// this will get column value
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        public static List<string> GetColumnValues(string colName)
        {
            List<string> originalData
             = (from e in _tableDatacollections
                where e.ColumnName == colName
                select e.ColumnValue.ToString()).ToList();

            return originalData;
        }

        /// <summary>
        /// This will assert column in assending order
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="formatter"></param>
        public static void AssertColumnInAscending(string colName, int formatter = 1)
        {
            List<string> originalData = GetColumnValues(colName);
            List<string> sortedData = originalData.ToList();
            if (formatter == 1)
            {
                sortedData.Sort((a, b) => a.CompareTo(b));
            }
            else if (formatter == 2)
            {
                sortedData.OrderBy(x => DateTime.Parse(x));
            }
            if (originalData.SequenceEqual(sortedData))
            {
                LogHelper.Write("column sorted in Ascending order");
            }
            else
            {
                LogHelper.Write("column is not sorted in Ascending order");
                Assert.Fail("AssertElementNotSortedAscending");
            }
        }

        /// <summary>
        /// This will assert column in descending order
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="formatter"></param>
        public static void AssertColumnInDescending(string colName, int formatter = 1)
        {
            List<string> originalData = GetColumnValues(colName);
            List<string> sortedData = originalData.ToList();
            if (formatter == 1)
            {
                sortedData.Sort((a, b) => -1 * a.CompareTo(b));
            }
            else if (formatter == 2)
            {
                sortedData.OrderByDescending(x => DateTime.Parse(x));
            }
            if (originalData.SequenceEqual(sortedData))
            {
                LogHelper.Write("column sorted in Descending order");
            }
            else
            {
                LogHelper.Write("column is not sorted in AssertColumnInDescending order");
                Assert.Fail("AssertElementNotSortedDescending");
            }
        }

        /// <summary>
        /// This will assert Alias description in table
        /// </summary>
        /// <param name="content"></param>
        /// <param name="logHelper"></param>
        public static void AssertAliasDescription(string content, string logHelper)
        {
            int resultCount = (from t in _tableDatacollections
                               where t.ColumnValue.Contains(content)
                               select t.ColumnName).Count();

            if (resultCount == 1)
            {
                LogHelper.Write(logHelper);
            }
            else
            {
                Assert.Fail("Alias/Descripton not visible");
            }
        }

        /// <summary>
        /// This will assert sequence of element
        /// </summary>
        /// <param name="divSmoking"></param>
        /// <param name="divProcedures"></param>
        public static void AssertSequence(IWebElement divSmoking, IWebElement divProcedures)
        {
            try
            {
                int smokingY = divSmoking.Location.Y;
                int proceduresY = divProcedures.Location.Y;
                Assert.Greater(proceduresY, smokingY);
            }
            catch (AssertionException)
            {
                Assert.Fail("Procedures App is not located below Smoking Status");
            }
        }

        public static void AssertTableDataComparison(List<ExcelTableDataCollection> expected)
        {
            try
            {
                for (int i = 0; i < _tableDatacollections.Count; i++)
                {
                    Assert.AreEqual(_tableDatacollections[i].ColumnName, expected[i + 2].colName);
                    Assert.AreEqual(_tableDatacollections[i].ColumnValue, expected[i + 2].colValue);

                }
            }
            catch (AssertionException)
            {
                LogHelper.Write("Data comparision fail");
                Assert.Fail("DataComparisonFail");
            }
        }

        public static void AssertTableDataComparison(List<HtmlTableDataCollection> expected)
        {
            try
            {
                for (int i = 0; i < _tableDatacollections.Count; i++)
                {
                    Assert.AreEqual(expected[i].ColumnName, _tableDatacollections[i].ColumnName);
                    Assert.AreEqual(expected[i].ColumnValue, _tableDatacollections[i].ColumnValue);
                    LogHelper.Write("Data comparision success");
                }
            }
            catch (AssertionException)
            {
                LogHelper.Write("Data comparision fail");
                Assert.Fail("DataComparisonFail");
            }
        }
    }

    [Serializable]
    public class HtmlTableDataCollection
    {
        public int RowNumber { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public ColumnSpecialValue ColumnSpecialValues { get; set; }
    }

    [Serializable]
    public class ColumnSpecialValue
    {
        public IEnumerable<IWebElement> ElementCollection { get; set; }
        public string ControlType { get; set; }
    }
}
