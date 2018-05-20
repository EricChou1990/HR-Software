using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows;
namespace Asign2
{
    class PublicationDetail
    {
        public static string Pdetail(string doi, List<database.publication> publication_list)
        {
            //Use LINQ to find specific researcher
            IEnumerable<database.publication> PublicationQuery =
            from publication in publication_list
            where publication.doi == doi
            select publication;
            //--

            string content = "";
            foreach (database.publication publication in PublicationQuery)
            {
                
                content += "DOI:     " + doi + "\r\n";
                content += "Title:   " + publication.title + "\r\n";
                content += "Authors: " + publication.authors + "\r\n";
                content += "Publication year: " + publication.year + "\r\n";
                content += "Type: " + publication.type + "\r\n";
                content += "Cite as: " + publication.cite_as + "\r\n";
                content += "Availability date: " + ResearcherDetail.time_convert(publication.available).ToShortDateString() + "\r\n";
                content += "Age: " + DateTime.Now.Subtract(ResearcherDetail.time_convert(publication.available)).Days.ToString() + " days\r\n";               
            }
            return content;
        }
    }
    
}
