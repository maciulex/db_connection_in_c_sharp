using DATABASE;

internal class HelloWorld
{
    static void Main()
    {
        //opcja 1 z konkretnym serverem ustawione
        DATABASE.DATABASE_CREDITS db_cr = new DATABASE.DATABASE_CREDITS("localhost", "root", "", "example_db");
        DATABASE.DATABASE db = new DATABASE.DATABASE(db_cr);

        //DATABASE.DATABASE_CONNECTION db = new DATABASE.DATABASE_CONNECTION(); // opcja 2 deafultowe wartości z pliku database_credits

        //db.MANAGER.dbSchemeToJSON();   //stworzenie obrazu bazy danych do pliku json
        //db.MANAGER.dbSchemeToClass();  //stworzenie obrazu bazy danych do plików c# zawięrajacych namespace DATABASE_SCHEME class <db_name> -> class<table>
        //db.MANAGER.getAllSnipets();    //stworzenie i plików json i c#
        //db.MANAGER.getAllScheme();     //pobranie struktóry baz danych z serwera




        //opcja 1 połączenie jest otwierane i zamykane w query
        string query = new DATABASE.QUERY_BUILDER().SELECT("*").FROM("agents").getDone();//stworzenie query
        Console.WriteLine(query);
        var data = db.query(query); // wykonanie query metoda zwraca klase QUERY_RESULT

        List<DATABASE_SCHEME.example_db.agents> TEMPS = new List<DATABASE_SCHEME.example_db.agents>{};//stworzenie listy
        TEMPS = DATABASE_SCHEME.example_db.agents.get_agents_from_query(data);//konwersja QUERY_RESULT na liste class z zapytania, klasy wygenerowane przez db.MANAGER.dbSchemeToClass(); 

        foreach(DATABASE_SCHEME.example_db.agents t in TEMPS) {
            Console.WriteLine(t.AGENT_CODE  );
            Console.WriteLine(t.AGENT_NAME  );
            Console.WriteLine(t.WORKING_AREA);
            Console.WriteLine(t.COUNTRY     );

        }

        //db.connect(); // opcja 2 połączenie manualnie otwierane i zamyka odpowiednie do wielu zapytań
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
