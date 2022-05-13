using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Uc_18_AddressBookTsql;
using System.Collections.Generic;
//using Microsoft.Azure.KeyVault.Models;

namespace UnitTest18_34
{
    [TestClass]


    public class AddressBookUnitTets
    {
        public static string masterQuery = @"SELECT * FROM PersonContactsTable";
        public static string dateQuery = @"SELECT * FROM PersonContactsTable WHERE Date_Added BETWEEN CAST('2020-05-10' AS DATE) AND GETDATE()";
        AddressBookData addressBookDataBase = new AddressBookData();
        [TestMethod]
        public void GivenDBConnectionString_InAddressBookDataBase_ReturnListOfContactsinDB()
        {
            string[] expectedNamesinDB = { "Deva", "Sonu", "Nami", "Geeta", "BUNNY" };
            string[] namesRetrived = { "", "", "", "", "" };
            List<Contacts> checkingContacts = addressBookDataBase.GetContactsListByDataAdapter(masterQuery);
            int i = 0;
            foreach (Contacts contact in checkingContacts)
            {
                namesRetrived[i] = contact.firstName;
                Assert.AreEqual(namesRetrived[i], expectedNamesinDB[i]);
                i++;
            }
        }

        [TestMethod]
        public void GivenContactsUpdatedObject_InUpdateContactsMethod_ReturnListOfUpdatedContact()
        {
            Contacts contacts = new Contacts { firstName = "Deva", lastName = "Pal", city = "Agra", state = "U.P", zipCode = 124113 };
            addressBookDataBase.UpdateContactDetailsofAPerson(contacts);
            List<Contacts> updatedListOfContacts = addressBookDataBase.GetContactsListByDataAdapterFromDB(masterQuery);
            foreach (Contacts contact in updatedListOfContacts)
            {
                if (contact.firstName == contacts.firstName && contact.lastName == contacts.lastName)
                {
                    Assert.AreEqual(contacts.city, contact.city);
                }
            }
        }
        [TestMethod]
        public void GivenQueryForDateRange_InGetContactsMethod_ReturnListOfContactsBetweenParticularDates()
        {
            string[] expectedContactsPersonNames = { "Geeta", "Lala", "BUNNY" };
            List<Contacts> listOfContactsForParticularDateRange = addressBookDataBase.GetContactsListByDataAdapterFromDB(dateQuery);
            int i = 0;
            foreach (Contacts contact in listOfContactsForParticularDateRange)
            {
                Assert.AreEqual(expectedContactsPersonNames[i], contact.firstName);
                i++;
            }
        }
    }

}
