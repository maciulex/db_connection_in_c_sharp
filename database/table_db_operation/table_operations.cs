namespace DATABASE {
    partial class DATABASE_MANAGER {
        public void CreateTable(string tb_name, string [] columns, string [] columnTypes) {
            DATABASE_MAIN.query(@"
                CREATE TABLE `"+tb_name+@"`(

                )ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
            ");
        }
    }
}