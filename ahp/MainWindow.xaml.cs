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
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls.Ribbon;

namespace ahp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        string tmpFile;
        public MainWindow()
        {
            InitializeComponent();
            showChart();

            // when software starts up, new project file is created (temporary file)
            // also, xml structure is set up.
            //tmpFile = CreateTmpFile();
            //UpdateTmpFile(tmpFile);
            //ReadTmpFile(tmpFile);
            //DeleteTmpFile(tmpFile);
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

        private static string CreateTmpFile()
        {
            string fileName = string.Empty;

            try
            {
                // Get the full name of the newly created Temporary file. 
                // Note that the GetTempFileName() method actually creates
                // a 0-byte file and returns the name of the created file.
                fileName = System.IO.Path.GetTempFileName();

                // Craete a FileInfo object to set the file's attributes
                FileInfo fileInfo = new FileInfo(fileName);

                // Set the Attribute property of this file to Temporary. 
                // Although this is not completely necessary, the .NET Framework is able 
                // to optimize the use of Temporary files by keeping them cached in memory.
                fileInfo.Attributes = FileAttributes.Temporary;
                //CreatXmlTree(fileName);

                Console.WriteLine("TEMP file created at: " + fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create TEMP file or set its attributes: " + ex.Message);
            }

            return fileName;
        }

        private static void UpdateTmpFile(string tmpFile)
        {
            try
            {
                // Write to the temp file.
                StreamWriter streamWriter = File.AppendText(tmpFile);
                streamWriter.WriteLine("Hello from www.daveoncsharp.com!");
                streamWriter.Flush();
                streamWriter.Close();

                Console.WriteLine("TEMP file updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to TEMP file: " + ex.Message);
            }
        }

        private static void ReadTmpFile(string tmpFile)
        {
            try
            {
                // Read from the temp file.
                StreamReader myReader = File.OpenText(tmpFile);
                Console.WriteLine("TEMP file contents: " + myReader.ReadToEnd());
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading TEMP file: " + ex.Message);
            }
        }

        private static void DeleteTmpFile(string tmpFile)
        {
            try
            {
                // Delete the temp file (if it exists)
                if (File.Exists(tmpFile))
                {
                    File.Delete(tmpFile);
                    Console.WriteLine("TEMP file deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleteing TEMP file: " + ex.Message);
            }
        }

        public void CreatXmlTree(string xmlPath)
        {
            XElement xElement = new XElement(
                new XElement("Project",
                    new XElement("Name"),
                    new XElement("Goal"),
                    new XElement("Description"),
                    new XElement("Alternatives",
                         new XElement("AlternativesItem",
                            new XElement("Name", "Alternative1"),
                            new XElement("Description")
                        ),
                        new XElement("AlternativesItem",
                            new XElement("Name", "Alternative2"),
                            new XElement("Description")
                        ),
                        new XElement("AlternativesItem", 
                            new XElement("Name", "Alternative3"),
                            new XElement("Description")
                        )
                    ),
                    new XElement("Criteria",
                        new XElement("CriteriaItem",
                            new XElement("Name", "Criteria1"),
                            new XElement("Description")
                        ),
                        new XElement("CriteriaItem",
                            new XElement("Name", "Criteria2"),
                            new XElement("Description")
                        ),
                        new XElement("CriteriaItem",
                            new XElement("Name", "Criteria3"),
                            new XElement("Description")
                        )
                    ),
                    new XElement("Results")));
 
             //需要指定编码格式，否则在读取时会抛：根级别上的数据无效。 第 1 行 位置 1异常
             XmlWriterSettings settings = new XmlWriterSettings();
             settings.Encoding = new UTF8Encoding(false);
             settings.Indent = true;
             XmlWriter xw = XmlWriter.Create(xmlPath, settings);
             xElement.Save(xw);
             //写入文件
             xw.Flush();
             xw.Close();
        }
    }

    class XmlOperation
        {

            public void Create(string xmlPath)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                var root = xmlDoc.DocumentElement;//取到根结点

                XmlNode newNode = xmlDoc.CreateNode("element", "Name", "");
                newNode.InnerText = "Zery";

                //添加为根元素的第一层子结点
                root.AppendChild(newNode);
                xmlDoc.Save(xmlPath);
            }
            //属性
            public void CreateAttribute(string xmlPath)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlElement node = (XmlElement)xmlDoc.SelectSingleNode("Collection/Book");
                node.SetAttribute("Name", "C#");
                xmlDoc.Save(xmlPath);
            }

            public void Delete(string xmlPath)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                var root = xmlDoc.DocumentElement;//取到根结点

                var element = xmlDoc.SelectSingleNode("Collection/Name");
                root.RemoveChild(element);
                xmlDoc.Save(xmlPath);
            }

            public void DeleteAttribute(string xmlPath)
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlElement node = (XmlElement)xmlDoc.SelectSingleNode("Collection/Book");
                //移除指定属性
                node.RemoveAttribute("Name");
                //移除当前节点所有属性，不包括默认属性
                node.RemoveAllAttributes();

                xmlDoc.Save(xmlPath);

            }

            public void Modify(string xmlPath)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                var root = xmlDoc.DocumentElement;//取到根结点
                XmlNodeList nodeList = xmlDoc.SelectNodes("/Collection/Book");
                //xml不能直接更改结点名称，只能复制然后替换，再删除原来的结点
                foreach (XmlNode node in nodeList)
                {
                    var xmlNode = (XmlElement)node;
                    xmlNode.SetAttribute("ISBN", "Zery");
                }
                xmlDoc.Save(xmlPath);

            }

            public void ModifyAttribute(string xmlPath)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlElement element = (XmlElement)xmlDoc.SelectSingleNode("Collection/Book");
                element.SetAttribute("Name", "Zhang");
                xmlDoc.Save(xmlPath);

            }

            public void Select(string xmlPath)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                //取根结点
                var root = xmlDoc.DocumentElement;//取到根结点
                                                  //取指定的单个结点
                XmlNode singleNode = xmlDoc.SelectSingleNode("Collection/Book");

                //取指定的结点的集合
                XmlNodeList nodes = xmlDoc.SelectNodes("Collection/Book");

                //取到所有的xml结点
                XmlNodeList nodelist = xmlDoc.GetElementsByTagName("*");
            }

            public void SelectAttribute(string xmlPath)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlElement element = (XmlElement)xmlDoc.SelectSingleNode("Collection/Book");
                string name = element.GetAttribute("Name");

            }
        }

    struct AlternativesItem
    {
    }
}
