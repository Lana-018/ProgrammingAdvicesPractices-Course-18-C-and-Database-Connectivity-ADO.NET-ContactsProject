using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsProject
{
    public partial class frmMainContactsForm : Form
    {
        public frmMainContactsForm()
        {
            InitializeComponent();
        }
        void _FillComboboxByColumnsName()
        {
            DataTable dt = clsContact.GettAllContacts();
            foreach(DataColumn cl in dt.Columns )
            {
                cmbxColumnToSortedBy.Items.Add(cl.ColumnName);
            }
        }
        private void _RefreshContactsList()
        {
            dgvAllContacts.DataSource = clsContact.GettAllContacts();
            _FillComboboxByColumnsName();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditContact AddEditContact = new frmAddEditContact((int)dgvAllContacts.CurrentRow.Cells["ContactID"].Value);
            AddEditContact.ShowDialog();
            _RefreshContactsList();
        }

        private void deleteContactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAllContacts.CurrentRow.Cells[0].Value;
            DialogResult result = MessageBox.Show($"Are you sure to delete contact with ID={ID}?", "Delete contact", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes) 
            {
                if (clsContact.DeleteContact(ID))
                {
                    MessageBox.Show("Contact deleted successfully");
                    _RefreshContactsList();
                }
                else MessageBox.Show("Contact is not deleted");
            }
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            frmAddEditContact form = new frmAddEditContact(-1);
            form.ShowDialog();
            _RefreshContactsList();
        }

        private void btnApplyDataFilter_Click(object sender, EventArgs e)
        {
            DataTable dt = clsContact.GettAllContacts();
            DataView dv = dt.DefaultView;
            try 
            {
                dv.RowFilter = txtFilterData.Text;
                dgvAllContacts.DataSource = dv;

            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "Invalid filter expression.\nUse: ColumnName Operator Value\nExample: Age > 20");
            }
        }

        private void rbAsc_CheckedChanged(object sender, EventArgs e)
        {
            dgvAllContacts.Sort(dgvAllContacts.Columns[cmbxColumnToSortedBy.SelectedIndex],
            ListSortDirection.Ascending);
        }

        private void rbDesc_CheckedChanged(object sender, EventArgs e)
        {
            dgvAllContacts.Sort(dgvAllContacts.Columns[cmbxColumnToSortedBy.SelectedIndex],
            ListSortDirection.Descending);
        }

        private void cmbxColumnToSortedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbAsc.Checked = false;
            rbDesc.Checked = false;
        }
    }
}
