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

namespace ahp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            showChart();
        }

        private void comMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("button clicked");
            if (comMatrixButton.Background != comMatrixButton.MouseOverBackground)
            {
                comMatrixButton.Background = comMatrixButton.MouseOverBackground;
                this.criGrid.Visibility = Visibility.Hidden;
                this.mtxGrid.Visibility = Visibility.Visible;
                this.mtxGrid.Children.Clear();
                this.mtxGrid.RowDefinitions.Clear();
                this.mtxGrid.ColumnDefinitions.Clear();

                //Grid mtxGrid = new Grid();
                RowDefinition row1 = new RowDefinition(); 
                RowDefinition row2 = new RowDefinition();
                RowDefinition row3 = new RowDefinition();
                RowDefinition row4 = new RowDefinition();
                ColumnDefinition col1 = new ColumnDefinition();
                ColumnDefinition col2 = new ColumnDefinition();
                ColumnDefinition col3 = new ColumnDefinition();
                ColumnDefinition col4 = new ColumnDefinition();

                //row1.Height = new GridLength(3, GridUnitType.Star);
                //row2.Height = new GridLength(4, GridUnitType.Star);
                //row3.Height = new GridLength(3, GridUnitType.Star);
                //col.Width = new GridLength(10, GridUnitType.Star);

                this.mtxGrid.RowDefinitions.Add(row1);
                this.mtxGrid.RowDefinitions.Add(row2);
                this.mtxGrid.RowDefinitions.Add(row3);
                this.mtxGrid.RowDefinitions.Add(row4);
                this.mtxGrid.ColumnDefinitions.Add(col1);
                this.mtxGrid.ColumnDefinitions.Add(col2);
                this.mtxGrid.ColumnDefinitions.Add(col3);
                this.mtxGrid.ColumnDefinitions.Add(col4);

                //this.ribbon.Visibility = Visibility.Hidden;
                


                TextBlock text01 = new TextBlock();
                text01.Text = "Criteria1";
                //text01.VerticalAlignment = VerticalAlignment.Center;
                //text01.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text02 = new TextBlock();
                text02.Text = "Criteria2";
                //text02.VerticalAlignment = VerticalAlignment.Center;
                //text02.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text03 = new TextBlock();
                text03.Text = "Criteria3";
               // text03.VerticalAlignment = VerticalAlignment.Center;
                //text03.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock text10 = new TextBlock();
                text10.Text = "Criteria1";
                //text10.VerticalAlignment = VerticalAlignment.Center;
                //text10.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text20 = new TextBlock();
                text20.Text = "Criteria2";
                //text20.VerticalAlignment = VerticalAlignment.Center;
                //text20.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text30 = new TextBlock();
                text30.Text = "Criteria3";
                //text30.VerticalAlignment = VerticalAlignment.Center;
                //text30.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock text11 = new TextBlock();
                text11.Text = "1";
               // text11.VerticalAlignment = VerticalAlignment.Center;
               // text11.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text22 = new TextBlock();
                text22.Text = "1";
               // text22.VerticalAlignment = VerticalAlignment.Center;
               // text22.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text33 = new TextBlock();
                text33.Text = "1";
               // text33.VerticalAlignment = VerticalAlignment.Center;
               // text33.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock text21 = new TextBlock();
                text21.Text = "1/2";
               // text21.VerticalAlignment = VerticalAlignment.Center;
               // text21.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text31 = new TextBlock();
                text31.Text = "1/3";
                //text31.VerticalAlignment = VerticalAlignment.Center;
               // text31.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock text32 = new TextBlock();
                text32.Text = "5";
                //text32.VerticalAlignment = VerticalAlignment.Center;
               // text32.HorizontalAlignment = HorizontalAlignment.Center;

                TextBox text12 = new TextBox();
                //text12.VerticalAlignment = VerticalAlignment.Center;
                //text12.HorizontalAlignment = HorizontalAlignment.Center;
                TextBox text13 = new TextBox();
                //text13.VerticalAlignment = VerticalAlignment.Center;
                //text13.HorizontalAlignment = HorizontalAlignment.Center;
                TextBox text23 = new TextBox();
                //text23.VerticalAlignment = VerticalAlignment.Center;
                //text23.HorizontalAlignment = HorizontalAlignment.Center;

                this.mtxGrid.Children.Add(text01);
                Grid.SetRow(text01, 0);
                Grid.SetColumn(text01, 1);

                this.mtxGrid.Children.Add(text02);
                Grid.SetRow(text02, 0);
                Grid.SetColumn(text02, 2);

                this.mtxGrid.Children.Add(text03);
                Grid.SetRow(text03, 0);
                Grid.SetColumn(text03, 3);

                this.mtxGrid.Children.Add(text10);
                Grid.SetRow(text10, 1);
                Grid.SetColumn(text10, 0);

                this.mtxGrid.Children.Add(text20);
                Grid.SetRow(text20, 2);
                Grid.SetColumn(text20, 0);

                this.mtxGrid.Children.Add(text30);
                Grid.SetRow(text30, 3);
                Grid.SetColumn(text30, 0);

                this.mtxGrid.Children.Add(text11);
                Grid.SetRow(text11, 1);
                Grid.SetColumn(text11, 1);

                this.mtxGrid.Children.Add(text22);
                Grid.SetRow(text22, 2);
                Grid.SetColumn(text22, 2);

                this.mtxGrid.Children.Add(text33);
                Grid.SetRow(text33, 3);
                Grid.SetColumn(text33, 3);

                this.mtxGrid.Children.Add(text21);
                Grid.SetRow(text21, 2);
                Grid.SetColumn(text21, 1);

                this.mtxGrid.Children.Add(text31);
                Grid.SetRow(text31, 3);
                Grid.SetColumn(text31, 1);

                this.mtxGrid.Children.Add(text32);
                Grid.SetRow(text32, 3);
                Grid.SetColumn(text32, 2);

                this.mtxGrid.Children.Add(text12);
                Grid.SetRow(text12, 1);
                Grid.SetColumn(text12, 2);

                this.mtxGrid.Children.Add(text13);
                Grid.SetRow(text13, 1);
                Grid.SetColumn(text13, 3);

                this.mtxGrid.Children.Add(text23);
                Grid.SetRow(text23, 2);
                Grid.SetColumn(text23, 3);
            }
            else
            {
                comMatrixButton.Background = Brushes.Transparent;
                this.criGrid.Visibility = Visibility.Visible;
                this.mtxGrid.Visibility = Visibility.Hidden;
            }
        }

        private void showChart()
        {
            List<KeyValuePair<string, int>> MyValue = new List<KeyValuePair<string, int>>();
            MyValue.Add(new KeyValuePair<string, int>("Alternative1", 10));
            MyValue.Add(new KeyValuePair<string, int>("Alternative3", 30));
            MyValue.Add(new KeyValuePair<string, int>("Alternative2", 60));

            BarChart1.DataContext = MyValue;
        }
    }
}
