using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc_19AddBookTsql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book ADO.Net!");
            AddressBookRepo repo = new AddressBookRepo();
            //UC 20
            AddressBookData model = new AddressBookData();

           // UC 16
            repo.GetDetails();

          //  UC 17
            repo.Update(model);

           // UC - 18 Alter Table Call This method First while Commenting GetDatRange method and vise versa
            repo.Alter(model);

          //  UC - 18 Get Range
            repo.GetDateRange(model);

          //  UC 19
            repo.Count(model);

            //UC 22 - CSv
            ReadWriteTableCsv rwcsv = new ReadWriteTableCsv();
            rwcsv.GetCSv();
            Console.WriteLine("\nCompleted Reading and Writing\n");
            Console.WriteLine("\nReading Data From that Csv File\n");
            rwcsv.ReadallLines();

            //UC 22 - JSON
            ReadWriteTableJSON rwjson = new ReadWriteTableJSON();
            rwjson.GetJson();
            Console.WriteLine("Read and Write it Completed to Json File");



        }
    }
    
}
