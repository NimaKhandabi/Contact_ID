using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class Form1 : Form
    {
        IContactRepository repository;

        public Form1()
        {
            InitializeComponent();
            repository = new ContactRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            dgContact.AutoGenerateColumns = false;
            dgContact.DataSource = repository.SelectAll();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEOrEdit frm = new frmAddEOrEdit();
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgContact.CurrentRow!=null)
            {
                string name = dgContact.CurrentRow.Cells[1].Value.ToString();
                string family = dgContact.CurrentRow.Cells[2].Value.ToString();
                string fullName = name + " " + family;
                if (MessageBox.Show($"آیا از حذف {fullName} مطمئن هستید؟","توجه",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    int contactId = (int)dgContact.CurrentRow.Cells[0].Value;
                    if (repository.Delete(contactId))
                    {
                        BindGrid();
                    } 
                }
            }
            else
            {
                MessageBox.Show("لطفا شخص مورد نظر را انتخاب کنید");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgContact.CurrentRow!=null)
            {
                int contactId = (int)dgContact.CurrentRow.Cells[0].Value; //me
                frmAddEOrEdit frm = new frmAddEOrEdit();
                frm.contactId = contactId;
                if (frm.ShowDialog()==DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgContact.DataSource = repository.Search(txtSearch.Text);
        }
    }
}
