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
                    if (doesDatabaseExists(database.DATABASE.DATABASE_META.DATABASE_NAME.ToString())) 
                        return false;

                    CreateDatabase(database.DATABASE.DATABASE_META.DATABASE_NAME.ToString());
                    DATABASE_MAIN.changeDB(database.DATABASE.DATABASE_META.DATABASE_NAME.ToString());
                    
                    foreach (var TABLE in database.DATABASE.DATABASE_DATA) {
                        //Console.WriteLine(TABLE.Name);
                        List<string> columnNames     = new List<string>{}; 
                        List<string> columnTypes     = new List<string>{};
                        List<bool> isAutoIncrement   = new List<bool>{};
                        List<bool> isPrimary         = new List<bool>{};
                        List<bool> isUnique          = new List<bool>{};
                        List<bool> isNull            = new List<bool>{};
                        List<string> deafult         = new List<string>{};
                        List<string> extra           = new List<string>{};

                        foreach (var COLUMN in TABLE.Value) {
                            columnNames.Add    ((string)COLUMN.Name);
                            // Console.WriteLine(COLUMN.Name);
                            
                            columnTypes.Add    ((string)COLUMN.Value.COLUMN_TYPE);

                            isAutoIncrement.Add((COLUMN.Value.IS_AUTOINCREMENT  == "true") ? true : false);
                            isPrimary.Add      ((COLUMN.Value.IS_PRIMARY        == "true") ? true : false);
                            isUnique.Add       ((COLUMN.Value.IS_UNIQUE         == "true") ? true : false);
                            isNull.Add         ((COLUMN.Value.IS_NULLABLE       == "true") ? true : false);

                            deafult.Add        ((string)COLUMN.Value.COLUMN_DEFAULT);
                            extra.Add          ((string)COLUMN.Value.EXTRA);

                        }
                        CreateTable(TABLE.Name, columnNames, columnTypes, isAutoIncrement, isPrimary, isUnique, isNull, deafult, extra);

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