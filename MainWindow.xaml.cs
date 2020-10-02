using System;
using System.Collections.Generic;
using System.IO;
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

namespace CalculatorApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HistoryBox.Text = File.ReadAllText(@"db/bestdb.txt");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text += ((Button)sender).Content;
        }

        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text = "";
        }

        private void ButtonBackSpace_Click(object sender, RoutedEventArgs e)
        {
            if(TextBox1.Text != "")
                TextBox1.Text = TextBox1.Text.Remove(TextBox1.Text.Length-1, 1);
        }

        private void ButtonResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Char> signs1 = new List<char>();
                string[] parts1 = TextBox1.Text.Split('+', '-');
                for (int i = 0; i < TextBox1.Text.Length; i++)
                {
                    if (TextBox1.Text[i] == '+' || TextBox1.Text[i] == '-')
                    {
                        signs1.Add(TextBox1.Text[i]);
                    }
                }

                for (int i = 0; i < parts1.Length; i++)
                {
                    string[] parts2 = parts1[i].Split('*', '/');
                    List<Char> signs2 = new List<char>();
                    for (int j = 0; j < parts1[i].Length; j++)
                    {
                        if (parts1[i][j] == '*' || parts1[i][j] == '/')
                        {
                            signs2.Add(parts1[i][j]);
                        }
                    }

                    //Корень и факториал
                    //for (int f = 0; f < parts2.Length; f++)
                    //{
                    //    if (parts2[f] != "" && parts2[f][parts2[f].Length - 1] == '!' && parts2[f][0] == '√')
                    //    {
                    //        parts2[f] = parts2[f].Remove(parts2[f].Length - 1, 1);
                    //        parts2[f] = parts2[f].Remove(0, 1);
                    //        int factorial = 1;
                    //        int num = int.Parse(parts2[f]);
                    //        for (int k = 1; k <= num; k++)
                    //        {
                    //            factorial *= k;
                    //        }
                    //        parts2[f] = factorial.ToString();
                    //        parts2[f] = Math.Sqrt(double.Parse(parts2[f])).ToString();
                    //    }
                    //}

                    //Факториал
                    for (int f = 0; f < parts2.Length; f++)
                    {
                        if (parts2[f] != "" && parts2[f][parts2[f].Length - 1] == '!')
                        {
                            double factorial = 1;
                            int num = int.Parse(parts2[f].Remove(parts2[f].Length - 1, 1));
                            for (int k = 1; k <= num; k++)
                            {
                                factorial *= k;
                            }
                            parts2[f] = factorial.ToString();
                        }
                    }

                    //Корень
                    for (int k = 0; k < parts2.Length; k++)
                    {
                        if (parts2[k] != "" && parts2[k][0] == '√')
                        {
                            parts2[k] = Math.Sqrt(double.Parse(parts2[k].Remove(0,1))).ToString();
                        }
                    }
                    for (int j = 0; j < signs2.Count; j++)
                    {
                        if (signs2[j] == '*')
                        {
                            parts2[j + 1] = (double.Parse(parts2[j]) * double.Parse(parts2[j + 1])).ToString();
                        }
                        else
                        {
                            parts2[j + 1] = (double.Parse(parts2[j]) / double.Parse(parts2[j + 1])).ToString();
                        }
                    }
                    parts1[i] = parts2[parts2.Length - 1];
                }

                if (TextBox1.Text[0] != '+' && TextBox1.Text[0] != '-')
                {
                    for (int i = 0; i < signs1.Count; i++)
                    {
                        if (signs1[i] == '+')
                        {
                            parts1[i + 1] = (double.Parse(parts1[i]) + double.Parse(parts1[i + 1])).ToString();
                        }
                        else
                        {
                            parts1[i + 1] = (double.Parse(parts1[i]) - double.Parse(parts1[i + 1])).ToString();
                        }
                    }
                }
                else
                {
                    parts1[1] = signs1[0] + parts1[1];
                    for (int i = 1; i < signs1.Count; i++)
                    {
                        if (signs1[i] == '+')
                        {
                            parts1[i + 1] = (double.Parse(parts1[i]) + double.Parse(parts1[i + 1])).ToString();
                        }
                        else
                        {
                            parts1[i + 1] = (double.Parse(parts1[i]) - double.Parse(parts1[i + 1])).ToString();
                        }
                    }
                }

                string result = Math.Round(double.Parse(parts1[parts1.Length - 1]), int.Parse(ComboBox.Text)).ToString();

                File.AppendAllText(@"db/bestdb.txt", TextBox1.Text + " = " + result + "\n");
                HistoryBox.Text = File.ReadAllText(@"db/bestdb.txt");

                TextBox1.Text = result;
            }

            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}
