using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            plusMinusButton.Click += plusMinusButton_Click;
            percentageButton.Click += percentageButton_Click;
            equalsButton.Click += equalsButton_Click;
        }

        private void equalsButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch(selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.divide(lastNumber, newNumber);
                        break;
                }
            }

            resultLabel.Content = result.ToString();
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = (tempNumber / 100);
                if(lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }

                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void plusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber ))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplyButton)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == divideButton)
                selectedOperator = SelectedOperator.Division;
            if (sender == plusButton)
                selectedOperator = SelectedOperator.Addition;
            if (sender == minusButton)
                selectedOperator = SelectedOperator.Subtraction;

        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
                resultLabel.Content = selectedValue.ToString();
            else
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            
        }

        private void decimalButton_Click(Object sender, RoutedEventArgs e)
        {
            if(resultLabel.Content.ToString().Contains("."))
            {
                // Do Nothing
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }
        public enum SelectedOperator
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        public class SimpleMath
        {
            public static double add(double n1, double n2)
            {
                return n1 + n2; 
            }
            public static double subtract(double n1, double n2)
            {
                return n1 - n2; 
            }
            public static double multiply(double n1, double n2)
            {
                return n1 * n2; 
            }
            public static double divide(double n1, double n2)
            {
                if(n2 == 0)
                {
                    MessageBox.Show("Division by 0 is not supported", "Wrong Operation",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }

                return n1 / n2; 
            }
        }

    }
}