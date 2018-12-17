using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Sql
{
    class DBConnection
    {
        //class fo working with the sql server
        private string connectString;
        private MySqlConnection connection;
        private MySqlCommand command;

        public DBConnection(string server, string project, string user, string pass)
        {
            this.connectString = string.Format("server={0};port=3306;Database={1};Uid={2};password={3};", server, project, user, pass);
            this.connection = new MySqlConnection(this.connectString);
            setCommandParams();
            try
            {
                this.connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public string getAllData(string table)
        {
            //implements "select * from table;"
            this.command.CommandText = string.Format("SELECT * FROM {0};", table);
            MySqlDataReader reader = this.command.ExecuteReader();
            setCommandParams();
            string rtrn = "";
            while (reader.Read())
                rtrn += reader["UserName"] + "," + reader["UserContacts"] + "," + reader["UserThreadId"] + "\n";
            reader.Close();
            return rtrn;

        }

        public void Update_Table(string tableName,string columns , string values)
        {
            //updates the according to recieved data 
            this.command.CommandText = string.Format("INSERT INTO {0} " + columns + values, tableName);
            MySqlDataReader reader = this.command.ExecuteReader();
            setCommandParams();
            reader.Close();
        }

        public void DeleteFromTable(string tableName, Dictionary<string, string> conditions)
        { 
            //gets the table name and a conditions dictionary in the format of (condition column = codition column value)
            // performs a DELETE operation on all conditions in the dictionary
            // if column type is string there must be parenthesis ("")
            string basicSyntex = string.Format("DELETE FROM {0} WHERE ", tableName);
            this.command.CommandText = basicSyntex;
            foreach(string key in conditions.Keys)
            {
                this.command.CommandText = basicSyntex + string.Format(key + " = " + conditions[key] + ";");
                
                MySqlDataReader reader = this.command.ExecuteReader();
                reader.Close();
            }
            setCommandParams();

        }

        public string GetConditionedData(string tableName, string[] columns, Dictionary<string, string> condition)
        {
            //returns the data from database conditioned;
            string colms = "";
            if (columns != null)
            {
                for (int i = 0; i < columns.Length - 1; i++)
                    colms += columns[i] + ",";
                colms += columns[columns.Length - 1];
            }
            else
                colms = "*";

            this.command.CommandText = string.Format("Select {0} from {1} where {2} = {3};", colms, tableName, condition.Keys.First<string>(), condition[condition.Keys.First<string>()]);
            Console.WriteLine(this.command.CommandText);

            MySqlDataReader reader = this.command.ExecuteReader();
            string rtrn = "";
            while (reader.Read())
            {
                if (colms == "*")
                    rtrn = reader["UserName"] + "," + reader["UserContacts"] + "," + reader["UserThreadId"];
                else
                {
                    for (int i = 0; i < columns.Length - 1; i++)
                        rtrn += reader[columns[i]] + ",";
                    rtrn += reader[columns[columns.Length - 1]];
                }

            }
            setCommandParams();
            return rtrn;
        }
        private void setCommandParams()
        {
            this.command = this.connection.CreateCommand();
        }
    }
}
