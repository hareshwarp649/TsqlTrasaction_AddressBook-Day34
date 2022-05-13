using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc_19AddBookTsql
{
    internal class ReadWriteTableCsv
    {
        public string GetCSv()
        {
            using (SqlConnection connect = new SqlConnection(GetConnection()))
            {
                connect.Open();
                return CreateCsv(new SqlCommand("Select * From Address_Book_Table", connect).ExecuteReader());
            }
        }
        private string CreateCsv(IDataReader reader)
        {
            string file = @"D:\.net\Day 34 Address Book\Day34AddressBook\TableData_TO_CSVFile.csv";
            List<string> lines = new List<string>();
            string headerLine = "";
            if (reader.Read())
            {
                string[] columns = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columns[i] = reader.GetName(i);
                }
                headerLine = string.Join(",", columns);
                lines.Add(headerLine);

            }
            //data
            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                lines.Add(string.Join(",", values));
            }
            //create file
            File.WriteAllLines(file, lines);

            return file;
        }
        private string GetConnection()
        {
            return @"Data Source=localhost\SQLEXPRESS;Database=Address_Book;Trusted_Connection=True";
        }
        public void ReadallLines()
        {
            string path = @"D:\.net\Day 34 Address Book\Day34AddressBook\TableData_TO_CSVFile.csv";
            string[] lines;
            lines = File.ReadAllLines(path);
            foreach (var l in lines)
            {
                Console.WriteLine(l);
            }
        }

    }
}
