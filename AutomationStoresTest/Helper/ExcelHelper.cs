using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace AutomationStoresTest.Helper
{
    public class ExcelHelper
    {


        public DataTable ConvertExcelToDataTable(string fileName, string sheetName, bool UseFirstRowAsHeader = true)
        {
            Console.WriteLine("File Path = fileName");
            //open file and returns as Stream
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = UseFirstRowAsHeader
                }
            });

            // DataSet result = excelReader.AsDataSet();
            //Get all the Tables
            DataTableCollection table = result.Tables;

            //Store it in DataTable
            DataTable resultTable = table[sheetName];
            //return
            stream.Close();
            return resultTable;

        }
    }
}

