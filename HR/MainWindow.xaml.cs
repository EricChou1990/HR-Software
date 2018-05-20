using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Data;

namespace Asign2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : DevExpress.Xpf.Core.DXWindow
    {
        static List<database.researcher> rlist = (ResearcherList.generate_R_list());//Generate Researcher list
        static List<database.position> position_list = (ResearcherList.generate_position_list());//Generate position list
        static List<database.publication> publication_list = (ResearcherList.generate_publication_list());//Generate publication list
        static List<database.researcher_publication> researcher_publication_list = (ResearcherList.generate_researcher_publication_list());//Generate researcher_publication list
        static List<database.publication> P_list = new List<database.publication>();
        static List<database.researcher> orgin_rlist = (ResearcherList.generate_R_list());
        static List<database.publication> Orgin_list = new List<database.publication>();
        static int global_id;
        static bool invert_control;
        static string selected_level;
        public MainWindow()
        {
            InitializeComponent();

           // new list1<database.{Name=“Anuska Sharama”,Age=21,Email=“anuska@xyz.com”,Image=“anuska.jpg”},

          //ResearcherList.add_R_listitems(list1, rlist); //Traverse all the items of researcher list and add into Listbox  

          // list1.ItemsSource = rlist;

            list1.ItemsSource = rlist;
            
            KeyDown += new KeyEventHandler(sc1_KeyDown);

            for (int i = 1900; i <= 2100; i++)
            {
                //int year = 1900;
                cbo1.Items.Add(i.ToString());
                cbo2.Items.Add(i.ToString());

            }
            //cbo1.
            cbo1.SelectedItem = "2000";
            //cbo1.SelectionLength = 5;
            cbo1.PopupMaxHeight = 30;
            //cbo1.MinHeight = 20;
            cbo1.PopupMaxHeight = 100;


            cbo2.SelectedItem = "2015";
            //cbo1.SelectionLength = 5;
            cbo2.PopupMaxHeight = 30;
            //cbo1.MinHeight = 20;
            cbo2.PopupMaxHeight = 100;

            
          
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(lbi.SelectedIndex.ToString());

           // list1.Items.Clear();

            if (lbi.SelectedIndex == 0)
            {

                rlist = orgin_rlist;
                list1.ItemsSource = rlist;
                list1.Items.Refresh();
            }
            else if (lbi.SelectedIndex == 1)
            {
                rlist = Filter_ByName_AllStudents(orgin_rlist);
                list1.ItemsSource = rlist;
                list1.Items.Refresh();
            }
            else
            {
                switch (lbi.SelectedIndex)
                {

                    case 2:
                        selected_level = "A";                      //postdoc
                        break;
                    case 3:
                        selected_level = "B";                  //lecture
                        break;
                    case 4:
                        selected_level = "C";                            //senior lecture
                        break;
                    case 5:
                        selected_level = "D";                            //associate professor
                        break;
                    case 6:
                        selected_level = "E";                            //professor
                        break;
                    default:
                        selected_level = "unknown info";
                        break;
                }

                rlist = Filter_ByName(selected_level, orgin_rlist);
                list1.ItemsSource = rlist;
                list1.Items.Refresh();
            }

        }

        private static List<database.researcher> Filter_ByName(String Filter_level, List<database.researcher> rlist)             //fliter research_list by selected level
        {
            //LINQ statement
            var results = from researcher in rlist
                          where researcher.level == Filter_level
                          select researcher;
            List<database.researcher> newreseacher = new List<database.researcher>();

            foreach (database.researcher researcher in results)
            {
                newreseacher.Add(researcher);
            }

            return newreseacher;
        }



        private static List<database.researcher> Filter_ByName_AllStudents(List<database.researcher> rlist)
        {
            //LINQ statement
            var results = from researcher in rlist
                          where researcher.type == "Student"
                          select researcher;
            List<database.researcher> newreseacher = new List<database.researcher>();

            foreach (database.researcher researcher in results)
            {
                newreseacher.Add(researcher);
            }

            return newreseacher;
        }


         private static List<database.researcher>  Search_ByName(String search_name, List<database.researcher> rlist)             //fliter research_list by selected level
        {
            
             //LINQ statement
            var results = from researcher in rlist                       
                         // where researcher.last_name.StartsWith(search_name) 
                          where researcher.first_name.ToLower().Contains(search_name.ToLower()) || researcher.last_name.ToLower().Contains(search_name.ToLower())
                        //  where researcher.f
                          select researcher;
            List<database.researcher> newreseacher = new List<database.researcher>();

            foreach (database.researcher researcher in results)
            {
                newreseacher.Add(researcher);
            }

            return newreseacher;
        }
       
        private void ListBox_SelectedIndexChanged(object sender, RoutedEventArgs e)//event of researcher list
        {

            //disable 'showname'btn,'cumulative count'btn and datagrid before user select a record
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            datagrid1.IsEnabled = true;

            list2.ItemsSource = P_list;
            //r_details.GotFocus = true;


            //filter function bug here
            if (list1.SelectedIndex != -1)//if user selected a listitem and use filter function,application will be crushed
            {
                BitmapImage b = new BitmapImage();  //Generate avatar
                b.BeginInit();
                b.UriSource = new Uri(@rlist[list1.SelectedIndex].photo.ToString());
                b.EndInit();
                avatar.Source = b;
                //show details of specific researcher
                ContentControl1.Content = ResearcherDetail.showdetail(rlist[list1.SelectedIndex].id, rlist, position_list, researcher_publication_list, publication_list, btn1);
                datagrid1.ItemsSource = null;//reset datagrid
                tbk_name.Text = "";//reset show name


                global_id = rlist[list1.SelectedIndex].id;//set grobal value for id              

                P_list = PublicationList.generate_P_list(rlist[list1.SelectedIndex].id, researcher_publication_list, publication_list);
                Orgin_list = P_list;
                list2.ItemsSource = P_list;
                list2.Items.Refresh();

                tab_m.SelectedIndex = 1;//auto jump to tab 2
            }
        }


        private void ListBox2_SelectedIndexChanged(object sender, RoutedEventArgs e)//event of publication list
        {
            List<database.researcher> rlist = (ResearcherList.generate_R_list());

            if (list2.SelectedIndex != -1)
            {

                // MessageBox.Show(list2.SelectedIndex.ToString());

                tbk1.Text = PublicationDetail.Pdetail(P_list[list2.SelectedIndex].doi.ToString(), publication_list);//bug fixed  
               
                tab_m.SelectedIndex = 3;//auto jump to tab 2
            }           
        }

        private void invert_Click(object sender, RoutedEventArgs e)
        {



            // list2.Items.Refresh();
            if (list2.Items.Count > 0)//listbox is not empty
            {
                List<database.researcher> rlist = (ResearcherList.generate_R_list());
                





                // PublicationList.additemt_p_listbox(list2, tmp_list); 
                //P_list = tmp_list;
                //list2.ItemsSource = P_list;
                // invert_control = false;

                if (invert_control == true)//click once invert,click twice resume
                {
                    //list2.Items.Clear();
                    //tmp_list = PublicationList.sort_P_list(P_list, Convert.ToInt32(cbo1.SelectedItem), Convert.ToInt32(cbo2.SelectedItem), true);                     
                    // PublicationList.additemt_p_listbox(list2, tmp_list); 

                    P_list = PublicationList.sort_P_list(P_list, 1900, 2100, true);
                    // tmp_list = P_list;
                    list2.ItemsSource = P_list;
                    list2.Items.Refresh();
                    // P_list = tmp_list;
                    invert_control = false;
                }
                else
                {
                    //list2.Items.Clear();
                    P_list = PublicationList.sort_P_list(P_list, 1900, 2100, false);
                    // PublicationList.additemt_p_listbox(list2, tmp_list);

                    //  tmp_list = P_list;
                    list2.ItemsSource = P_list;
                    list2.Items.Refresh();
                    invert_control = true;
                }
            }
            else
            {
                MessageBox.Show("Publication list is empty ");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbk_name.Text = ResearcherDetail.showname(global_id, rlist);            
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
             ResearcherDetail. cumulative(global_id, rlist, publication_list, researcher_publication_list,datagrid1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (list2.Items.Count != 0)
            {
                if (Convert.ToInt32(cbo2.SelectedItem) >= Convert.ToInt32(cbo1.SelectedItem))
                {
                    P_list = PublicationList.sort_P_list(Orgin_list, Convert.ToInt32(cbo1.SelectedItem), Convert.ToInt32(cbo2.SelectedItem), false);

                    list2.ItemsSource = P_list;
                    list2.Items.Refresh();
                    // list2.Items.Clear();
                    //PublicationList.additemt_p_listbox(list2, tmp_list);
                }
                else
                {
                    MessageBox.Show("Year range error,later year mus larger than former year!");
                }
            }
            else
            {
                MessageBox.Show("List is empty!");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Report reportform = new Report();            
            generate_report.researcher_report(global_id,
            rlist,
            publication_list,
            researcher_publication_list,
            reportform.datagrid2, 
            reportform.datagrid3, 
            reportform.datagrid4, 
            reportform.datagrid5);

            reportform.Show();           

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (sc1.SearchText != null)
            // MessageBox.Show(sc1.SearchText.ToString());
            // list1.Items.Clear();
            // ResearcherList.add_R_listitems(list1, Search_ByName(sc1.SearchText.ToString(), rlist)); 
            {
                List<database.researcher> tmp_rlist = new List<database.researcher>();
                rlist = Search_ByName(sc1.SearchText.ToString(), rlist);
                list1.ItemsSource = rlist;
                list1.Items.Refresh();
                //var obj =rlist.Find(x => x.first_name == "john");

            }
            else
            {
                MessageBox.Show("Please input search string");

            }
            //MessageBox.Show("Found in list"); 

           
        }

        private void sc1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sc1.SearchText != null)
                // MessageBox.Show(sc1.SearchText.ToString());
                // list1.Items.Clear();
                // ResearcherList.add_R_listitems(list1, Search_ByName(sc1.SearchText.ToString(), rlist)); 
                {
                    List<database.researcher> tmp_rlist = new List<database.researcher>();
                    rlist = Search_ByName(sc1.SearchText.ToString(), rlist);
                    list1.ItemsSource = rlist;
                    list1.Items.Refresh();
                    //var obj =rlist.Find(x => x.first_name == "john");

                }
                else
                {
                    MessageBox.Show("Please input search string");

                }

            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); //shut down application
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // P_list.Clear();
            P_list = PublicationList.sort_P_list(Orgin_list, 1900, 2100, true);

            list2.ItemsSource = P_list;
            list2.Items.Refresh();
        }

        
      
        
       // ResearcherList.add_R_listitems(list1, Filter_ByName(selected_level, rlist));  
       
        
    }
}
