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

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
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
                string result = ExpressionParser.calculate(TextBox1.Text, int.Parse(ComboBox.Text)).ToString();

                File.AppendAllText(@"db/bestdb.txt", TextBox1.Text + " = " + result + "\n");
                HistoryBox.Text = File.ReadAllText(@"db/bestdb.txt");

                TextBox1.Text = result;
            }

            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextBoxRome.Text += ((Button)sender).Content;
        }

        private void ButtonResultRome_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string result = RomanNumeralsConverter.convertNumberToRoman(ExpressionParser.calculate(RomanNumeralsConverter.convertExpression(TextBoxRome.Text), 0).ToString());

                File.AppendAllText(@"db/bestdb.txt", TextBoxRome.Text + " = " + result + "\n");
                HistoryBox.Text = File.ReadAllText(@"db/bestdb.txt");

                TextBoxRome.Text = result;
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void ButtonBackSpace_Click_2(object sender, RoutedEventArgs e)
        {
            if (TextBoxRome.Text != "")
                TextBoxRome.Text = TextBoxRome.Text.Remove(TextBoxRome.Text.Length - 1, 1);
        }

        private void ButtonClear_Click_2(object sender, RoutedEventArgs e)
        {
            TextBoxRome.Text = "";
        }
    }
}
