using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FnS
{
    class PurpleFish : Fish
    {
        Random rnd = new Random();
        //private int speed;
        //private int age;
        public PurpleFish()
        {
            //age = rnd.Next(1, 10);
            F.Height = 100;
            F.Width = 100;

            ibFish.AlignmentX = AlignmentX.Left;
            ibFish.AlignmentY = AlignmentY.Top;
            //ibFish.ImageSource = new BitmapImage(new Uri("C:\\Users\\Vladimir\\Desktop\\FnS\\img\\PurpleFish.png", UriKind.Absolute));
            ibFish.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/1488/fish1.png", UriKind.Absolute));
            F.Fill = ibFish;
        }
    }
}
