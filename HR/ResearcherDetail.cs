using System;
using System.Windows.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Asign2
{
    class ResearcherDetail
    {
        public static string showdetail(int ID, 
            List<database.researcher> rlist, 
            List<database.position> position_list,
            List<database.researcher_publication> researcher_publication_list,
            List<database.publication> publication_list,
            Button btn_showname)//Generate details of specific researcher
        {
            string content = ""; 
            //Use LINQ to find specific researcher
            IEnumerable<database.researcher> ResearcherQuery =
            from researcher in rlist
            where researcher.id == ID
            select researcher;
            //--

            foreach (database.researcher researcher in ResearcherQuery)
            {
                content = "Name:                                        " + researcher.first_name + ", " + researcher.last_name + "\r\n";
                content += "Title:                                           " + researcher.title + "\r\n";
                content += "School/Unit:                               " + researcher.unit + "\r\n";
                content += "Campus:                                     " + researcher.campus + "\r\n";
                content += "Email:                                          " + researcher.email + "\r\n";
                content += "Current Job Title:                        " + levelconvert(researcher.level) + "\r\n";
                content += "Commenced with institution:    " + time_convert(researcher.utas_start).ToShortDateString() + "\r\n";
                content += "Commenced current position:   " + time_convert(researcher.current_start).ToShortDateString() + "\r\n";
                content += "Previous position:                       " + prepos(ID, rlist, position_list) + "\r\n";
                content += "Tenure:                                       " + (DateTime.Now.Year - time_convert(researcher.utas_start).Year).ToString() + "\r\n";
                content += "Publications:                               " + publication_count(ID, researcher_publication_list) + "\r\n";
                if (researcher.type == "Staff")
                {
                    content += "3-year Average:                          " + threeyear_avg(ID, researcher_publication_list, publication_list) + "\r\n";
                    content += "Performance:                              " + performance(threeyear_avg(ID, researcher_publication_list, publication_list), researcher.level) + " %  \r\n";
                    content += "Supervision:                                " + supervision(ID, rlist) + "\r\n";
                }
                else
                {
                    content += "Degree:                                     " + researcher.degree + "\r\n";
                    content += "Supervisor:                                " + find_supervisor(ID, researcher.supervisor_id, rlist) + "\r\n";
                }

               // btn_showname.Click +=  new EventHandler(this.rBtn_Click);

                
            }

            return content ;
        }

        private static string levelconvert(string level)//Level conversion,eg. A->Postdoc
        {
            string levelcon="";
            switch (level)
            {
                case "A":
                    levelcon = "Postdoc";
                    break;
                case "B":
                    levelcon = "Lecturer";
                    break;
                case "C":
                    levelcon = "Senior Lecturer";
                    break;
                case "D":
                    levelcon = "Associate Professor";
                    break;
                case "E":
                    levelcon = "Professor";
                    break;
                default:
                    levelcon = "Research Student";
                    break;
            }
            return levelcon;
        }

        private static string publication_count(int ID, List<database.researcher_publication> researcher_publication_list)//Generate nubmers of publication by SQL
        {            
            //string sql3 = "SELECT COUNT(*) from researcher_publication where researcher_id=" + ID;

            //LINQ statement
            IEnumerable<database.researcher_publication> ResearcherQuery =
            from researcher_publication in researcher_publication_list
            where researcher_publication.researcher_id == ID
            select researcher_publication;
            //--
            
            string str = ResearcherQuery.Count().ToString();           
            return str;
        }

        public static double threeyear_avg(int ID, 
            List<database.researcher_publication> researcher_publication_list,
            List<database.publication> publication_list)
        {
            string fromyear = (DateTime.Now.Year - 3).ToString();
            string toyear = (DateTime.Now.Year - 1).ToString();           
            //string sql3 = "SELECT count(*) from (select year  from researcher_publication right join publication on publication.doi=researcher_publication.doi where year>"+past3+")as num" ;

            //LINQ statement
            var results = ( from researcher_publication in researcher_publication_list
                            join publication in publication_list
                            on researcher_publication.doi equals publication.doi into joined
                            from j in joined.DefaultIfEmpty()
                            where Convert.ToInt32(j.year) >= Convert.ToInt32(fromyear) && Convert.ToInt32(j.year) <= Convert.ToInt32(toyear)
                            //where Convert.ToInt32(j.year) >= 2012 && Convert.ToInt32(j.year) <= 2014
                            where researcher_publication.researcher_id==ID                          
                            select new
                            {                                                      
                            }).Count();
           double avg=0 ;
           double allpub = Convert.ToDouble(results);            
           avg = allpub / 3;
           avg = Math.Round(avg, 2);           
           return avg;
        }

        private static int publication_count(int ID,int year,
            List<database.researcher_publication> researcher_publication_list,
            List<database.publication> publication_list)
        {
            
            //LINQ statement
            var results = (from researcher_publication in researcher_publication_list
                           join publication in publication_list
                           on researcher_publication.doi equals publication.doi into joined
                           from j in joined.DefaultIfEmpty()
                           where Convert.ToInt32(j.year) ==year 
                           //where Convert.ToInt32(j.year) >= 2012 && Convert.ToInt32(j.year) <= 2014
                           where researcher_publication.researcher_id == ID
                           select new
                           {
                           }).Count();

            return results;
        }


        public static double performance(double threeyear_avg,string level)
        {        
            double  outcome ;
            switch (level)
            {
                case "A":                    
                    outcome = (Math.Round((threeyear_avg / 0.5),2) * 100) ;
                    break;
                case "B":
                    outcome = (Math.Round((threeyear_avg / 1),2) * 100) ;
                    break;
                case "C":
                    outcome = (Math.Round((threeyear_avg / 2),2) * 100) ;
                    break;
                case "D":
                    outcome = (Math.Round((threeyear_avg / 3.2),2) * 100) ;
                    break;
                case "E":
                    outcome = (Math.Round((threeyear_avg / 4),2) * 100);
                    break;
                default:
                    outcome = 0;
                    break;
            }           
            return outcome;
        }

        private static string supervision(int ID, List<database.researcher> rlist)
        {
            //LINQ statement
            var results = (from researcher in rlist                           
                           where researcher.supervisor_id == ID.ToString()
                           select researcher
                          ).Count();           
            //string sql3 = "select count(*) FROM (select * FROM researcher where supervisor_id="+ ID+") as num";
         
            return results.ToString();
        }
        private static string find_supervisor(int ID, string supervisor_id, List<database.researcher> rlist)
        {
            string str = "";
            //string sql3 = "select given_name,family_name from researcher where id =(select supervisor_id from researcher where id =" + ID + ")";

            //LINQ statement
            IEnumerable<database.researcher> ResearcherQuery =
            from researcher in rlist
            where researcher.id.ToString() == supervisor_id
            select researcher;
            foreach (database.researcher researcher in ResearcherQuery)
            {
                str =  researcher.first_name + ", " + researcher.last_name + "\r\n";
            }
            //MessageBox.Show(str);            
            return str;
        }
        public static DateTime time_convert(string date)
        {
            DateTime dt=DateTime.Parse(date); 
            return dt; 
        }
        public static string showname(int ID, List<database.researcher> rlist)
        {
            string str = "";
            var results = from researcher in rlist
                          where researcher.supervisor_id == ID.ToString()
                          select researcher;

            foreach (database.researcher researcher in results)
            {
                str += researcher.first_name + ", " + researcher.last_name + "\r\n";
            }
            //MessageBox.Show(str);
            return str;


        }

        public static void cumulative(int ID, 
            List<database.researcher> rlist, 
            List<database.publication> publication_list,
            List<database.researcher_publication> researcher_publication_list,
            DataGrid datagrid1)//cumulative
        {
             //LINQ statement
            IEnumerable<database.researcher> ResearcherQuery =
            from researcher in rlist
            where researcher.id == ID
            select researcher;           

            int fromyear;
            int toyear;
            toyear = DateTime.Now.Year;
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            dt.Columns.Add("Cumulative");

            DataRow[] dra = new DataRow[3000];

            foreach (database.researcher researcher in ResearcherQuery)
            {
                fromyear=time_convert(researcher.utas_start).Year;
                //MessageBox.Show(fromyear.ToString());
                
                for(int i=fromyear;i<=toyear;i++)
                {
                    string str2 = ResearcherQuery.Count().ToString();       
                        //str += i+"\r\n";
                        dra[i] = dt.NewRow();

                        dra[i]["Year"] = i;
                        //dr["Year"] = i;
                        dra[i]["Cumulative"] = publication_count(ID, i, researcher_publication_list, publication_list); 
                        dt.Rows.Add(dra[i]);
                }                             
                datagrid1.ItemsSource = dt.DefaultView;
            }        
        }


        private static string prepos(int ID, 
            List<database.researcher> rlist, 
            List<database.position> position_list)
        {
            string curpos="";
            IEnumerable<database.researcher> ResearcherQuery =
            from researcher in rlist
            where researcher.id == ID 
            select researcher;
            foreach (database.researcher researcher in ResearcherQuery)
            {
                curpos = researcher.current_start.ToString();
            }
            //MessageBox.Show(curpos);
            string str="";
            IEnumerable<database.position> PosQuery =
            (from position in position_list
            where position.id == ID && time_convert(position.start)<time_convert(curpos)
            orderby position.start ascending
            select position).Take(1);
            if (PosQuery.Count()!=0)
            {
                foreach (database.position position in PosQuery)
                {                   
                    str = time_convert(position.start).ToShortDateString() + "  " + time_convert(position.end).ToShortDateString() + "  " + levelconvert(position.level);                 
                }
            }
            else
            {
                str = "--";
            }
            return str;
        }
            

    }
}
