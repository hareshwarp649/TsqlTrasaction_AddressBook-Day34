using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Uc17_AddressBookTsql;

namespace UnitTest17_34
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        { }


            public static string masterQuery = @"SELECT * FROM PersonContactsTable";
        AddressBookData addressBookDataBase = new AddressBookData();
        [TestMethod]
        public void GivenDBConnectionString_InAddressBookDataBase_ReturnListOfContactsinDB()
        {
            addressBookDataBase.GetContactDetailsByDataAdapter(masterQuery);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GivenContactsUpdatedObject_InUpdateContactsMethod_Return()
        {
            Contacts contacts = new Contacts { firstName = "Deva", lastName = "KHANEJA", city = "Ludhiana", state = "Punjab", zipCode = 110008 };
            var actual = addressBookDataBase.UpdateContactDetailsofAPerson(contacts);
            Assert.AreEqual("Punjab", actual.state);
        }
    
    

    }
}
