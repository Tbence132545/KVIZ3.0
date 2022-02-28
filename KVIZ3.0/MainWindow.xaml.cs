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

namespace KVIZ3._0
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string fajlnev;
        static List<string> temak;
        public MainWindow()
        {
            InitializeComponent();
            temak = new List<string>();
            using (StreamReader sr = new StreamReader("temak.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temak.Add(sr.ReadLine());
                }

            }
            temavalaszto.ItemsSource = temak;
        }
        

        private void start_btn_Click(object sender,RoutedEventArgs e) 
        {
            try { 
            Kviz ablak = new Kviz(fajlnev);
            ablak.Show();
            this.Close();
            }
            catch
            {
                MessageBox.Show("A fájl nem elérhető vagy nem létezik!");
            }
        }
        private void temakivalasztva(object sender, SelectionChangedEventArgs e)
        {
            start_btn.IsEnabled = true;
            fajlnev = (string)temavalaszto.SelectedItem;
        }
    }
}
