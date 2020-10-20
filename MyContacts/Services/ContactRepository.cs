using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Drawing.Text;

namespace MyContacts
{
    class ContactRepository : IContactRepository
    {

        //private string myConnection = "Data Source=.,Initial Catalog=Contact_DB,Integrated Security=yes";
        private string myConnection = "Data Source=.;" + "Initial Catalog=Contact_DB;" + "Integrated Security=SSPI;";

        public bool Add(string name, string family, int age, string mobile)
        {
            SqlConnection connection = new SqlConnection(myConnection);
            try
            {
                string query = "Insert Into MyContacts(Name,Family,Age,Mobile) values(@Name,@Family,@Age,@Mobile)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Mobile", mobile);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public bool Delete(int contactID)
        {
            SqlConnection connection = new SqlConnection(myConnection);
            try
            {
                string query = "Delete From MyContacts Where ContactID=@Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", contactID);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Edit(int contactID, string name, string family, int age, string mobile)
        {
            SqlConnection connection = new SqlConnection(myConnection);
            try
            {
                string query = "Update MyContacts Set Name=@Name,Family=@Family,Age=@Age,Mobile=@Mobile Where ContactID=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id",contactID);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Mobile", mobile);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally 
            {
                connection.Close();
            }
        }

        public DataTable Search(string parameter)
        {
            string query = "Select * From MyContacts Where Name like @parameter or Family like @parameter";
            SqlConnection connection = new SqlConnection(myConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@parameter","%"+ parameter +"%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectAll()
        {
            string query = "Select * From MyContacts";
            SqlConnection connection = new SqlConnection(myConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectRow(int contactID)
        {
            string query = "Select * From MyContacts Where ContactID=" + contactID;
            SqlConnection connection = new SqlConnection(myConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }
    }
}
