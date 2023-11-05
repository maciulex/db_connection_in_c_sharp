using DATABASE;

internal class HelloWorld
{
    static void Main()
    {
        //opcja 1 z konkretnym serverem ustawione
        DATABASE.DATABASE_CREDITS db_cr = new DATABASE.DATABASE_CREDITS("localhost", "root", "");//option 1 connects to specified server
        //DATABASE.DATABASE_CONNECTION db = new DATABASE.DATABASE_CONNECTION(); // conn option 2 connects to data from data_holders/database_credits.cs
        DATABASE.DATABASE db = new DATABASE.DATABASE(db_cr);//creates connection


        //db.changeDB("db_name"); // changes active database

        //db.MANAGER.wholeSnippet();     //create files json(database structures) c#(for use in program) sql(data in databases)
        //db.MANAGER.dbSchemeToJSON();   //create image of database structure
        //db.MANAGER.dbSchemeToClass();  //Create image of database in c# files class namespace DATABASE_SCHEME, class <db_name>_DATABASE -> class <table>_TABLE
        //db.MANAGER.getAllSnipets();    //Creates json and c# files
        //db.MANAGER.dbDataToSql();      //Creates sql files with INSERT commands for restoring database data in tables
        //db.MANAGER.getAllScheme();     //Download database structure from server saves in db.DATABASES.MANAGER var;

        //drops all databases from given path (only json files)
        //db.MANAGER.dropAllDatabsesFromSnippet("programmist/snippet/1/json/");
        //creates databases and tables from given json file
        //db.MANAGER.recoverFromJson("programmist/snippet/1/json/");
        //restores data in tables from given sql path
        //db.MANAGER.sqlDataToDb("programmist/snippet/1/sql/");
        return;
        // string query = new DATABASE.QUERY_BUILDER().SELECT("*").FROM("agents").getDone();//Creating query not finished!
        // Console.WriteLine(query);
        // var data = db.query(query); //executes query returns data_holders/QUERY_RESULT
        
        //cating query_result to generated c# class based on database structure
        // List<DATABASE_SCHEME.example_db_DATABASE.agents_TABLE> TEMPS = DATABASE_SCHEME.example_db_DATABASE.agents_TABLE.get_agents_from_query(data)

        //printing some data
        // foreach(DATABASE_SCHEME.example_db_DATABASE.agents_TABLE t in TEMPS) {
        //     Console.WriteLine(t.AGENT_CODE  );
        //     Console.WriteLine(t.AGENT_NAME  );
        //     Console.WriteLine(t.WORKING_AREA);
        //     Console.WriteLine(t.COUNTRY     );

        // }

        //db.connect(); //manual opening and closing connection 
        //var data = db.query(query);
        //db.closeConnection();

        
        //foreach (var listItem in data.data){
        //    //jak wyciągamy np 1000 userów to listItem jest jednym userem a instrukcja foreach przejdzie po każdym userze wybranym z bazy danych
        //    foreach (var kvp in listItem){
        //        //kvp.Key   <- to jest nazwa kolumny
        //        //kvp.Value <- to jest wartość tej kolumny
        //        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        //    }
        //}
    }
}
