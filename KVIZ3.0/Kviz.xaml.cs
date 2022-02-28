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
using System.IO;
using System.Linq;

namespace KVIZ3._0
{
    /// <summary>
    /// Interaction logic for Kviz.xaml
    /// </summary>
    public partial class Kviz : Window
    {
        List<Kerdes> kerdessor = new List<Kerdes>();
        int progressCounter = 0;
        List<int> veletlenSorrend = new List<int>();
        List<CheckBox> options;
        public Kviz(string fajlnev)
        {
            InitializeComponent();
            options = new List<CheckBox> { optionA, optionB, optionC, optionD };
            using (StreamReader sr = new StreamReader(fajlnev))
            {
                while (!sr.EndOfStream)
                {
                    kerdessor.Add(new Kerdes(sr.ReadLine()));
                }
            }
            progress_label.Content = progressCounter + 1 + "/" + kerdessor.Count;

            Random rnd = new Random();
            for (int i = 0; i < kerdessor.Count; i++)
            {
                int temp = rnd.Next(0, kerdessor.Count);
                while (veletlenSorrend.Contains(temp))
                {
                    temp = rnd.Next(0, kerdessor.Count);
                }
                veletlenSorrend.Add(temp);
            }
            progress.Maximum = kerdessor.Count - 1;
            LoadQuestion();
        }

        private void result_btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void LoadQuestion()
        {
            questionText.Text = kerdessor[veletlenSorrend[progressCounter]].akerdes;
            optionA.Content = kerdessor[veletlenSorrend[progressCounter]].valaszok[0];
            optionB.Content = kerdessor[veletlenSorrend[progressCounter]].valaszok[1];
            optionC.Content = kerdessor[veletlenSorrend[progressCounter]].valaszok[2];
            optionD.Content = kerdessor[veletlenSorrend[progressCounter]].valaszok[3];
            if (kerdessor[veletlenSorrend[progressCounter]].kivalasztott != -1)
            {
                foreach (var x in container.Children.OfType<CheckBox>())
                {
                    x.IsChecked = false;
                }
                options[kerdessor[veletlenSorrend[progressCounter]].kivalasztott].IsChecked = true;


            }
            else
            {
                foreach (var x in container.Children.OfType<CheckBox>())
                {
                    x.IsChecked = false;
                }
            }
        }

        private void option_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var x in container.Children.OfType<CheckBox>())
            {
                x.IsEnabled = false;
                if (x.IsChecked == true)
                {
                    kerdessor[veletlenSorrend[progressCounter]].kivalasztott = options.IndexOf(x);

                    x.IsEnabled = true;
                }
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {

        }
        private void optionuncheck(object sender, RoutedEventArgs e)
        {
            foreach(var x in container.Children.OfType<CheckBox>())
            {
                x.IsEnabled = true;
            }
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
