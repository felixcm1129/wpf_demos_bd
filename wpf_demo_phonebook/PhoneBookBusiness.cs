using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wpf_demo_phonebook
{
    static class PhoneBookBusiness
    {
        private static PhonebookDAO dao = new PhonebookDAO();

        public static ContactModel GetContactByName(string _name)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByName(_name);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }

        public static IEnumerable<ContactModel> GetAll()
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            List<ContactModel> contactsList = new List<ContactModel> { };

            dt = dao.GetAllContact();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
               
                    contactsList.Add(cm);
                }


            }

            foreach (ContactModel c in contactsList)
            {

                yield return c;
            }


        }

        public static void UpdateContact(ContactModel contact)
        {

            dao.UpdateContact(contact.FirstName, contact.LastName, contact.Email, contact.Phone, contact.Mobile, contact.ContactID);
        }

        public static void DeleteContact(int _id)
        {
            dao.DeleteContact(_id);

        }

        public static ContactModel GetContactByID(int _id)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByID(_id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }

        private static ContactModel RowToContactModel(DataRow row)
        {
            ContactModel cm = new ContactModel();

            cm.ContactID = Convert.ToInt32(row["ContactID"]);
            cm.FirstName = row["FirstName"].ToString();
            cm.LastName = row["LastName"].ToString();
            cm.Email = row["Email"].ToString();
            cm.Phone = row["Phone"].ToString();
            cm.Mobile = row["Mobile"].ToString();

            return cm;
        }

        public static void NewContact(ContactModel c)
        {
            dao.AddContact(c.FirstName, c.LastName, c.Email, c.Phone, c.Mobile, c.ContactID);
        }

    }
}