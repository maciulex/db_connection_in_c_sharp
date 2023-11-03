namespace FILES {
    class FILES {
        public bool directoryExists(string path) {
            if (Directory.Exists(path)){
                return true;
            }
            return false;
        }
        public bool createDirectory(string path) {
            if (directoryExists(path)) return false;
            Directory.CreateDirectory(path);

            return true;
        }
    }   
}