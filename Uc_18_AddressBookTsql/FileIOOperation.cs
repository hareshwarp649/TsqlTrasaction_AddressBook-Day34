using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc_18_AddressBookTsql
{
    public class FileIOOperation
    {
        #region FilePath to and from which File to be Write & Read
        public static string path = @"C:\Users\ADMIN\Desktop\MsSqlAssignment\day34-TSQL\AddressBookTsql\Uc_18_AddressBookTsql\TextFile34.txt";
        public static string csvPath = @"C:\Users\ADMIN\Desktop\MsSqlAssignment\day34-TSQL\AddressBookTsql\Uc_18_AddressBookTsql\AaressBook34.csv";
        public static string jsonPath = @"C:\Users\ADMIN\Desktop\MsSqlAssignment\day34-TSQL\AddressBookTsql\Uc_18_AddressBookTsql\MsAddBook.json";
        #endregion

        #region Writing Person Details in AddressBook.txt File
        public static void WritingAllPersonContactsinFile(IDictionary<string, Book> addressBook)
        {
            File.WriteAllText(path, string.Empty);
            foreach (KeyValuePair<string, Book> book in addressBook)
            {
                File.AppendAllText(path, $"Address Book Name : {book.Key} {Environment.NewLine}");
                foreach (Contacts contacts in book.Value.listOfContacts)
                {
                    File.AppendAllText(path, contacts.ToString());
                    File.AppendAllText(path, Environment.NewLine);
                }
                File.AppendAllText(path, Environment.NewLine);
            }
            Console.WriteLine("Content has written to AdrressBook.txt file");
        }
        #endregion

        #region Reading File from AddressBook.txt file
        public static void ReadingAllPersonContactsinFile()
        {
            string lines = File.ReadAllText(path);
            Console.WriteLine("Reading files from AdrressBook.txt file");
            Console.WriteLine(lines);
        }
        #endregion

        #region Writing to AddressBook1.csv File
        public static void WritigAllPersonContactsinCSVFile(IDictionary<string, Book> addressBook)
        {
            File.WriteAllText(csvPath, string.Empty);
            var stream = File.Open(csvPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(stream);
            CsvWriter csvWriter = new CsvWriter(sw, CultureInfo.InvariantCulture);
            foreach (var kvp in addressBook)
            {
                foreach (var contact in kvp.Value.listOfContacts)
                {
                    List<Contacts> listToCSV = new List<Contacts>();
                    listToCSV.Add(contact);
                    csvWriter.WriteRecords(listToCSV);
                }
            }
            csvWriter.Flush();
            sw.Close();
        }
        #endregion

        #region Reading from CSV File
        public static void ReadingAllPersonContactsfromCSVFile()
        {
            using (StreamReader streamreader = new StreamReader(csvPath))
            using (CsvReader csvReader = new CsvReader(streamreader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<Contacts>().ToList();
                foreach (Contacts contact in records)
                {
                    Console.WriteLine(contact.ToString());
                }
            }
        }
        #endregion

        #region Writing to AddressBook.json File
        public static void WritigAllPersonContactsinJsonFile(IDictionary<string, Book> addressBook)
        {
            File.WriteAllText(jsonPath, string.Empty);
            foreach (var book in addressBook)
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                var stream = File.Open(jsonPath, FileMode.Append);
                var streamwriter = new StreamWriter(stream);
                var jsonwriter = new JsonTextWriter(streamwriter);
                foreach (Contacts contact in book.Value.listOfContacts)
                {
                    jsonSerializer.Serialize(jsonwriter, contact);
                }
                jsonwriter.Close();
                streamwriter.Close();
            }
        }
        #endregion

        #region For reading from json File
        public static void ReadingAllPersonContactsFromJsonFile()
        {
            using (StreamReader streamreader = new StreamReader(jsonPath))
            {
                string json = streamreader.ReadToEnd();
                dynamic jsonarray = JsonConvert.DeserializeObject(json);
                foreach (Contacts contacts in jsonarray)
                {
                    Console.WriteLine(contacts.ToString());
                }
            }
        }
        #endregion
    }
}
