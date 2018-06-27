using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace CSV_Consumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string folder = @".\CSV\";
        List<string> headers = new List<string>();
        List<dynamic> data = new List<dynamic>();
        //string testPath = @".\CSV\test2.csv";
        string savePath = @".\log.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        //Load button event
        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            //Clear the listbox
            fileList.Items.Clear();

            //Get file information
            foreach (string file in Directory.EnumerateFiles(folder, "*.csv"))
            {
                fileList.Items.Add(Helper.loadFile(file));
                data = Helper.getData(file);
                Helper.saveData(data, savePath);
            }

            MessageBox.Show("Log saved");
        }
        
        //Cancel button event
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
