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

namespace FnS
{
    class Seaweed
    {
        Random rnd = new Random();
        public Rectangle S = new Rectangle();
        public ImageBrush ibSeaweed = new ImageBrush();
        public int fx;
        public int fy;
        
        public Seaweed() //seaweed's constructor 
        {
            S.Height = 100;
            S.Width = 100;
            S.Fill = ibSeaweed;
            ibSeaweed.AlignmentX = AlignmentX.Left;
            ibSeaweed.AlignmentY = AlignmentY.Top;
            //ibSeaweed.ImageSource = new BitmapImage(new Uri("C:\\Users\\Vladimir\\Desktop\\FnS\\img\\Seaweed.png", UriKind.Absolute));
            ibSeaweed.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/1488/vodo1.png", UriKind.Absolute));
        }
    }
}