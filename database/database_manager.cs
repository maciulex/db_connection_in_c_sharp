using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Net.Http.Headers;
using Azure.Core.Pipeline;
using MySqlConnector;

namespace DATABASE {
    public partial class  DATABASE_MANAGER {
        DATABASE DATABASE_MAIN;
        List<DATABASE_STRUCT>    DATABASES = new List<DATABASE_STRUCT>{};

        enum DATABASE_LIST_MODE {WHITELIST, BLACKLIST};
        DATABASE_LIST_MODE DATABASE_LIST_ACTIVE_MODE = DATABASE_LIST_MODE.WHITELIST;
        //"performance_schema", "information_schema","mysql"
        List<string>             DATABASE_LIST = new List<string>{"example_db"};
        string baseSnippetPath = "programmist/snippet/";
        
        bool DATABASE_TABLES_DOWLOADED = false;
        bool DATABASE_NAME_DOWLOADED   = false;

        private int getFreeSnipetNumber() {
            int snippetFolder = 1;
            FILES.FILES fileManager = new FILES.FILES();

            fileManager.createDirectory(baseSnippetPath);
            while (fileManager.directoryExists(baseSnippetPath+""+snippetFolder)) snippetFolder++;

            return snippetFolder;
        }

        public void getAllSnipets() {
            int snippetNumber = getFreeSnipetNumber();
            dbSchemeToClass(snippetNumber);
            dbSchemeToJSON (snippetNumber);
        }

        public void dbSchemeToClass(int snippetNumber = -1) {
            if (DATABASES.Count() == 0) getAllScheme();
            FILES.FILES fileManager = new FILES.FILES();

            string classPath;

            if (snippetNumber == -1) snippetNumber = getFreeSnipetNumber();
            classPath = baseSnippetPath+""+snippetNumber+"/class/";
            fileManager.createDirectory(classPath);

            foreach (DATABASE_STRUCT db in DATABASES) {
                StreamWriter f = new StreamWriter(classPath+db.DB_NAME+".cs", false);

                f.WriteLine("using DATABASE;");
                f.WriteLine("namespace DATABASE_SCHEME {");
                f.WriteLine("\tclass "+db.DB_NAME+" {");
                foreach (DATABASE_TABLE_STRUCT table in db.TABLES) {
                    f.WriteLine("\t\tpublic class "+table.TABLE_NAME+" {");
                    string ifForConversion = "";
                    foreach (DATABASE_TABLE_COLUMN_STRUCT colummn in table.COLUMNS) {
                        string type       = "";
                        string deafultVal = "";
                        string converter  = "";
                        switch (colummn.DATA_TYPE) {
                            case "tinyint":
                            case "smallint":
                            case "mediumint":
                            case "int":
                                type       = "int";
                                deafultVal = "0";
                                converter = "Convert.ToInt32";
                            break;
                            case "bigint":
                                type       = "long";
                                deafultVal = "0";
                                converter = "Convert.ToInt64";
                            break;
                            case "decimal":
                            case "float":
                                type       = "float";
                                deafultVal = "0";
                                converter = "Convert.ToSingle";
                            break;
                            case "double":
                                type       = "double";
                                deafultVal = "0";
                                converter = "Convert.ToDouble";
                            break;
                            case "real":
                                type       = "decimal";
                                deafultVal = "0";
                                converter = "Convert.ToDecimal";
                            break;
                            case "bit":
                            case "boolean":
                                type       = "bool";
                                deafultVal = "false";
                                converter = "Convert.ToBoolean";
                            break;
                            case "data":
                            case "datetime":
                            case "date":
                            case "timestamp":
                            case "time":
                            case "year":
                            case "varchar":
                            case "tinytext":
                            case "text":
                            case "mediumtext":
                            case "longtext":   
                            case "char":
                                type       = "string";
                                deafultVal = "\"\"";  
                                converter = "Convert.ToString";                       
                            break;
                        }
                        f.WriteLine("\t\t\tpublic "+type+" "+colummn.COLUMN_NAME+"= "+deafultVal+";");

                        ifForConversion += @"
                    if (queryRow.ContainsKey("""+colummn.COLUMN_NAME+@""")) {
                        tempClass."+colummn.COLUMN_NAME+@" = "+converter+@"(queryRow["""+colummn.COLUMN_NAME+@"""]);
                    }
                        ";
                    }
                    string a  = "            static public List<"+table.TABLE_NAME+"> get_"+table.TABLE_NAME+"_from_query(QUERY_RESULT query) {\n";
				            a+= "                 List<"+table.TABLE_NAME+"> result = new List<"+table.TABLE_NAME+">{};\n"; 
			                a+= "                 foreach (Dictionary<string, object> queryRow in query.data) {\n";
				            a+= "                    "+table.TABLE_NAME+" tempClass = new "+table.TABLE_NAME+"{};\n"; 
                            a+= "                     "+ifForConversion+"\n";
				            a+= "                    result.Add(tempClass);\n"; 
			                a+= "                 }\n";
			                a+= "                 return result;\n";
			                a+= "             }\n";
                    f.WriteLine(a);
                    f.WriteLine("\t\t}");
                }
                f.WriteLine("\t}\n}");
                f.Close();
            }
        }

