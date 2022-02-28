using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KVIZ3._0
{
    /// <summary>
    /// Interaction logic for Kviz.xaml
    /// </summary>
    public partial class Kviz : Window
    {
      
        public Kviz(string fajlnev)
        {
            InitializeComponent();
        }

        private void result_btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadQuestion()
        {

        }

        private void option_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fomenu_btn_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
    class Kerdes
    {
        List<string> answers = new List<string>();
        int correctAnswer;
        string kerdes;
        int selected = -1;
        public Kerdes(string sor)
        {

            string[] splitted = sor.Split(';');
            kerdes = splitted[0];
            for (int i = 1; i < splitted.Length - 1; i++)
            {

                answers.Add(splitted[i]);
            }
            correctAnswer = Convert.ToInt32(splitted[5]);

        }
        public string akerdes { get => kerdes; }
        public List<string> valaszok { get => answers; }
        public int kivalasztott { get => selected; set => selected = value; }
        public int correctindex { get => correctAnswer; }
    }
}
