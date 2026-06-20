using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsProject
{
    public partial class frmAddEditContact : Form
    {
        int _ContactID = -1;
        clsContact _Contact;
        enum enMode{AddNew=1,Update= 2 }
        enMode _Mode= enMode.AddNew;
        public frmAddEditContact(int ContactID)
        {
            InitializeComponent();

            _ContactID = ContactID;
            if (_ContactID==-1)
                _Mode = enMode.AddNew;
            else _Mode = enMode.Update;
        }
        private void _FillCountriesInCombobox()
        {
            DataTable dt = clsCountry.ListCountries();
            foreach (DataRow Row in dt.Rows)
            {
                cmbxCountries.Items.Add(Row["CountryName"]);
            }
        }
        private void _LoadData()
        {
            _FillCountriesInCombobox();
            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New Contact";
                _Contact = new clsContact();
                llRemoveImage.Visible = false;
                lbContactID.Text = "";
                return;
            }
            _Contact = clsContact.Find(_ContactID);
            if (_Contact==null)
            {
                MessageBox.Show($"This form will be closed because no contact with ID={_ContactID} is available");
                this.Close();
                return;
            }
            lbTitle.Text ="Update contact with ID="+ _ContactID;
            lbContactID.Text = _Contact.ContactID.ToString();
            txtFirstName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtPhone.Text = _Contact.Phone;
            txtAddress.Text = _Contact.Address;
            dtpDateOfBirth.Value= _Contact.DateOfBirth;
            if(_Contact.ImagePath!="")
            {
                pbxImagePath.Load(_Contact.ImagePath);
            }
            llRemoveImage.Visible = (_Contact.ImagePath != null && _Contact.ImagePath!="");
            cmbxCountries.SelectedIndex = cmbxCountries.FindString(clsCountry.GetCountryByID(_Contact.CountryID).CountryName);

        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            _Contact.FirstName = txtFirstName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Phone = txtPhone.Text;
            _Contact.Address = txtAddress.Text;
            _Contact.DateOfBirth = dtpDateOfBirth.Value;
            if (pbxImagePath.ImageLocation != null)
                _Contact.ImagePath = pbxImagePath.ImageLocation;
            else _Contact.ImagePath = "";
            _Contact.CountryID = clsCountry.GetCountryByName(cmbxCountries.Text).CountryID;
            if (_Contact.Save())
            {
                switch(_Mode)
                {
                    case enMode.AddNew:
                        MessageBox.Show("Contact added successfully :-)");
                        break;
                    case enMode.Update:
                        MessageBox.Show("Contact updated successfully :-)");
                        break;
                }

            }
            else MessageBox.Show("Operation failed!");

            _Mode = enMode.Update;
            lbTitle.Text = $"Edit contact with ID={_Contact.ContactID}";
            lbContactID.Text = _Contact.ContactID.ToString();
            if (pbxImagePath.Image != null)
                llRemoveImage.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;

                pbxImagePath.Load(selectedFilePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbxImagePath.ImageLocation = null;
            _Contact.ImagePath = "";
            llRemoveImage.Visible=false;
        }
    }
}
