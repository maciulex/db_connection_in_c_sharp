using Azure.Core.Pipeline;
using Microsoft.Identity.Client;
using MySqlConnector;
using System.Data;

namespace DATABASE {
    public class DATABASE
    {   
        CONNECTION conn = new CONNECTION();
        public DATABASE_MANAGER MANAGER;



        public ref CONNECTION getConnection() {
            return ref conn;
        }

        /// <summary>
        /// Establishes a database connection using provided credentials.
        /// Given credentials will overwrite main connection credentials
        /// The manually opened connection needs to be manually closed
        /// </summary>
        /// <param name="host">The database server host address.</param>
        /// <param name="user">The username for the database connection.</param>
        /// <param name="password">The password for the database connection.</param>
        /// <param name="DB">The name of the database to connect to.</param>
        /// <returns>Bool value if true connection is successful if false connection failed</returns>
        public bool connect(string host, string user, string password, string DB) {
            conn.connection_credits.DATABASE_SERVER = host;
            conn.connection_credits.USER            = user;
            conn.connection_credits.PASSWORD        = password;
            conn.connection_credits.DATABASE        = DB;
            
            conn.openConnection();
            return conn.connection_state();
        }

        /// <summary>
        /// Establishes a database connection using provided credentials without specifying the host.
        /// Given credentials will overwrite main connection credentials
        /// The manually opened connection needs to be manually closed
        /// </summary>
        /// <param name="user">The username for the database connection.</param>
        /// <param name="password">The password for the database connection.</param>
        /// <param name="DB">The name of the database to connect to.</param>
        /// <returns>Bool value if true connection is successful if false connection failed</returns>
        public bool connect(string user, string password, string DB) {
            conn.connection_credits.USER            = user;
            conn.connection_credits.PASSWORD        = password;
            conn.connection_credits.DATABASE        = DB;
            
            conn.openConnection();
            return conn.connection_state();
        }

        /// <summary>
        /// Establishes a database connection using provided database name without specifying the credentials.
        /// Given database name will overwrite main connection credentials
        /// The manually opened connection needs to be manually closed
        /// </summary>
        /// <param name="DB">The name of the database to connect to.</param>
        /// <returns>Bool value if true connection is successful if false connection failed</returns>
        public bool connect(string DB) {
            conn.connection_credits.DATABASE        = DB;
            
            conn.openConnection();
            return conn.connection_state();
        }
        
        /// <summary>
        /// Establishes a database connection using the provided database credentials object.
        /// Provided credentials will overwrite main connection credentials
        /// The manually opened connection needs to be manually closed
        /// </summary>
        /// <param name="credit">An instance of DATABASE_CREDITS containing the connection details.</param>
        /// <returns>Bool value if true connection is successful if false connection failed</returns>
        public bool connect(DATABASE_CREDITS credit) {
            conn.connection_credits                = credit;
            conn.openConnection();
            return conn.connection_state();
        }

        /// <summary>
        /// Establishes a database connection using the previously configured credentials.
        /// The manually opened connection needs to be manually closed
        /// </summary>
        /// <returns>Bool value if true connection is successful if false connection failed</returns>
        public bool connect() {
            conn.openConnection();
            return conn.connection_state();
        }

        /// <summary>
        /// Executes an SQL query using the provided SQL query text and database credentials.
        /// </summary>
        /// <param name="queryText">The SQL query to execute.</param>
        /// <param name="credit">An instance of DATABASE_CREDITS containing the connection details.</param>
        /// <returns>A QUERY_RESULT object containing the query results or error information.</returns>
        public QUERY_RESULT query(String queryText, DATABASE_CREDITS credit) {
            connect(credit);
            var d = query(queryText);
            conn.closeConnection();
            return d;
        }

        /// <summary>
        /// Executes an SQL query using the provided SQL query text.
        /// If the connection was not established, the program will try to establish it with set credentials and close connection after query execution
        /// If the connection was manually opened before, it is required to close it manually
        /// </summary>
        /// <param name="queryText">The SQL query to execute.</param>
        /// <returns>A QUERY_RESULT object containing the query results or error information.</returns>
        public QUERY_RESULT query(String queryText) {
            var wholeData = new QUERY_RESULT();
            bool connectionOpened = false;
            try {
                if (!conn.connection_state()) {
                    connect();
                    connectionOpened = true;
                }
                using var command = new MySqlCommand(queryText, conn.getConn());
                using var reader = command.ExecuteReader();

                while (reader.Read()) {
                    wholeData.columnAmount = Convert.ToUInt64(reader.FieldCount);
                    var data = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++) {
                        string columnName  = reader.GetName(i);
                        object columnValue = reader.GetValue(i);
                        data[columnName] = columnValue;
                    }

                    // Teraz "data" zawiera dynamicznie odczytane pola z wyników zapytania
                    wholeData.data.Add(data);
                }
                reader.Close();

                if (connectionOpened) 
                    conn.closeConnection();
            } catch (Exception e) {
                if (connectionOpened) 
                    conn.closeConnection();
                
                Console.WriteLine(e.ToString());
                wholeData.e = e;
                wholeData.columnAmount = 0;

                return wholeData;
            } 
            return wholeData;
        }

        /// <summary>
        /// Initializes a new instance of the DATABASE class with the provided database credentials.
        /// </summary>
        /// <param name="credit">An instance of DATABASE_CREDITS containing the connection details.</param>
        public DATABASE(DATABASE_CREDITS credit) {
            conn.connection_credits = credit;
            MANAGER = new DATABASE_MANAGER(this);
        }

        /// <summary>
        /// Initializes a new instance of the DATABASE class without specifying credentials.
        /// </summary>
        public DATABASE() {
            MANAGER = new DATABASE_MANAGER(this);
        }
    }
}
