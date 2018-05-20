using System;
using System.Windows;
using System.Windows.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Asign2
{
    class ResearcherList
    {

      
        public static List<database.researcher> generate_R_list()//Restrive data from database and generate researcher list
        {
            List<database.researcher> allreseacher = new List<database.researcher>();
            string connStr = "server=alacritas.cis.utas.edu.au;user=kit206;database=kit206;port=3306;password=kit206;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {                
                conn.Open();             
                string sql = "SELECT * FROM researcher";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        allreseacher.Add(new database.researcher() 
                        { 
                            id = int.Parse(rdr[0].ToString()),
                            type = rdr["type"].ToString(), 
                            first_name = rdr[2].ToString(), 
                            last_name = rdr[3].ToString(), 
                            title = rdr[4].ToString(),
                            unit = rdr["unit"].ToString(),
                            campus = rdr["campus"].ToString(),
                            email = rdr["email"].ToString(),
                            photo = rdr["photo"].ToString(),
                            degree = rdr["degree"].ToString(),
                            supervisor_id = rdr["supervisor_id"].ToString(),
                            level = rdr["level"].ToString(), 
                            utas_start = rdr["utas_start"].ToString(),
                            current_start = rdr["current_start"].ToString(),                             
                        });
                    }                 
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.ToString());
            }
            conn.Close();  
            return allreseacher;
        }


        public static List<database.position> generate_position_list()//Restrive data from database and generate position list
        {
            List<database.position> position = new List<database.position>();
            string connStr = "server=alacritas.cis.utas.edu.au;user=kit206;database=kit206;port=3306;password=kit206;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT * FROM position";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        position.Add(new database.position()
                        {
                            id = int.Parse(rdr[0].ToString()),
                            level = rdr["level"].ToString(),
                            start = rdr["start"].ToString(),
                            end=rdr["end"].ToString(),                            
                        });
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
            return position;
        }


        public static List<database.publication> generate_publication_list()//Restrive data from database and generate publication list
        {
            List<database.publication> publication = new List<database.publication>();
            string connStr = "server=alacritas.cis.utas.edu.au;user=kit206;database=kit206;port=3306;password=kit206;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT * FROM publication";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        publication.Add(new database.publication()
                        {                            
                            doi = rdr["doi"].ToString(),
                            title = rdr["title"].ToString(),
                            authors = rdr["authors"].ToString(),
                            year = rdr["year"].ToString(),
                            type = rdr["type"].ToString(),
                            cite_as = rdr["cite_as"].ToString(),
                            available = rdr["available"].ToString(),                           
                        });
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
            return publication;
        }


        public static List<database.researcher_publication> generate_researcher_publication_list()//Restrive data from database and generate researcher_publication list
        {
            List<database.researcher_publication> researcher_publication = new List<database.researcher_publication>();
            string connStr = "server=alacritas.cis.utas.edu.au;user=kit206;database=kit206;port=3306;password=kit206;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT * FROM researcher_publication";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        researcher_publication.Add(new database.researcher_publication()
                        {                            
                            researcher_id =Convert.ToInt32(rdr["researcher_id"].ToString()),
                            doi = rdr["doi"].ToString(),                           
                        });
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conn.Close();
            return researcher_publication;
        }

        public static void add_R_listitems(ListBox list, List<database.researcher> allreseacher)
        {
            allreseacher.ForEach((x) =>
            {
                list.Items.Add(x.first_name + ", " + x.last_name + " (" + x.title + ")");                
            });

        }
         
    }
}
