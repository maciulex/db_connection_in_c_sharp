namespace DATABASE {
    public struct QUERY_RESULT {
        public Exception e;
        public UInt64 columnAmount;
        public List<Dictionary<string, object>> data;

        public QUERY_RESULT()
        {
            e = new Exception();
            data = new List<Dictionary<string, object>>();
            columnAmount = 0;
        }
    }
}