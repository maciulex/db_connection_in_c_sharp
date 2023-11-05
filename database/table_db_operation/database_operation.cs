namespace DATABASE {
    partial class DATABASE_MANAGER {
        public void CreateDatabase(string db_name) {
            DATABASE_MAIN.query("CREATE DATABASE "+db_name);
        }

        public void DropDatabases(List<string> db_names) {
            DATABASE_MAIN.connect();
            foreach (string db in db_names) {
                string sql = "DROP DATABASE IF EXISTS "+db+";";
                DATABASE_MAIN.query(sql);
            }
            DATABASE_MAIN.closeConnection();

        }
    }
}