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
using System.Xml;
using OxyPlot;
using OxyPlot.Series;

namespace FnS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<YellowFish> ListOfFish = new List<YellowFish>();
        List<PurpleFish> ListOfFish2 = new List<PurpleFish>();
        List<Seaweed> ListOfSeaweed = new List<Seaweed>();

        System.Windows.Threading.DispatcherTimer Timer;
        System.Windows.Threading.DispatcherTimer Timer2;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void GenerateBt_Click(object sender, RoutedEventArgs e)
        {
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(dispatcherTimer_Tick);
            Timer.Interval = new TimeSpan( 0 , 0 , 1);
            Timer2 = new System.Windows.Threading.DispatcherTimer();
            Timer2.Tick += new EventHandler(dispatcherTimer2_Tick);
            Timer2.Interval = new TimeSpan(0, 0, 0, 2);

            int countFish, countFish2, countSeaweed, round = 1;
            try
            {
                countFish = int.Parse(countOfFish.Text);
                countFish2 = int.Parse(countOfFish2.Text);
                countSeaweed = int.Parse(countOfSeaweed.Text);

                if (countFish <= 50 && countSeaweed <= 100 && countFish2 <=50)
                {
                    if (countFish > 0 && countSeaweed > 0 && countFish2 > 0)
                    {
                        Menu.Visibility = Visibility.Hidden;
                        MainSpace.Visibility = Visibility.Visible;

                        Random rnd = new Random();

                        for (int i = 1; i <= countFish; i++)
                        {
                            YellowFish Fish = new YellowFish();
                            Fish.F.Margin = new Thickness(0, 0, 0, 0);
                            Fish.fx = rnd.Next(0, 8);
                            Fish.fy = rnd.Next(0, 5);
                            Fish.F.RenderTransform = new TranslateTransform(Fish.fx * 100 + 240, Fish.fy * 100 + 60);
                            Scene.Children.Add(Fish.F);

                            ListOfFish.Add(Fish);
                        }

                        for (int i = 1; i <= countFish2; i++)
                        {
                            PurpleFish Fish2 = new PurpleFish();
                            Fish2.F.Margin = new Thickness(0, 0, 0, 0);
                            Fish2.fx = rnd.Next(0, 8);
                            Fish2.fy = rnd.Next(0, 5);
                            Fish2.F.RenderTransform = new TranslateTransform(Fish2.fx * 100 + 240, Fish2.fy * 100 + 60);
                            Scene.Children.Add(Fish2.F);

                            ListOfFish2.Add(Fish2);
                        }

                        for (int i = 1; i <= countSeaweed; i++)
                        {
                            Seaweed Seaweed = new Seaweed();
                            Seaweed.fx = rnd.Next(0, 8);
                            Seaweed.fy = rnd.Next(0, 5);
                            Seaweed.S.Margin = new Thickness(0, 0, 0, 0);
                            Seaweed.S.RenderTransform = new TranslateTransform(Seaweed.fx * 100 + 240, Seaweed.fy * 100 + 60);
                            Scene.Children.Add(Seaweed.S);
                            ListOfSeaweed.Add(Seaweed);
                        }

                        XmlDocument xDoc = new XmlDocument();
                        //xDoc.Load("C:\\Users\\Vladimir\\Desktop\\FnS\\Statistics.xml");
                        xDoc.Load(@"pack://application:,,,/1488/Statistics.xml");
                        XmlElement xRoot = xDoc.DocumentElement;
                        XmlElement OptionsElem = xDoc.CreateElement("Round");
                        XmlAttribute numAttr = xDoc.CreateAttribute("id");

                        XmlElement CYF = xDoc.CreateElement("Yellow_Fish");
                        XmlElement CPF = xDoc.CreateElement("Purple_Fish");

                        XmlText roundNum = xDoc.CreateTextNode(round.ToString());
                        XmlText CYFn = xDoc.CreateTextNode(countFish.ToString());
                        XmlText CPFn = xDoc.CreateTextNode(countFish2.ToString());

                        //Creating nodes
                        numAttr.AppendChild(roundNum);
                        CYF.AppendChild(CYFn);
                        CPF.AppendChild(CPFn);
                        OptionsElem.Attributes.Append(numAttr);
                        OptionsElem.AppendChild(CYF);
                        OptionsElem.AppendChild(CPF);
                        xRoot.AppendChild(OptionsElem);
                        //xDoc.Save("C:\\Users\\Vladimir\\Desktop\\FnS\\Statistics.xml" );
                        xDoc.Save(@"pack://application:,,,/1488/Statistics.xml");
                    }
                    else
                    {
                        MessageBox.Show("Count of Fish and Seaweed must be more than 0!");
                        countOfFish.Text = "";
                        countOfFish2.Text = "";
                        countOfSeaweed.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Fish must be <=5, Seaweed must be <= 10!");
                    countOfFish.Text = "";
                    countOfFish2.Text = "";
                    countOfSeaweed.Text = "";
                }
            }
            catch
            {
                MessageBox.Show("Enter numbers!", "Error");
                countOfFish.Text = "";
                countOfFish2.Text = "";
                countOfSeaweed.Text = "";
            }
            Timer.Start();
            Timer2.Start();
        }

        

        double MinXY = 1100;
        int X, Y;
        double XY;
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(ListOfSeaweed.Count == 0) //проверка на пустоту списка водорослей, и остановка таймеров
            {
                Timer.Stop();
                Timer2.Stop();
            }

            foreach (YellowFish fi in ListOfFish)
            {
                foreach (Seaweed se in ListOfSeaweed)
                {
                    XY = Math.Sqrt(Math.Pow(fi.fx - se.fx, 2) + Math.Pow(fi.fy - se.fy, 2));
                    if (MinXY < XY)
                    {
                        MinXY = XY;
                        X = se.fx;//минимальный х до водоросли
                        Y = se.fy;//минимальный у до водоросли
                    }

                    if (X == fi.fx & Y == fi.fy)
                    {
                        ListOfSeaweed.Remove(se);
                        Scene.Children.Remove(se.S);
                    }
                }

                if (X > fi.fx) //координаты хранятся, как цифры от 0 до 8
                    fi.fx =+ 1;
                else if (X < fi.fx)
                    fi.fx =- 1;

                if (Y > fi.fy)
                    fi.fy =+ 1;
                else if (Y < fi.fy)
                    fi.fy =- 1;

                Scene.Children.Remove(fi.F);
                fi.F.RenderTransform = new TranslateTransform(fi.fx * 100 + 240, fi.fy * 100 + 60); //тут мы их переводим в нормальные координаты (учит. смещение) и отрисовываем
                Scene.Children.Add(fi.F);               
            }
        }

        private void dispatcherTimer2_Tick(object sender, EventArgs e)
        {
            foreach (PurpleFish fi in ListOfFish2)
            {
                foreach (Seaweed se in ListOfSeaweed)
                {
                    XY = Math.Sqrt(Math.Pow(fi.fx - se.fx, 2) + Math.Pow(fi.fy - se.fy, 2));
                    if (MinXY > XY)
                    {
                        MinXY = XY;
                        X = se.fx;//минимальный х до водоросли
                        Y = se.fy;//минимальный у до водоросли
                    }

                    if (X == fi.fx & Y == fi.fy)
                        Scene.Children.Remove(se.S);
                }

                if (X > fi.fx)
                    fi.fx = +1;
                else if (X < fi.fx)
                    fi.fx = -1;

                if (Y > fi.fy)
                    fi.fy = +1;
                else if (Y < fi.fy)
                    fi.fy = -1;

                Scene.Children.Remove(fi.F);
                fi.F.RenderTransform = new TranslateTransform(fi.fx * 100 + 240, fi.fy * 100 + 60);
                Scene.Children.Add(fi.F);
            }
        }

        private void countOfFish2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CountOfFish_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void countOfSeaweed_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DrawBt_Click(object sender, RoutedEventArgs e)
        {
            Stat.Visibility = Visibility.Visible;
            Menu.Visibility = Visibility.Hidden;
        }
    }
}