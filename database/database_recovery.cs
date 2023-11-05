namespace DATABASE {


    
    public partial class DATABASE_MANAGER {
        public string getLatestSnippsetPath() {
            string directoryWithBackUp = "programmist/snippet";

            FILES.FILES fileManager = new FILES.FILES();
            string [] directories = fileManager.GetDirectories(directoryWithBackUp);
            int latestSnippet = -1;
            string result = "NULL";
            foreach (string d in directories) {
                try {
                    if (!fileManager.directoryExists(d+"/json/")) continue;
                    string nr = d.Split("\\")[1];
                    if (Convert.ToInt32(nr) > latestSnippet) {
                        latestSnippet = Convert.ToInt32(nr);
                        result = d;
                    }
                } catch (Exception e) {

                }
            }
            return result;
        }

        public bool recoverFromJson(string path) {
            FILES.FILES fileManager = new FILES.FILES();
            if (!fileManager.directoryExists(path)) return false;

            string [] files = fileManager.GetFiles(path);

            if (!DATABASE_NAME_DOWLOADED) {
                getAllScheme();
            }

            foreach (string f in files) {
                try {
                    var database = fileManager.getJsonFile(f);
                    Console.WriteLine(database.DATABASE.DATABASE_META.DATABASE_NAME.ToString());
                    if (doesDatabaseExists(database.DATABASE.DATABASE_META.DATABASE_NAME.ToString())) 
                        return false;

                    //CreateDatabase(database.DATABASE.DATABASE_META.DATABASE_NAME.ToString());
                    //DATABASE_MAIN.changeDB(database.DATABASE.DATABASE_META.DATABASE_NAME.ToString());
                    
                    foreach (var TABLE in database.DATABASE.DATABASE_DATA) {
                        Console.WriteLine(TABLE.Name);
                        //CreateTable(TABLE.Name);

                    }
                } catch (Exception e) { 
                    Console.WriteLine(e.ToString());
                }
            }

            return false;
        }

        public bool recoverFromLatestJson() {
            FILES.FILES fileManager = new FILES.FILES();
            
            return true;
        }
    }











}