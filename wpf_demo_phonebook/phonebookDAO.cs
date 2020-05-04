using System.Data;
using System.Data.SqlClient;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        private DbConnection conn;

        public PhonebookDAO()
        {
            conn = new DbConnection();

        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par nom
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        public DataTable GetAllContact()
        {

            string _querry =
                $"SELECT * FROM [Contacts]";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters = null;

            return conn.ExecuteSelectQuery(_querry, parameters);
        }

        public void DeleteContact(int _id)
        {


            string _querry =
                $"DELETE FROM [Contacts] WHERE ContactID = @_id";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            conn.ExecuteSelectQuery(_querry, parameters);

        }

        public void UpdateContact(string _FirstName, string _LastName, string _Email, string _Phone, string _Mobile, int _ContactID)
        {

            string _querry = $"UPDATE Contacts " +
                            $"SET FirstName = '{_FirstName}', " +
                            $"LastName = '{_LastName}'," +
                            $"Email = '{_Email}'," +
                            $"Phone = '{_Phone}'," +
                            $"Mobile = '{_Mobile}'" +
                            $"WHERE ContactID = @_id";


            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _ContactID;


            conn.ExecutUpdateQuery(_querry, parameters);
        }

        public void AddContact(string _FirstName, string _LastName, string _Email, string _Phone, string _Mobile, int _ContactID)
        {
            string _query = $"INSERT INTO [Contacts]" +
                           $" (FirstName, LastName, Email, Phone, Mobile) " +
                           $"OUTPUT INSERTED.ContactID " +
                           $"VALUES ('{_FirstName}','{_LastName}','{_Email}','{_Phone}','{_Mobile}')";

            conn.ExecutInsertQuery(_query, null);
        }
    }
}