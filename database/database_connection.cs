using MySqlConnector;

namespace DATABASE {
    public class CONNECTION {
        private MySqlConnection connection          = new MySqlConnection();
        public  DATABASE_CREDITS connection_credits = new DATABASE_CREDITS();

        /// <summary>
        /// Opens a new database connection using the configured credentials.
        /// </summary>
        /// <returns>A reference to the MySqlConnection object for the newly opened connection.</returns>
        public ref MySqlConnection openConnection() {
            connection = new MySqlConnection("Server=" + connection_credits.DATABASE_SERVER + ";User ID=" + connection_credits.USER + ";Password=" + connection_credits.PASSWORD + ";Database=" + connection_credits.DATABASE);
            connection.Open();
            return ref connection;
        }

        /// <summary>
        /// Closes the database connection.
        /// </summary>
        public void closeConnection() {
            connection.Close();
        }
            
        public ref MySqlConnection getConn() {
            return ref connection;
        }

        /// <summary>
        /// Checks the current state of the database connection.
        /// </summary>
        /// <returns>True if the connection is open, false otherwise.</returns>
        public bool connection_state() {
            if (connection.State.ToString() == "Open") {
                return true;
            }
            return false;
        }

        public CONNECTION() {

        }
    };
}