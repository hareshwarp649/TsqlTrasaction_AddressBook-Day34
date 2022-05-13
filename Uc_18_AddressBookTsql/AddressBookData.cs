using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc_18_AddressBookTsql
{
    public class AddressBookData
    {
        #region DB Connection String
        public static string connectionString = @"Data Source=DESKTOP-C7TGR0I;Initial Catalog=Address_Book_Service;Integrated Security=True  ";
        #endregion

        SqlConnection sqlConnection = new SqlConnection(connectionString);

        #region Get All the Contacts From Database to Console
        public void GetContactDetailsByDataAdapter(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection connection;
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(ds);
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        Console.WriteLine(dataRow["FirstName"] + "," + dataRow["LastName"] + "," + dataRow["AddressDetails"] + "," + dataRow["City"] + "," + dataRow["StateName"] + "," + dataRow["Zip"] + "," + dataRow["PhoneNo"] + "," + dataRow["Email"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Update Contact Details for a person & retrieve data in Object Form from Database
        public Contacts UpdateContactDetailsofAPerson(Contacts contacts)
        {
            try
            {
                SqlCommand command = new SqlCommand("spUpdateContacts", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@FirstName", contacts.firstName);
                command.Parameters.AddWithValue("@LastName", contacts.lastName);
                command.Parameters.AddWithValue("@City", contacts.city);
                command.Parameters.AddWithValue("@State", contacts.state);
                command.Parameters.AddWithValue("@Zip", contacts.zipCode);
                sqlConnection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Records updated successfully");
                    Contacts updatedContact = RetrieveDataFromDBInObjectForm(connectionString, contacts);
                    return updatedContact;
                }
                else
                {
                    Console.WriteLine("Record not updated successfully");
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
        #endregion

        public Contacts RetrieveDataFromDBInObjectForm(string connectionstring, Contacts passingContact)
        {
            SqlConnection connection;
            SqlDataReader sqlDataReader = default;
            try
            {
                Contacts contacts = new Contacts();
                using (connection = new SqlConnection(connectionstring))
                {
                    string query = @"SELECT * FROM PersonContactsTable";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    connection.Open();
                    sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            if (sqlDataReader.GetString(1) == passingContact.firstName && sqlDataReader.GetString(2) == passingContact.lastName)
                            {
                                contacts.firstName = sqlDataReader.GetString(1);
                                contacts.lastName = sqlDataReader.GetString(2);
                                contacts.address = sqlDataReader.GetString(3);
                                contacts.city = sqlDataReader.GetString(4);
                                contacts.state = sqlDataReader.GetString(5);
                                contacts.zipCode = sqlDataReader.GetInt32(6);
                                contacts.phoneNo = sqlDataReader.GetInt64(7);
                                contacts.email = sqlDataReader.GetString(8);
                                break;
                            }
                        }
                    }
                    sqlDataReader.Close();
                    return contacts;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
