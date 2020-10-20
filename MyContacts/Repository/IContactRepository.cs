using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyContacts
{
    interface IContactRepository
    {
        public DataTable SelectAll();
        public DataTable SelectRow(int contactID);

        public DataTable Search(string parameter);
        public bool Add(string name, string family, int age, string mobile);
        public bool Edit(int contactID, string name, string family, int age, string mobile);
        public bool Delete(int contactID);

    }
}
