using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;

namespace Asign2
{
    class generate_report
    {


        public static void researcher_report(int ID,
           List<database.researcher> rlist,
           List<database.publication> publication_list,
           List<database.researcher_publication> researcher_publication_list,
           DataGrid datagrid1,
           DataGrid datagrid2,
           DataGrid datagrid3,
           DataGrid datagrid4)//cumulative
        {
            //LINQ statement
            IEnumerable<database.researcher> ResearcherQuery =
            from researcher in rlist          
            select researcher;

            DataTable dt = new DataTable();
            dt.Columns.Add("Performance Metric");
            dt.Columns.Add("Performance");
            dt.Columns.Add("Researcher");
            dt.Columns.Add("Email");

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Performance Metric");
            dt2.Columns.Add("Performance");
            dt2.Columns.Add("Researcher");
            dt2.Columns.Add("Email");
            DataTable dt3 = new DataTable();
            dt3.Columns.Add("Performance Metric");
            dt3.Columns.Add("Performance");
            dt3.Columns.Add("Researcher");
            dt3.Columns.Add("Email");
            DataTable dt4 = new DataTable();
            dt4.Columns.Add("Performance Metric");
            dt4.Columns.Add("Performance");
            dt4.Columns.Add("Researcher");
            dt4.Columns.Add("Email");

            DataRow[,] dra = new DataRow[4,3000];

            int i=0;


            foreach (database.researcher researcher in ResearcherQuery)
            {

                if (researcher.type == "Staff")
                {
                    double threeavg = ResearcherDetail.threeyear_avg(researcher.id, researcher_publication_list, publication_list);
                    double perdata = ResearcherDetail.performance(threeavg, researcher.level);
                    // string permetric = perfomance_convert(perdata);
                    dra[0, i] = dt.NewRow();
                    dra[1, i] = dt2.NewRow();
                    dra[2, i] = dt3.NewRow();
                    dra[3, i] = dt4.NewRow();
                    if (perdata >= 200)
                    {
                        datagrid1.ItemsSource = dt.DefaultView;
                        dra[0, i]["Researcher"] = researcher.first_name + " " + researcher.last_name;
                        dra[0, i]["Performance Metric"] = "Star Performance";
                        dra[0, i]["Performance"] = perdata + "%";
                        dra[0, i]["Email"] = researcher.email;
                        dt.Rows.Add(dra[0, i]);

                        //datagrid1.Columns[1].SortDirection.Value = "Descending";
                    }
                    else if (perdata >= 110 && perdata < 200)
                    {
                        datagrid2.ItemsSource = dt2.DefaultView;
                        dra[1, i]["Researcher"] = researcher.first_name + " " + researcher.last_name;
                        dra[1, i]["Performance Metric"] = "Meeting Minimum";
                        dra[1, i]["Performance"] = perdata + "%";
                        dra[1, i]["Email"] = researcher.email;
                        dt2.Rows.Add(dra[1, i]);
                    }
                    else if (perdata >= 70 && perdata < 110)
                    {
                        datagrid3.ItemsSource = dt3.DefaultView;
                        dra[2, i]["Researcher"] = researcher.first_name + " " + researcher.last_name;
                        dra[2, i]["Performance Metric"] = "Below Expertation";
                        dra[2, i]["Performance"] = perdata + "%";
                        dra[2, i]["Email"] = researcher.email;
                        dt3.Rows.Add(dra[2, i]);
                    }
                    else if (perdata >= 0 && perdata < 70)
                    {
                        datagrid4.ItemsSource = dt4.DefaultView;
                        dra[3, i]["Researcher"] = researcher.first_name + " " + researcher.last_name;
                        dra[3, i]["Performance Metric"] = "Poor";
                        dra[3, i]["Performance"] = perdata + "%";
                        dra[3, i]["Email"] = researcher.email;               
                                            
                        dt4.Rows.Add(dra[3, i]);
                        
                        
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }

                   

                    

                    i++;

                   

                }
            }



           
         
        }

         

    }
}
