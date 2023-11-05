namespace DATABASE {
    partial class DATABASE_MANAGER {
        public void CreateDatabase(string db_name) {
            DATABASE_MAIN.query("CREATE DATABASE "+db_name);
        }
    }
}