        public void dbSchemeToJSON(int snippetNumber = -1) {
            if (DATABASES.Count() == 0) getAllScheme();
            FILES.FILES fileManager = new FILES.FILES();

            string jsonPath;

            if (snippetNumber == -1) snippetNumber = getFreeSnipetNumber();
            jsonPath = baseSnippetPath+""+snippetNumber+"/json/";
            fileManager.createDirectory(jsonPath);

            foreach (DATABASE_STRUCT db in DATABASES) {
                StreamWriter f = new StreamWriter(jsonPath+db.DB_NAME+".json", false);
                f.WriteLine("{");
                f.WriteLine("\t\"DATABASE\": {");
                f.WriteLine("\t\t\"DATABASE_META\": {");
                f.WriteLine("\t\t\t\"DATABASE_NAME\":\""+db.DB_NAME+"\",");
                f.WriteLine("\t\t\t\"DATABASE_TABLE_COUNT\":\""+db.TABLES.Count+"\"");
                f.WriteLine("\t\t},");
                f.WriteLine("\t\t\"DATABASE_DATA\": {");
                for (int i = 0; ; i++) {
                    if (db.TABLES.Count <= 0) break;
                    f.WriteLine("\t\t\t\""+db.TABLES[i].TABLE_NAME+"\": {");
                    for (int z = 0; ; z++) {
                        if (db.TABLES[i].COLUMNS.Count <= 0) break;
                        f.WriteLine("\t\t\t\t\""+db.TABLES[i].COLUMNS[z].COLUMN_NAME+"\":{"                 );
                        f.WriteLine("\t\t\t\t\t\"COLUMN_DEFAULT\":   \""+db.TABLES[i].COLUMNS[z].COLUMN_DEFAULT     +"\","  );
                        f.WriteLine("\t\t\t\t\t\"ORDINAL_POSITION\": \""+db.TABLES[i].COLUMNS[z].ORDINAL_POSITION   +"\","  );
                        f.WriteLine("\t\t\t\t\t\"DATA_TYPE\":        \""+db.TABLES[i].COLUMNS[z].DATA_TYPE          +"\","  );
                        f.WriteLine("\t\t\t\t\t\"COLUMN_KEY\":       \""+db.TABLES[i].COLUMNS[z].COLUMN_KEY         +"\","  );
                        f.WriteLine("\t\t\t\t\t\"EXTRA\":            \""+db.TABLES[i].COLUMNS[z].EXTRA              +"\","  );
                        f.WriteLine("\t\t\t\t\t\"COLUMN_TYPE\":      \""+db.TABLES[i].COLUMNS[z].COLUMN_TYPE        +"\","  );
                        f.WriteLine("\t\t\t\t\t\"IS_PRIMARY\":       \""+((db.TABLES[i].COLUMNS[z].IS_PRIMARY      ) ? "true" : false)+"\","  );
                        f.WriteLine("\t\t\t\t\t\"IS_UNIQUE\":        \""+((db.TABLES[i].COLUMNS[z].IS_UNIQUE       ) ? "true" : false)+"\","  );
                        f.WriteLine("\t\t\t\t\t\"IS_AUTOINCREMENT\": \""+((db.TABLES[i].COLUMNS[z].IS_AUTOINCREMENT) ? "true" : false)+"\","  );
                        f.WriteLine("\t\t\t\t\t\"IS_NULLABLE\":      \""+((db.TABLES[i].COLUMNS[z].IS_NULLABLE     ) ? "true" : false)+"\""   );


                        if (z < db.TABLES[i].COLUMNS.Count - 1) { 
                            f.WriteLine("\t\t\t\t},");
                        } else {
                            f.WriteLine("\t\t\t\t}");
                            break;
                        }
                    }
                    if (i < db.TABLES.Count - 1) { 
                        f.WriteLine("\t\t\t},");
                    } else {
                        f.WriteLine("\t\t\t}");
                        break;
                    }
                }
                f.WriteLine("\t\t}");

                f.WriteLine("\t}\n}");
                f.Close();
            }
        }

        public bool doesDatabaseExists(string db_name) {
            if (!DATABASE_NAME_DOWLOADED) getAllDatabases(DATABASE_MAIN);
            foreach (DATABASE_STRUCT db in DATABASES) {
                if (db.DB_NAME == db_name) return true;
            }
            return false;
        }

