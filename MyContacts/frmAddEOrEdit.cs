using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class frmAddEOrEdit : Form
    {
        IContactRepository repository;
        public int contactId = 0;
        public frmAddEOrEdit()
        {
            InitializeComponent();
            repository = new ContactRepository();
        }

        private void frmAddEOrEdit_Load(object sender, EventArgs e)
        {
            if (contactId == 0)
            {
                this.Text = "افزون شخص جدید";
            }
            else
            {
                this.Text = "ویرایش اشخاص";
                DataTable dt = new DataTable();
                dt = repository.SelectRow(contactId);
                txtName.Text = dt.Rows[0][1].ToString();
                txtFamily.Text = dt.Rows[0][2].ToString();
                //txtAge.Value = Convert.ToInt32(dt.Rows[0][3].ToString());
                txtAge.Text = dt.Rows[0][3].ToString();
                txtMobile.Text = dt.Rows[0][4].ToString();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (isValidate())
            {
                bool isSuccsess;
                if (contactId == 0) // new one
                {
                    isSuccsess = repository.Add(txtName.Text, txtFamily.Text, (int)txtAge.Value, txtMobile.Text);
                    if (isSuccsess)
                    {
                        DialogResult = DialogResult.OK;
                        MessageBox.Show("اطلاعات با موفقیت ثبت شد", "", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("خطا در انجام عملیات", "", MessageBoxButtons.OK);
                    }
                }
                else // old one
                {
                    isSuccsess = repository.Edit(contactId, txtName.Text, txtFamily.Text, (int)txtAge.Value, txtMobile.Text);
                    if (isSuccsess)
                    {
                        DialogResult = DialogResult.OK;
                        MessageBox.Show("اطلاعات با موفقیت ویرایش شد","",MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("خطا در انجام عملیات", "", MessageBoxButtons.OK);
                    }
                }

            }

        }

        bool isValidate()
        {

            if (txtName.Text == "")
            {
                MessageBox.Show("لطفا نام را وارد کنید", "خطا", MessageBoxButtons.OK);
                return false;
            }
            if (txtFamily.Text == "")
            {
                MessageBox.Show("لطفا نام  خانوادگی را وارد کنید", "خطا", MessageBoxButtons.OK);
                return false;
            }
            if (txtAge.Value == 0)
            {
                MessageBox.Show("لطفا سن را وارد کنید", "خطا", MessageBoxButtons.OK);
                return false;
            }
            if (txtMobile.Text == "")
            {
                MessageBox.Show("لطفا شماره موبایل را وارد کنید", "خطا", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }


    }
}
