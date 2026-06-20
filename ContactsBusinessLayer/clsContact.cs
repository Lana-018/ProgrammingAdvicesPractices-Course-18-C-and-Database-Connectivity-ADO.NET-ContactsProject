using ContactsDataAccessLayer;
using System;
using System.Data;
using System.Runtime.CompilerServices;
namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public int CountryID { get; set; }

        enum enMode { AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;
        public clsContact()
        {
            this.ContactID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";
            Mode = enMode.AddNew;
        }
        private clsContact(int ID, string FirstName,
           string LastName, string Email, string Phone,
           string Address, DateTime DateOfBirth,
           int CountryID, string ImagePath)
        {
            this.ContactID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            Mode = enMode.Update;
        }
        static public clsContact Find(int ID)
        {
            int CountryID = -1;
            string FirstName = "", LastName = "", Phone = ""
                , Address = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            if (clsContactsDataAccess.GetContactByID(ID,
            ref FirstName, ref LastName, ref Email, ref Phone,
            ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))
                return new clsContact(ID,
                FirstName, LastName, Email, Phone,
                 Address, DateOfBirth, CountryID, ImagePath);

            else return null;
        }
        bool _AddNewContact()
        {
            this.ContactID = clsContactsDataAccess.AddNewContact(this.FirstName,
                 this.LastName, this.Email, this.Phone, this.Address,
                 this.DateOfBirth, this.CountryID, this.ImagePath);
            if (this.ContactID == -1)
                return false;
            else return true;
        }
        bool _UpdateConatct()
        {
            return clsContactsDataAccess.UpdateContact(this.ContactID,
                 this.FirstName, this.LastName, this.Email, this.Phone,
                 this.Address, this.DateOfBirth, this.CountryID,
                 this.ImagePath);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateConatct();
            }
            return false;
        }
        static public bool DeleteContact(int ID)
        {
            return clsContactsDataAccess.DeleteContact(ID);
        }
        static public DataTable GettAllContacts()
        {
            return clsContactsDataAccess.GetAllContacts();
        }
        static public bool IsContactExist(int ID)
        {
            return clsContactsDataAccess.IsContactExist(ID);
        }
    }
   
}
