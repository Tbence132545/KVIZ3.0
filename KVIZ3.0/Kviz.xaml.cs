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
            int correctAnswers = 0;
            foreach (var x in kerdessor)
            {
                if (x.correctindex == x.kivalasztott)
                {
                    correctAnswers++;
                }
            }
            List<string> nem_megvalaszolt = new List<string>();
            foreach (var x in kerdessor)
            {

                if (x.kivalasztott == -1)
                {
                    nem_megvalaszolt.Add("A(z)" + (kerdessor.IndexOf(x) + 1) + ". kérdés nincs megválaszolva!");
                }
            }
            if (nem_megvalaszolt.Count != 0)
            {
                foreach (string x in nem_megvalaszolt)
                {
                    MessageBox.Show(x);
                }
            }
            else if (correctAnswers != kerdessor.Count)
            {
                MessageBox.Show(Convert.ToString("Hibás válaszok száma: " + Convert.ToString(kerdessor.Count - correctAnswers)));
            }
            else
            {
                MessageBox.Show("Hibátlan megoldás! Visszadobás a főmenübe.");
                MainWindow fomenu = new MainWindow();
                fomenu.Show();
                this.Close();
            }
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
            if (progressCounter + 1 < kerdessor.Count)
            {
                progressCounter++;
                LoadQuestion();
                if (progressCounter >= 1)
                {
                    previous.IsEnabled = true;

                }
                progress.Value++;
            }
            if (progress.Value == kerdessor.Count - 1)
            {
                next.IsEnabled = false;
                result_btn.IsEnabled = true;
                progress_label.Content = progressCounter + 1 + "/" + kerdessor.Count;
            }
            progress_label.Content = progressCounter + 1 + "/" + kerdessor.Count;
        }

        private void previous_Click(object sender, RoutedEventArgs e)
        {
            if (progressCounter - 1 >= 0)
            {

                if (progress.Value - 1 >= 0)
                {
                    next.IsEnabled = true;
                    result_btn.IsEnabled = false;
                    progressCounter--;
                    LoadQuestion();
                    progress.Value--;

                }
                progress_label.Content = progressCounter + 1 + "/" + kerdessor.Count;

            }
            else if (progress.Value == 1)
            {
                previous.IsEnabled = false;
                progress_label.Content = progressCounter + 1 + "/" + kerdessor.Count;
            }
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