        public void getAllScheme() {
            DATABASE_TABLES_DOWLOADED = true;
            getAllDatabases         (DATABASE_MAIN);
            getAllTablesForDatabases(DATABASE_MAIN);
        }

        public void getAllTablesForDatabases(DATABASE db) {
            DATABASE_TABLES_DOWLOADED = true;
            foreach (DATABASE_STRUCT dbClass in DATABASES) {
                String sql_get_tables_in_db      = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='"+dbClass.DB_NAME+"' GROUP BY TABLE_NAME;";
                List<Dictionary<string, object>> TableNames = db.query(sql_get_tables_in_db).data;

                foreach (Dictionary<string, object> Table in TableNames) {
                    String sql_get_scheme_table_in_db= "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='"+dbClass.DB_NAME+"' AND TABLE_NAME = '"+Table["TABLE_NAME"]+"';";
                    List<Dictionary<string, object>> table = db.query(sql_get_scheme_table_in_db).data;
                    DATABASE_TABLE_STRUCT table_struct = new DATABASE_TABLE_STRUCT{};
                    table_struct.TABLE_NAME = (string)Table["TABLE_NAME"];

                    foreach (Dictionary<string, object> column in table) {
                        DATABASE_TABLE_COLUMN_STRUCT table_column_struct = new DATABASE_TABLE_COLUMN_STRUCT();
                        foreach (var singleColumn in column) {
                            switch (singleColumn.Key) {
                                case "COLUMN_NAME":
                                    table_column_struct.COLUMN_NAME         = (string)singleColumn.Value;
                                break;
                                case "COLUMN_DEFAULT":
                                    if (DBNull.Value.Equals(singleColumn.Value)){
                                        table_column_struct.COLUMN_DEFAULT      = "NULL";
                                    }else { 
                                        table_column_struct.COLUMN_DEFAULT      = (string)singleColumn.Value;
                                    }
                                break;
                                case "ORDINAL_POSITION":
                                    table_column_struct.ORDINAL_POSITION    = (int)(UInt64)singleColumn.Value;
                                break;
                                case "DATA_TYPE":
                                    table_column_struct.DATA_TYPE           = (string)singleColumn.Value;
                                break;
                                case "COLUMN_TYPE":
                                    table_column_struct.COLUMN_TYPE         = (string)singleColumn.Value;
                                break;
                                case "COLUMN_KEY":
                                    table_column_struct.COLUMN_KEY          = (string)singleColumn.Value;
                                    table_column_struct.IS_PRIMARY          = ((string)singleColumn.Value).Contains("PRI");
                                    table_column_struct.IS_UNIQUE           = ((string)singleColumn.Value).Contains("UNI");
                                break;
                                case "EXTRA":
                                    table_column_struct.EXTRA               = (string)singleColumn.Value;
                                    table_column_struct.IS_AUTOINCREMENT    = ((string)singleColumn.Value).Contains("auto_increment");
                                break;
                                case "IS_NULLABLE":
                                    table_column_struct.IS_NULLABLE         = ((string)singleColumn.Value == "NO") ? false : true;
                                break;
                            }
                        }
                        table_struct.COLUMNS.Add(table_column_struct);
                    }
                    dbClass.TABLES.Add(table_struct);
                }
            }
        }


        public bool checkIfIgnoreDatabase(string db_name) {
            bool toIgnore = false;
            if (DATABASE_LIST_ACTIVE_MODE == DATABASE_LIST_MODE.BLACKLIST) {
                foreach (string ignore in DATABASE_LIST) {
                    if (ignore == db_name) {
                        toIgnore = true;
                        break;
                    }
                }
            } else {
                toIgnore = true;
                foreach (string ignore in DATABASE_LIST) {
                    if (ignore == db_name) {
                        toIgnore = false;
                        break;
                    }
                }
            }
            return toIgnore;
        }
        public void getAllDatabases(DATABASE db) {
            DATABASE_NAME_DOWLOADED = true;
            QUERY_RESULT databases = new QUERY_RESULT();
            databases = db.query("SHOW DATABASES");
            
            DATABASES = new List<DATABASE_STRUCT>{};
            
            if (databases.columnAmount == 0) {
                return;
            }

            foreach (Dictionary<string, object> data in databases.data) {
                foreach (var name in data){
                    if (name.Key != "Database") continue;

                    if (checkIfIgnoreDatabase((string)name.Value)) continue;

                    DATABASE_STRUCT dNew = new DATABASE_STRUCT();
                    dNew.DB_NAME = (String)name.Value;
                    DATABASES.Add(dNew);
                }
            }
        }

        public DATABASE_MANAGER (DATABASE db) {
            this.DATABASE_MAIN = db;
        }
    }
}