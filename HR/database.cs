using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Asign2
{
    class database
    {
        public class researcher
        {
            public string type { get; set; }
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string title { get; set; }
            public string unit { get; set; }
            public string campus { get; set; }
            public string email { get; set; }
            public string photo { get; set; }
            public string degree { get; set; }
            public string supervisor_id { get; set; }
            public string level { get; set; }
            public string utas_start { get; set; }
            public string current_start { get; set; }

        }
        public class position
        {
            public int id { get; set; }
            public string level { get; set; }
            public string start { get; set; }
            public string end { get; set; }

        }

        public class publication
        {
            public string doi { get; set; }
            public string title { get; set; }
            public string authors { get; set; }
            public string year { get; set; }
            public string type { get; set; }
            public string cite_as { get; set; }
            public string available { get; set; }

        }

        public class researcher_publication
        {
            public int researcher_id { get; set; }
            public string doi { get; set; }
        }
        
        

    }
}
