using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc_19AddBookTsql
{
    internal class AddressBookRepo
    {

        public static string connectionString = @"Data Source=localhost\SQLEXPRESS;Database=Address_Book;Trusted_Connection=True";
        SqlConnection connection = new SqlConnection(connectionString);

        //UC 16 - Retrieve Records From DataBase
        public void GetDetails()
        {
            try
            {
                AddressBookData addressBookModel = new AddressBookData();
                using (this.connection)
                {


                    string query = @"SELECT * FROM Address_Book_Table";

                    //Define Sql Command Object
                    SqlCommand cmd = new SqlCommand(query, this.connection);

                    this.connection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            addressBookModel.FirstName = dr.GetString(0);
                            addressBookModel.LastName = dr.GetString(1);
                            addressBookModel._address = dr.GetString(2);
                            addressBookModel.City = dr.GetString(3);
                            addressBookModel._State = dr.GetString(4);
                            addressBookModel.Zip = dr.GetInt32(5);
                            addressBookModel.PhoneNumber = dr.GetString(6);
                            addressBookModel.email = dr.GetString(7);
                            addressBookModel.RElationType = dr.GetString(8);


                            //display retieved record

                            Console.WriteLine("FirstName : " + "{0}" + ", Last Name : " + "{1}" + ", Address : " + "{2}" + ", City : " + "{3}" + ", State" + "{4}" + ", Zip : " + "{5}" + ", PhoneNumber : " + "{6}" + ", Email : " + "{7}" + ", Relation Type : " + "{8}", addressBookModel.FirstName, addressBookModel.LastName, addressBookModel._address, addressBookModel.City, addressBookModel._State, addressBookModel.Zip, addressBookModel.PhoneNumber, addressBookModel.email, addressBookModel.RElationType);

                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    //Close Data Reader
                    dr.Close();
                    this.connection.Close();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        //UC 17 - Update
        public void Update(AddressBookData addressBookModel)
        {
            string query = @"Update Address_Book_Table Set StartDate ='01.01-2000' Where FirstName='Manoj'";

            SqlCommand cmd = new SqlCommand(query, this.connection);
            this.connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            AddressBookData addressBook = new AddressBookData();
            Console.WriteLine("Update Successfull");
            dr.Close();
            this.connection.Close();

            string query1 = @"Update Address_Book_Table Set StartDate ='01.01-1995' Where FirstName='Manu'";
            SqlCommand cmd1 = new SqlCommand(query1, this.connection);
            this.connection.Open();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            AddressBookData addressBook1 = new AddressBookData();
            Console.WriteLine("Update Successfull");
            dr1.Close();
            this.connection.Close();

        }
        //UC 18 - Alter 
        public void Alter(AddressBookData model)
        {
            string query = @"Alter Table Address_Book_Table Add StartDate datetime default GetDate() Not Null";
            SqlCommand cmd = new SqlCommand(query, this.connection);
            this.connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Console.WriteLine("Alter Successfull Added StartDate Column");
            dr.Close();
            this.connection.Close();

        }
        //UC 18 Get Date
        public void GetDateRange(AddressBookData model)
        {
            string query = @"Select * From Address_Book_Table Where StartDate Between '01-01-2000' And GetDate()";
            SqlCommand cmd = new SqlCommand(query, this.connection);
            this.connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            AddressBookData addressBookModel = new AddressBookData();
            Console.WriteLine("Records in Data Range\n");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    addressBookModel.FirstName = dr.GetString(0);
                    addressBookModel.LastName = dr.GetString(1);
                    addressBookModel._address = dr.GetString(2);
                    addressBookModel.City = dr.GetString(3);
                    addressBookModel._State = dr.GetString(4);
                    addressBookModel.Zip = dr.GetInt32(5);
                    addressBookModel.PhoneNumber = dr.GetString(6);
                    addressBookModel.email = dr.GetString(7);
                    addressBookModel.RElationType = dr.GetString(8);
                    addressBookModel.StartDate = dr.GetDateTime(9);

                    Console.WriteLine("\nFirstName : " + "{0}" + ", Last Name : " + "{1}" + ", Address : " + "{2}" + ", City : " + "{3}" + ", State" + "{4}" + ", Zip : " + "{5}" + ", PhoneNumber : " + "{6}" + ", Email : " + "{7}" + ", Relation Type : " + "{8}" + ", Start Date : " + "{9}", addressBookModel.FirstName, addressBookModel.LastName, addressBookModel._address, addressBookModel.City, addressBookModel._State, addressBookModel.Zip, addressBookModel.PhoneNumber, addressBookModel.email, addressBookModel.RElationType, addressBookModel.StartDate);

                }
            }
            dr.Close();
            this.connection.Close();

        }
        //UC 19
        public void Count(AddressBookData model)
        {
            string query = @"Select Count(City) From Address_Book_Table Where City='Kazipet'";
            SqlCommand cmd = new SqlCommand(query, this.connection);
            this.connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int count = dr.GetInt32(0);
                    Console.WriteLine("Count of City Kazipet : " + "{0}", count);
                }
            }
            dr.Close();
            this.connection.Close();

        }
        //UC 20
        public void AddDetials(AddressBookData model1)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("SPAddressBook", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", model1.FirstName);
                    command.Parameters.AddWithValue("@LastName", model1.LastName);
                    command.Parameters.AddWithValue("@_address", model1._address);
                    command.Parameters.AddWithValue("@City", model1.City);
                    command.Parameters.AddWithValue("@_State", model1._State);
                    command.Parameters.AddWithValue("@Zip", model1.Zip);
                    command.Parameters.AddWithValue("@PhoneNumber", model1.PhoneNumber);
                    command.Parameters.AddWithValue("@email", model1.email);
                    command.Parameters.AddWithValue("@RElationType", model1.RElationType);
                    command.Parameters.AddWithValue("@StartDate", model1.StartDate);

                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        AddressBookRepo repo = new AddressBookRepo();
                        repo.GetDetails();

                    }
                }
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
