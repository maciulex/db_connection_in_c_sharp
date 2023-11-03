namespace DATABASE {
    public class DATABASE_CREDITS {
        //SET DEAFULT DATABASE CREDITS
        public string DATABASE_SERVER  = "xxx.xxx.x.x";
        public string USER             = "xxx";
        public string PASSWORD         = "xxx";
        public string DATABASE         = "xxx";

        public DATABASE_CREDITS(string server, string user, string password, string database) {
            DATABASE_SERVER = server;
            USER            = user;
            PASSWORD        = password;
            DATABASE        = database;
        }
        public DATABASE_CREDITS() {
            
        }
    }

}