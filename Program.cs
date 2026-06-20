using ContactsBusinessLayer;
using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
namespace ContactsProject
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new frmMainContactsForm());

        }
    }
}
