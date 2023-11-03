using System.ComponentModel.Design;

namespace DATABASE {
    public class QUERY_BUILDER {
        private List<string> toSelect = new List<string>{}; 
        private List<string> getFrom = new List<string>{}; 
        private List<string> getWhen = new List<string>{}; 

        public QUERY_BUILDER SELECT(string what) {
            toSelect.Add(what);
            return this;
        }
        public QUERY_BUILDER SELECT(string [] what) {
            foreach (string item in what)
                toSelect.Add(item);
            return this;
        }
        public QUERY_BUILDER SELECT(List<string> what) {
            foreach (string item in what)
                toSelect.Add(item);
            return this;
        }

        public QUERY_BUILDER FROM(string from) {
            getFrom.Add(from);
            return this;
        }
        public QUERY_BUILDER FROM(string [] from) {
            foreach (string item in from)
                getFrom.Add(item);
            return this;
        }
        public QUERY_BUILDER FROM(List<string> from) {
            foreach (string item in from)
                getFrom.Add(item);
            return this;
        }

        public QUERY_BUILDER WHERE(string WHERE) {
            getWhen.Add(WHERE);
            return this;
        }
        public QUERY_BUILDER WHERE(string [] WHERE) {
            foreach (string item in WHERE)
                getWhen.Add(item);
            return this;
        }
        public QUERY_BUILDER WHERE(List<string> WHERE) {
            foreach (string item in WHERE)
                getWhen.Add(item);
            return this;
        }

        public string getDone() {
            string query = "";

            if (toSelect.Count > 0) {
                query += "SELECT ";
                foreach (string sel in toSelect) {
                    if (sel == "*") {
                        query += sel+", ";
                    } else {
                        query += "`"+sel+"`, ";
                    }
                }
                query = query.Remove(query.Length - 2, 1);
            }

            if (toSelect.Count > 0) {
                query += "FROM ";
                foreach (string fr in getFrom) query += "`"+fr+"`, ";
                query = query.Remove(query.Length - 2, 1);
            }

            if (getWhen.Count != 0) {
                query += "WHERE ";
                foreach (string when in getWhen) query += when+" ";
            }


            return query;
        }
    }
}