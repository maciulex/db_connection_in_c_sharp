namespace DATABASE {
    partial class DATABASE_MANAGER {
        public void CreateTable(string tb_name, List<string> columnNames, List<string> columnTypes, List<bool> isAutoIncrement, 
                                List<bool> isPrimary, List<bool> isUnique, List<bool> isNull, List<string> deafult, List<string> extra) {
            
            string uniqueString = "", primaryString = ""; 
            
            string sql ="CREATE TABLE `"+tb_name+"`(";
            for (int i = 0; i < columnNames.Count(); i++) {
                string extraString = "";

                if (!isNull[i] || deafult[i] != "NULL") {
                    extraString += "NOT NULL";
                }
                if (!isNull[i] && deafult[i] != "NULL")
                    extraString += " DEFAULT "+ deafult[i];

                if (isAutoIncrement[i]) {
                    extraString += " AUTO_INCREMENT";
                }
                
                sql += columnNames[i] + " " + columnTypes[i]+" "+extraString;
                if (i < columnNames.Count() - 1) sql += ",";

                if (isPrimary[i]) {
                    primaryString += "ADD PRIMARY KEY (`"+columnNames[i]+"`),";
                }

                if (isUnique[i]) {
                    uniqueString += "ADD UNIQUE KEY `"+columnNames[i]+"` (`"+columnNames[i]+"`),";
                }
            }

            sql += ")ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;";
            
            if (uniqueString.Length == 0 && primaryString.Length > 0) 
                primaryString = primaryString.Remove(primaryString.Length-1, 1);

            if (uniqueString.Length > 0)
                uniqueString = uniqueString.Remove(uniqueString.Length-1, 1);
            
            if (uniqueString.Length > 0 || primaryString.Length > 0) {
                sql += "ALTER TABLE `"+tb_name+"` ";
                sql += primaryString + " " + uniqueString + ";";
            }

            // Console.WriteLine(sql);

            DATABASE_MAIN.query(sql);
        }
    }
}