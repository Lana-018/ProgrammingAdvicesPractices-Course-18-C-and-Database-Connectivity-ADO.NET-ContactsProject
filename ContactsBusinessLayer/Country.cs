using ContactsDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBusinessLayer
{
    public class clsCountry
    {
        enum enMode { AddNew=1,Update=2}
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string PhoneCode { get; set; }

        enMode Mode= enMode.AddNew;
        public clsCountry()
        {
            Mode = enMode.AddNew;
            CountryID = -1;
            CountryName = "";
            Code = "";
            PhoneCode = "";
        }
        private clsCountry(int ID, string CountryName, string Code, string PhoneCode)
        {
            Mode = enMode.Update;
            this.CountryID = ID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;
        }

        static public clsCountry GetCountryByID(int ID)
        {
            string CountryName = "";
            string Code = "";
            string PhoneCode = "";
            if(clsCountriesDataAccess.GetCountryByID(ID, ref CountryName,ref Code,ref PhoneCode))
                return new clsCountry(ID, CountryName,Code,PhoneCode);
            return null;
        }
        static public clsCountry GetCountryByName(string CountryName)
        {
            int CountryID = -1;
            string Code = "";
            string PhoneCode = "";
            if (clsCountriesDataAccess.GetCountryByName(ref CountryID,CountryName,ref Code,ref PhoneCode))
                return new clsCountry(CountryID, CountryName, Code, PhoneCode);
            return null;
        }
        static public bool IsCountryExist(string CountryName)
        {
            return clsCountriesDataAccess.IsCountryExist(CountryName);
        }
        static public bool IsCountryExist(int ID)
        {
            return clsCountriesDataAccess.IsCountryExist(ID);
        }
        private bool _AddNewCountry()
        {
            if (IsCountryExist(this.CountryName))
                return false;
            this.CountryID=clsCountriesDataAccess.AddNewCountry(this.CountryName,this.Code,this.PhoneCode);
            return this.CountryID != -1;
        }

        private bool _UpdateCountryName()
        {
            return (clsCountriesDataAccess.UpdateCountryName(this.CountryID, this.CountryName,this.Code,this.PhoneCode) > 0);
        }

        static public bool DeleteCountry(int ID)
        {
            return clsCountriesDataAccess.DeleteCountry(ID) > 0;
        }
        static public bool DeleteCountry(string CountryName)
        {
            return clsCountriesDataAccess.DeleteCountry(CountryName) > 0;
        }

        static public DataTable ListCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }
         public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;
                case enMode.Update:
                    return _UpdateCountryName();
                        
                    
            }
            return false;
        }
    }
}
