using System;
using System.Windows;
using System.Windows.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
namespace Asign2
{
    class PublicationList
    {
       

        public static List<database.publication> generate_P_list(
            int ID,           
            List<database.researcher_publication> researcher_publication_list, 
            List<database.publication> publication_list)
        {

            List<database.publication> P_list = new List<database.publication>();

            var PublicationQuery =  from researcher_publication in researcher_publication_list
                                    join publication in publication_list
                                    on researcher_publication.doi equals publication.doi into joined
                                    from j in joined.DefaultIfEmpty()
                                    orderby j.year descending, j.title ascending 
                                    // where Convert.ToInt32(j.year) >= Convert.ToInt32(fromyear) && Convert.ToInt32(j.year) <= Convert.ToInt32(toyear)
                                    //where Convert.ToInt32(j.year) >= year_from && Convert.ToInt32(j.year) <= year_to
                                    where researcher_publication.researcher_id == ID
                                    select new
                                    {
                                        j.year,
                                        j.title,
                                        j.doi,                                                                            
                                    };
           foreach (var x in PublicationQuery)
           {               
               P_list.Add(new database.publication { doi = x.doi, year = x.year, title = x.title });
           }

           return P_list;
       }

        public static void additemt_p_listbox(ListBox list2, List<database.publication> P_list)
        {
            

            foreach (database.publication publication in P_list)
            {
                list2.Items.Add(publication.title + " (" + publication.year + ")");
            }
           

        }
        public static List<database.publication> sort_P_list(List<database.publication> P_list, int year_from, int year_to, bool invert)
        {

            List<database.publication>tmp_list = new List<database.publication>();

            if (invert == true)
            {
                //MessageBox.Show("!23");
                var PublicationQuery = from publication in P_list
                                       // where Convert.ToInt32(j.year) >= Convert.ToInt32(fromyear) && Convert.ToInt32(j.year) <= Convert.ToInt32(toyear)
                                       where Convert.ToInt32(publication.year) >= year_from && Convert.ToInt32(publication.year) <= year_to
                                       // where researcher_publication.researcher_id == ID                                  
                                       orderby publication.year descending, publication.title ascending
                                       select publication;
                
                foreach (database.publication publication in PublicationQuery)
                {

                    tmp_list.Add(new database.publication { doi = publication.doi, year = publication.year, title = publication.title });
                }
                P_list = tmp_list;
                
            }
            if (invert == false)
            {

                var PublicationQuery = from publication in P_list
                                       // where Convert.ToInt32(j.year) >= Convert.ToInt32(fromyear) && Convert.ToInt32(j.year) <= Convert.ToInt32(toyear)
                                       where Convert.ToInt32(publication.year) >= year_from && Convert.ToInt32(publication.year) <= year_to
                                       // where researcher_publication.researcher_id == ID                                  
                                       orderby publication.year ascending, publication.title ascending
                                       select publication;
               // P_list.Clear();
                foreach (database.publication publication in PublicationQuery)
                {
                    tmp_list.Add(new database.publication { doi = publication.doi, year = publication.year, title = publication.title });
                }
                P_list = tmp_list;

            }

            

            return P_list;           

        }

    
         
    }
}
