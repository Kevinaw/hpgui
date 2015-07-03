﻿using System;
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

using System.Windows.Threading;
using System.Windows.Media.Effects;

namespace ahp
{
    /// <summary>
    /// Interaction logic for MainWindow_2.xaml
    /// </summary>
    /// 
    public partial class MainWindow_2 : Page
    {
        //string tmpFile;
        private Boolean isEditingAl = false;
        private int editingItemIndexAl;
        private Boolean isEditingCr = false;
        private int editingItemIndexCr;

        List<string> listAlt = new List<string>();
        List<string> listCr = new List<string>();

        private double[,] mtx;
        private static string[] strValues = { "1/9", "1/8", "1/7", "1/6", "1/5", "1/4", "1/3", "1/2", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private static double[] dblValues = { 0.1111111, 0.125, 0.1428571, 0.1666666, 0.2, 0.25, 0.3333333, 0.5, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public MainWindow_2()
        {
            InitializeComponent();

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //drawAhpHierarchy();
            addItemBoxAl_LostFocus();
            addItemBoxCr_LostFocus();
        }

        private void addBtnAl_Click(object sender, RoutedEventArgs e)
        {
            TextBox newTxtBx = new TextBox();
            newTxtBx.Width = contentLbxAl.ActualWidth - 10;
            newTxtBx.Name = "addItemBoxAl";

            // if the last item is textbox, don't show the box            
            if (contentLbxAl.Items.Count != 0)
            {
                int index;
                object lastItem;
                lastItem = contentLbxAl.Items.GetItemAt(contentLbxAl.Items.Count - 1);// as ListBoxItem;

                if (lastItem.GetType().ToString() != "System.Windows.Controls.TextBox")
                {
                    index = contentLbxAl.Items.Add(newTxtBx);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                (Action)(() => { Keyboard.Focus(newTxtBx); }));
                }
            }
            else
                contentLbxAl.Items.Add(newTxtBx);
        }

        private void addItemBoxAl_LostFocus(/*object sender, RoutedEventArgs e*/)
        {
            if (contentLbxAl.Items.Count != 0)
            {
                TextBox lastItem;
                lastItem = contentLbxAl.Items.GetItemAt(contentLbxAl.Items.Count - 1) as TextBox;

                if (lastItem != null && lastItem.Text != "")
                {
                    if (listAlt.Exists(x => x == lastItem.Text))
                    {
                        MessageBox.Show("duplicate alternative name!");
                        return;
                    }
                    listAlt.Add(lastItem.Text);
                    drawAhpHierarchy();


                    // delete the textbox
                    contentLbxAl.Items.RemoveAt(contentLbxAl.Items.Count - 1);

                    // add grid with delete button
                    ListBoxItem newItem = new ListBoxItem();

                    Grid newGrid = new Grid();
                    ColumnDefinition col1 = new ColumnDefinition();
                    ColumnDefinition col2 = new ColumnDefinition();
                    col1.Width = new GridLength(contentLbxAl.ActualWidth * 5 / 8);
                    col1.Width = new GridLength(contentLbxAl.ActualWidth * 3 / 8);
                    newGrid.ColumnDefinitions.Add(col1);
                    newGrid.ColumnDefinitions.Add(col2);

                    TextBlock newText = new TextBlock();
                    newText.Text = lastItem.Text;
                    //newText.AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(ListItem_DoubleClick));
                    newText.MouseDown += new MouseButtonEventHandler(ListItemAl_DoubleClick);

                    Button newBtn = new Button();
                    newBtn.Name = "deleteBtnAl" + contentLbxAl.Items.Count.ToString();
                    newBtn.Width = 22;
                    newBtn.Height = 22;
                    newBtn.Cursor = Cursors.Hand;
                    newBtn.ToolTip = "delete alternative";
                    newBtn.Click += new RoutedEventHandler(deleteBtnAl_OnClick);

                    ImageBrush newB = new ImageBrush();
                    newB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/delete1.png"));
                    newBtn.Background = newB;

                    newGrid.Children.Add(newText);
                    newGrid.Children.Add(newBtn);

                    Grid.SetColumn(newBtn, 1);

                    newItem.Content = newGrid;

                    contentLbxAl.Items.Add(newItem);
                    RegisterName(newBtn.Name, newBtn);
                }
                else if (lastItem != null && lastItem.Text == "")
                    contentLbxAl.Items.RemoveAt(contentLbxAl.Items.Count - 1);
            }

            if (isEditingAl == true)
            {

                ListBoxItem lbi = contentLbxAl.Items.GetItemAt(editingItemIndexAl) as ListBoxItem;
                TextBox txt = (lbi.Content as Grid).Children[1] as TextBox;
                if (txt == null)
                    txt = (lbi.Content as Grid).Children[0] as TextBox;

                if (listAlt.Exists(x => x == txt.Text))
                {
                    MessageBox.Show("duplicate alternative name!");
                    return;
                }
                //listAlt.Remove(txt.Text);
                listAlt.Add(txt.Text);
                drawAhpHierarchy();

                TextBlock txb = new TextBlock();
                txb.Text = txt.Text;

                (lbi.Content as Grid).Children.Remove(txt);
                (lbi.Content as Grid).Children.Add(txb);

                isEditingAl = false;
            }
        }

        private void deleteBtnAl_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int i;
                int idx;
                Button btn1;

                idx = Convert.ToInt16(btn.Name.Substring(11));

                ListBoxItem lbi = contentLbxAl.Items.GetItemAt(idx) as ListBoxItem;
                TextBlock txt = (lbi.Content as Grid).Children[0] as TextBlock;
                if (txt == null)
                    txt = (lbi.Content as Grid).Children[1] as TextBlock;
                listAlt.Remove(txt.Text);
                drawAhpHierarchy();
                // delete this item according the index
                contentLbxAl.Items.RemoveAt(idx);
                UnregisterName(btn.Name);

                if (idx != contentLbxAl.Items.Count)
                {
                    for (i = idx + 1; i < contentLbxAl.Items.Count + 1; i++)
                    {
                        btn1 = FindName("deleteBtnAl" + Convert.ToString(i)) as Button;
                        UnregisterName(btn1.Name);
                        btn1.Name = "deleteBtnAl" + (i - 1).ToString();
                        RegisterName(btn1.Name, btn1);
                    }
                }

            }
        }

        private void ListItemAl_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // double click, trigger this editing event
            if (e.ClickCount != 2)
                return;

            TextBlock txb = sender as TextBlock;
            if (txb == null)
                return;

            TextBox txb1 = new TextBox();
            txb1.Text = txb.Text;

            listAlt.Remove(txb.Text);
            drawAhpHierarchy();

            Grid grd = txb.Parent as Grid;
            grd.Children.Remove(txb);
            grd.Children.Add(txb1);

            isEditingAl = true;
            ListBoxItem lbi = grd.Parent as ListBoxItem;
            editingItemIndexAl = contentLbxAl.Items.IndexOf(lbi);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                (Action)(() => { Keyboard.Focus(txb1); }));

        }

        private void addBtnCr_Click(object sender, RoutedEventArgs e)
        {
            TextBox newTxtBx = new TextBox();
            newTxtBx.Width = contentLbxCr.ActualWidth - 10;
            newTxtBx.Name = "addItemBoxCr";

            // if the last item is textbox, don't show the box            
            if (contentLbxCr.Items.Count != 0)
            {
                int index;
                object lastItem;
                lastItem = contentLbxCr.Items.GetItemAt(contentLbxCr.Items.Count - 1);// as ListBoxItem;

                if (lastItem.GetType().ToString() != "System.Windows.Controls.TextBox")
                {
                    index = contentLbxCr.Items.Add(newTxtBx);
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                (Action)(() => { Keyboard.Focus(newTxtBx); }));
                }
            }
            else
                contentLbxCr.Items.Add(newTxtBx);
        }

        private void addItemBoxCr_LostFocus(/*object sender, RoutedEventArgs e*/)
        {
            if (contentLbxCr.Items.Count != 0)
            {
                TextBox lastItem;
                lastItem = contentLbxCr.Items.GetItemAt(contentLbxCr.Items.Count - 1) as TextBox;

                if (lastItem != null && lastItem.Text != "")
                {
                    if (listCr.Exists(x => x == lastItem.Text))
                    {
                        MessageBox.Show("duplicate criteria name!");
                        return;
                    }
                    listCr.Add(lastItem.Text);
                    drawAhpHierarchy();


                    // delete the textbox
                    contentLbxCr.Items.RemoveAt(contentLbxCr.Items.Count - 1);

                    // add grid with delete button
                    ListBoxItem newItem = new ListBoxItem();

                    Grid newGrid = new Grid();
                    ColumnDefinition col1 = new ColumnDefinition();
                    ColumnDefinition col2 = new ColumnDefinition();
                    col1.Width = new GridLength(contentLbxCr.ActualWidth * 5 / 8);
                    col1.Width = new GridLength(contentLbxCr.ActualWidth * 3 / 8);
                    newGrid.ColumnDefinitions.Add(col1);
                    newGrid.ColumnDefinitions.Add(col2);

                    TextBlock newText = new TextBlock();
                    newText.Text = lastItem.Text;
                    //newText.AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(ListItem_DoubleClick));
                    newText.MouseDown += new MouseButtonEventHandler(ListItemCr_DoubleClick);

                    Button newBtn = new Button();
                    newBtn.Name = "deleteBtnCr" + contentLbxCr.Items.Count.ToString();
                    newBtn.Width = 22;
                    newBtn.Height = 22;
                    newBtn.Cursor = Cursors.Hand;
                    newBtn.ToolTip = "delete criteria";
                    newBtn.Click += new RoutedEventHandler(deleteBtnCr_OnClick);

                    ImageBrush newB = new ImageBrush();
                    newB.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/delete1.png"));
                    newBtn.Background = newB;

                    newGrid.Children.Add(newText);
                    newGrid.Children.Add(newBtn);

                    Grid.SetColumn(newBtn, 1);

                    newItem.Content = newGrid;

                    contentLbxCr.Items.Add(newItem);
                    RegisterName(newBtn.Name, newBtn);
                }
                else if (lastItem != null && lastItem.Text == "")
                    contentLbxCr.Items.RemoveAt(contentLbxCr.Items.Count - 1);
            }

            if (isEditingCr == true)
            {

                ListBoxItem lbi = contentLbxCr.Items.GetItemAt(editingItemIndexCr) as ListBoxItem;
                TextBox txt = (lbi.Content as Grid).Children[1] as TextBox;
                if (txt == null)
                    txt = (lbi.Content as Grid).Children[0] as TextBox;

                if (listCr.Exists(x => x == txt.Text))
                {
                    MessageBox.Show("duplicate criteria name!");
                    return;
                }
                //listAlt.Remove(txt.Text);
                listCr.Add(txt.Text);
                drawAhpHierarchy();

                TextBlock txb = new TextBlock();
                txb.Text = txt.Text;

                (lbi.Content as Grid).Children.Remove(txt);
                (lbi.Content as Grid).Children.Add(txb);

                isEditingCr = false;
            }
        }

        private void deleteBtnCr_OnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                int i;
                int idx;
                Button btn1;

                idx = Convert.ToInt16(btn.Name.Substring(11));

                ListBoxItem lbi = contentLbxCr.Items.GetItemAt(idx) as ListBoxItem;
                TextBlock txt = (lbi.Content as Grid).Children[0] as TextBlock;
                if (txt == null)
                    txt = (lbi.Content as Grid).Children[1] as TextBlock;
                listCr.Remove(txt.Text);
                drawAhpHierarchy();
                // delete this item according the index
                contentLbxCr.Items.RemoveAt(idx);
                UnregisterName(btn.Name);

                if (idx != contentLbxCr.Items.Count)
                {
                    for (i = idx + 1; i < contentLbxCr.Items.Count + 1; i++)
                    {
                        btn1 = FindName("deleteBtnCr" + Convert.ToString(i)) as Button;
                        UnregisterName(btn1.Name);
                        btn1.Name = "deleteBtnCr" + (i - 1).ToString();
                        RegisterName(btn1.Name, btn1);
                    }
                }

            }
        }

        private void ListItemCr_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // double click, trigger this editing event
            if (e.ClickCount != 2)
                return;

            TextBlock txb = sender as TextBlock;
            if (txb == null)
                return;

            TextBox txb1 = new TextBox();
            txb1.Text = txb.Text;

            listCr.Remove(txb.Text);
            drawAhpHierarchy();

            Grid grd = txb.Parent as Grid;
            grd.Children.Remove(txb);
            grd.Children.Add(txb1);

            isEditingCr = true;
            ListBoxItem lbi = grd.Parent as ListBoxItem;
            editingItemIndexCr = contentLbxCr.Items.IndexOf(lbi);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                (Action)(() => { Keyboard.Focus(txb1); }));

        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Button btnUp = sender as Button;

            if (btnUp == null)
                return;

            string strN = btnUp.Name;
            int intR, intC;
            intR = Convert.ToInt16(strN.Substring(5, 1));
            intC = Convert.ToInt16(strN.Substring(6, 1));

            double dbl = mtx[intR, intC];
            int idx = Array.FindIndex(dblValues, x => x == dbl);

            if (idx == -1)
            { MessageBox.Show("not found!"); return; }

            if (idx == dblValues.Length - 1)
                return;
            else
                idx += 1;

            mtx[intR, intC] = dblValues[idx];
            mtx[intC, intR] = dblValues[dblValues.Length - 1 - idx];

            (FindName("txtNum" + intR.ToString() + intC.ToString()) as TextBox).Text = strValues[idx];
            (FindName("txtBlk" + intC.ToString() + intR.ToString()) as TextBlock).Text = strValues[strValues.Length - 1 - idx];
        }

        private void cmdDn_Click(object sender, RoutedEventArgs e)
        {
            Button btnDn = sender as Button;

            if (btnDn == null)
                return;

            string strN = btnDn.Name;
            int intR, intC;
            intR = Convert.ToInt16(strN.Substring(5, 1));
            intC = Convert.ToInt16(strN.Substring(6, 1));

            double dbl = mtx[intR, intC];
            int idx = Array.FindIndex(dblValues, x => x == dbl);

            if (idx == -1)
            { MessageBox.Show("not found!"); return; }

            if (idx == 0)
                return;
            else
                idx -= 1;

            mtx[intR, intC] = dblValues[idx];
            mtx[intC, intR] = dblValues[dblValues.Length - 1 - idx];

            (FindName("txtNum" + intR.ToString() + intC.ToString()) as TextBox).Text = strValues[idx];
            (FindName("txtBlk" + intC.ToString() + intR.ToString()) as TextBlock).Text = strValues[strValues.Length - 1 - idx];

        }

        // calculate weights and CR
        private StructEvalResult ahp_eval(double[,] matrix)
        {
            int n = (int)Math.Sqrt(matrix.Length);
            int row = n;
            int column = n;
            double[] mul_column = new double[row];
            double[] rot_column = new double[row];
            double[] w = new double[row];
            double[] aw = new double[row];
            double[] aw_w = new double[row];
            double CI;
            double CR;
            double sum = 0;
            double average;
            double[] RI = { 0.0, 0.0, 0.0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45, 1.49 };

            for (int i = 0; i < row; i++)
            {
                mul_column[i] = 1;
                for (int j = 0; j < column; j++)
                    mul_column[i] *= matrix[i, j];
                rot_column[i] = Math.Pow(mul_column[i], 1.0 / n);
                sum += rot_column[i];
            }

            // get normalized weight
            for (int i = 0; i < row; i++)
            {
                w[i] = rot_column[i] / sum;
            }

            sum = 0;
            // get aw and aww
            for (int i = 0; i < row; i++)
            {
                aw[i] = 0;
                for (int j = 0; j < column; j++)
                    aw[i] += matrix[i, j] * w[j];

                aw_w[i] = aw[i] / w[i];
                sum += aw_w[i];
            }
            average = sum / row;

            CI = (average - n) / (n - 1);
            CR = CI / RI[n];

            StructEvalResult result;
            result.w = w;
            result.CR = CR;
            //double[] result = new double[n + 1];
            //w.CopyTo(result, 0);
            //result[n] = CR;
            return result;
        }

        private void tabMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (e.AddedItems.Count == 0)
                return;
            TabItem selectedItem = e.AddedItems[0] as TabItem;
            if (selectedItem != null && selectedItem.Header.ToString() == "Pairwise Comparison")
            {
                DrawCriteriaCompMtx();
            }
            */

        }

        private void tabPwC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedItem = e.AddedItems[0] as TabItem;
            if (selectedItem != null && selectedItem.Header.ToString() == "Criteria vs. Goal")
            {
                //MessageBox.Show("Criteria vs. Goal");
            }

        }

        // draw criteria vs goal comparison matrix
        private void DrawCriteriaCompMtx()
        {
            int i, j;
            mtxBdr.Visibility = Visibility.Visible;
            //this.mtxGrid.Visibility = Visibility.Visible;
            mtxGrid.Children.Clear();
            mtxGrid.RowDefinitions.Clear();
            mtxGrid.ColumnDefinitions.Clear();

            if (listCr.Count == 0)
                return;

            if (mtx != null)
            {
                // unregister all the element names
                for (i = 0; i < Math.Sqrt(mtx.Length) - 1; i++)
                    for (j = i + 1; j < Math.Sqrt(mtx.Length); j++)
                    {
                        UnregisterName("txtNum" + i.ToString() + j.ToString());
                        UnregisterName("txtBlk" + j.ToString() + i.ToString());
                    }
            }

            if (mtx == null)
            {
                mtx = new double[listCr.Count, listCr.Count];
                for (i = 0; i < listCr.Count; i++)
                    for (j = 0; j < listCr.Count; j++)
                        mtx[i, j] = 1;
            }
            // delete one or more criteria
            else if (mtx.Length > listCr.Count * listCr.Count)
            {
                double[,] tmp = mtx;
                mtx = new double[listCr.Count, listCr.Count];

                for (i = 0; i < listCr.Count; i++)
                    for (j = 0; j < listCr.Count; j++)
                        mtx[i, j] = tmp[i, j];
            }
            // add one or more criteria
            else if (mtx.Length < listCr.Count * listCr.Count)
            {
                double[,] tmp = mtx;
                mtx = new double[listCr.Count, listCr.Count];
                for (i = 0; i < listCr.Count; i++)
                    for (j = 0; j < listCr.Count; j++)
                        mtx[i, j] = 1;

                for (i = 0; i < Math.Sqrt(tmp.Length); i++)
                    for (j = 0; j < Math.Sqrt(tmp.Length); j++)
                        mtx[i, j] = tmp[i, j];
            }
            else // no change to criteria
                ;


            double gridWidth = 90;
            double gridHeight = 60;
            (mtxGrid.Parent as Border).HorizontalAlignment = HorizontalAlignment.Center;
            (mtxGrid.Parent as Border).VerticalAlignment = VerticalAlignment.Center;
            double x = tabPwC.ActualWidth;
            double y = tabPwC.ActualHeight;
            if (x >= (listCr.Count + 1) * gridWidth)
            {
                for (i = 0; i < listCr.Count + 1; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(gridHeight);
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = new GridLength(gridWidth);

                    mtxGrid.RowDefinitions.Add(row);
                    mtxGrid.ColumnDefinitions.Add(col);
                }
                (mtxGrid.Parent as Border).Width = (listCr.Count + 1) * gridWidth;
                (mtxGrid.Parent as Border).Height = (listCr.Count + 1) * gridHeight;

            }

            else
            {
                gridWidth = (x - 10) / (listCr.Count + 1);
                gridHeight = gridWidth * 2 / 3;
                for (i = 0; i < listCr.Count + 1; i++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(gridHeight);
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = new GridLength(gridWidth);

                    mtxGrid.RowDefinitions.Add(row);
                    mtxGrid.ColumnDefinitions.Add(col);
                }
                (mtxGrid.Parent as Border).Width = x - 10;
                (mtxGrid.Parent as Border).Height = (x - 10) * 2 / 3;

            }

            // redraw matrix
            for (i = 0; i < listCr.Count + 1; i++)
            {
                TextBlock txt;

                if (i != listCr.Count)
                {
                    txt = new TextBlock();
                    txt.Text = listCr.ElementAt(i);
                    mtxGrid.Children.Add(txt);
                    Grid.SetRow(txt, 0);
                    Grid.SetColumn(txt, i + 1);
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.VerticalAlignment = VerticalAlignment.Center;

                    txt = new TextBlock();
                    txt.Text = listCr.ElementAt(i);
                    mtxGrid.Children.Add(txt);
                    Grid.SetRow(txt, i + 1);
                    Grid.SetColumn(txt, 0);
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.VerticalAlignment = VerticalAlignment.Center;
                }


                if (i > 0)
                {
                    txt = new TextBlock();
                    txt.Text = "1";
                    mtxGrid.Children.Add(txt);
                    Grid.SetRow(txt, i);
                    Grid.SetColumn(txt, i);
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.VerticalAlignment = VerticalAlignment.Center;
                }


                // starting from first line;
                if (i > 0 && i < listCr.Count)
                {
                    for (j = i + 1; j < listCr.Count + 1; j++)
                    {
                        StackPanel sp = new StackPanel();
                        sp.Orientation = Orientation.Horizontal;

                        TextBox tb = new TextBox();
                        int idx = Array.FindIndex(dblValues, z => z == mtx[i - 1, j - 1]);
                        tb.Text = strValues[idx];
                        //tb.Margin = new Thickness(5,5,0,5);
                        tb.Width = 3 * gridWidth / 5;
                        tb.Name = "txtNum" + (i - 1).ToString() + (j - 1).ToString();
                        RegisterName(tb.Name, tb);
                        tb.VerticalContentAlignment = VerticalAlignment.Center;

                        Button btnUp = new Button();
                        btnUp.Content = "+";
                        //btnUp.Margin = new Thickness(0, 10, 0, 0);
                        //btnUp.Width = 20;
                        btnUp.Name = "cmdUp" + (i - 1).ToString() + (j - 1).ToString();
                        btnUp.Click += new RoutedEventHandler(cmdUp_Click);
                        //RegisterName(btnUp.Name, btnUp);

                        Button btnDn = new Button();
                        btnDn.Content = "-";
                        //btnDn.Margin = new Thickness(0, 0, 0, 10);
                        //btnDn.Width = 20;
                        btnDn.Name = "cmdDn" + (i - 1).ToString() + (j - 1).ToString();
                        btnDn.Click += new RoutedEventHandler(cmdDn_Click);
                        //RegisterName(btnDn.Name, btnUp);

                        StackPanel sp1 = new StackPanel();
                        sp1.Orientation = Orientation.Vertical;
                        sp1.Children.Add(btnUp);
                        sp1.Children.Add(btnDn);

                        sp.Children.Add(tb);
                        sp.Children.Add(sp1);

                        mtxGrid.Children.Add(sp);
                        Grid.SetRow(sp, i);
                        Grid.SetColumn(sp, j);
                        sp.HorizontalAlignment = HorizontalAlignment.Center;
                        sp.VerticalAlignment = VerticalAlignment.Center;

                        txt = new TextBlock();
                        idx = Array.FindIndex(dblValues, z => z == mtx[j - 1, i - 1]);
                        txt.Text = strValues[idx];
                        txt.Name = "txtBlk" + (j - 1).ToString() + (i - 1).ToString();
                        RegisterName(txt.Name, txt);
                        mtxGrid.Children.Add(txt);
                        Grid.SetRow(txt, j);
                        Grid.SetColumn(txt, i);
                        txt.HorizontalAlignment = HorizontalAlignment.Center;
                        txt.VerticalAlignment = VerticalAlignment.Center;
                    }
                }
            }

        }

        private void evalButton_Click(object sender, RoutedEventArgs e)
        {
            if (mtx == null)
            {
                MessageBox.Show("criterias compare matrix has not been set!");
                return;
            }
            StructEvalResult res = ahp_eval(mtx);

            string weights = "\n";
            for (int i = 0; i < listCr.Count; i++)
                weights += "w(" + listCr[i] + ") = " + String.Format("{0:0.000000}", res.w[i]) + "; ";

            string rslt = "CR = ";
            rslt += String.Format("{0:0.000000}", res.CR);
            rslt += "\n";
            txtEvalRslt.Inlines.Clear();
            if (res.CR < 0.1)
            {
                BitmapImage MyImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/glad.png"));

                Image image = new Image();
                image.Source = MyImageSource;
                image.Width = 20;
                image.Height = 20;
                image.Visibility = Visibility.Visible;
                InlineUIContainer container = new InlineUIContainer(image);

                rslt += "good!";
                txtEvalRslt.Inlines.Add(rslt);
                txtEvalRslt.Inlines.Add(container);
                txtEvalRslt.Inlines.Add(weights);
                txtEvalRslt.Foreground = Brushes.Green;
            }
            else
            {
                BitmapImage MyImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/sad.png"));

                Image image = new Image();
                image.Source = MyImageSource;
                image.Width = 20;
                image.Height = 20;
                image.Visibility = Visibility.Visible;
                InlineUIContainer container = new InlineUIContainer(image);

                txtEvalRslt.Inlines.Add(rslt);
                txtEvalRslt.Inlines.Add(container);
                txtEvalRslt.Inlines.Add(weights);
                txtEvalRslt.Foreground = Brushes.Red;
            }
        }

        //draw AHP Hierarchy
        private void drawAhpHierarchy()
        {
            double cvsWidth = this.cvsHrcGraph.ActualWidth;
            double cvsHeight = this.cvsHrcGraph.ActualHeight;

            //clear the canvas and redraw.
            this.cvsHrcGraph.Children.Clear();

            // 1/5 fill words
            TextBlock txt = new TextBlock();
            txt.Text = "Goal:";
            Canvas.SetLeft(txt, 5);
            Canvas.SetTop(txt, cvsHeight / 6);
            this.cvsHrcGraph.Children.Add(txt);



            txt = new TextBlock();
            txt.Text = "Criteria:";
            Canvas.SetLeft(txt, 5);
            Canvas.SetTop(txt, cvsHeight / 2);
            this.cvsHrcGraph.Children.Add(txt);

            txt = new TextBlock();
            txt.Text = "Alternatives:";
            Canvas.SetLeft(txt, 5);
            Canvas.SetTop(txt, cvsHeight * 5 / 6);
            this.cvsHrcGraph.Children.Add(txt);

            DropShadowEffect dse = new DropShadowEffect();
            dse.BlurRadius = 4;
            dse.ShadowDepth = 10;
            dse.Color = Colors.Silver;

            // 4/5 fill graphics
            // goal rectangle
            Rectangle rec;
            if (this.txbGoal.Text != "")
            {
                rec = new Rectangle();
                rec.Width = cvsWidth * 4 / 5 / 4;
                rec.Height = rec.Width * 2 / 3;
                rec.Fill = Brushes.YellowGreen;
                rec.Effect = dse;
                Canvas.SetLeft(rec, cvsWidth / 5 + (cvsWidth * 4 / 5 - rec.Width) / 2);
                Canvas.SetTop(rec, (cvsHeight / 3 - rec.Height) / 2);
                this.cvsHrcGraph.Children.Add(rec);

                txt = new TextBlock();
                txt.Text = this.txbGoal.Text;
                txt.VerticalAlignment = VerticalAlignment.Center;
                txt.HorizontalAlignment = HorizontalAlignment.Center;
                txt.TextWrapping = TextWrapping.Wrap;

                Grid grd = new Grid();
                grd.Width = rec.Width - 2;
                grd.Height = rec.Height - 2;
                grd.Children.Add(txt);                

                Canvas.SetTop(grd, Canvas.GetTop(rec) + 1);
                Canvas.SetLeft(grd, Canvas.GetLeft(rec) + 1);
                this.cvsHrcGraph.Children.Add(grd);
            }
            
            if (listCr.Count != 0)
            {
                double crWidth = cvsWidth * 4 / 5 / listCr.Count * 4 / 5;
                double crHeight = crWidth * 2 / 3;
                for (int i = 0; i < listCr.Count; i++)
                {
                    rec = new Rectangle();
                    rec.HorizontalAlignment = HorizontalAlignment.Center;
                    rec.VerticalAlignment = VerticalAlignment.Center;

                    rec.Width = crWidth;
                    rec.Height = crHeight;
                    if (crHeight > cvsHeight / 3 / 2)
                        rec.Height = cvsHeight / 3 / 2;
                    rec.Fill = Brushes.Gold;
                    rec.Effect = dse;
                    Canvas.SetLeft(rec, cvsWidth / 5 + cvsWidth * 4 / 5 / listCr.Count * i + (cvsWidth * 4 / 5 / listCr.Count - crWidth) / 2);
                    Canvas.SetTop(rec, (cvsHeight / 3 - rec.Height) / 2 + cvsHeight / 3);
                    this.cvsHrcGraph.Children.Add(rec);

                    txt = new TextBlock();
                    txt.Text = listCr[i];
                    txt.VerticalAlignment = VerticalAlignment.Center;
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.TextWrapping = TextWrapping.Wrap;

                    Grid grd = new Grid();
                    grd.Width = rec.Width - 2;
                    grd.Height = rec.Height - 2;
                    grd.Children.Add(txt);

                    Canvas.SetTop(grd, Canvas.GetTop(rec) + 1);
                    Canvas.SetLeft(grd, Canvas.GetLeft(rec) + 1);
                    this.cvsHrcGraph.Children.Add(grd);
                }
            }



            if (listAlt.Count != 0)
            {
                double alWidth = cvsWidth * 4 / 5 / listAlt.Count * 4 / 5;
                double alHeight = alWidth * 2 / 3;
                for (int i = 0; i < listAlt.Count; i++)
                {
                    rec = new Rectangle();
                    rec.HorizontalAlignment = HorizontalAlignment.Center;
                    rec.VerticalAlignment = VerticalAlignment.Center;
                    rec.Width = alWidth;
                    rec.Height = alHeight;
                    if (alHeight > cvsHeight / 3 / 2)
                        rec.Height = cvsHeight / 3 / 2;
                    rec.Fill = Brushes.Pink;
                    rec.Effect = dse;
                    Canvas.SetLeft(rec, cvsWidth / 5 + cvsWidth * 4 / 5 / listAlt.Count * i + (cvsWidth * 4 / 5 / listAlt.Count - alWidth) / 2);
                    Canvas.SetTop(rec, (cvsHeight / 3 - rec.Height) / 2 + cvsHeight * 2 / 3);
                    this.cvsHrcGraph.Children.Add(rec);

                    txt = new TextBlock();
                    txt.Text = listAlt[i];
                    txt.VerticalAlignment = VerticalAlignment.Center;
                    txt.HorizontalAlignment = HorizontalAlignment.Center;
                    txt.TextWrapping = TextWrapping.Wrap;

                    Grid grd = new Grid();
                    grd.Width = rec.Width - 2;
                    grd.Height = rec.Height - 2;
                    grd.Children.Add(txt);

                    Canvas.SetTop(grd, Canvas.GetTop(rec) + 1);
                    Canvas.SetLeft(grd, Canvas.GetLeft(rec) + 1);
                    this.cvsHrcGraph.Children.Add(grd);
                }
            }
        }

        private void cvsHrcGraph_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            drawAhpHierarchy();

        }

        private void txbGoal_TextChanged(object sender, TextChangedEventArgs e)
        {
            drawAhpHierarchy();
        }

        private void tabPwC_Loaded(object sender, RoutedEventArgs e)
        {
            DrawCriteriaCompMtx();
        }
    }
}
