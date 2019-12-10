using System;
using System.Xml.Linq;
using System.Collections.Generic;
using OxyPlot;

namespace FnS
{
    class MainViewModel
    {
        public string Title { get; private set; }
        public IList<DataPoint> YellowFish { get; private set; }
        public IList<DataPoint> PurpleFish { get; private set; }

        public MainViewModel()
        {
            this.Title = "Fish and Seaweeds";
            this.YellowFish = new List<DataPoint>();
            this.PurpleFish = new List<DataPoint>();

            //XDocument xdoc = XDocument.Load("C:\\Users\\Vladimir\\Desktop\\FnS\\Statistics.xml");
            XDocument xdoc = XDocument.Load(@"pack://application:,,,/1488/Statistics.xml");
            foreach (XElement elem in xdoc.Element("sprint").Elements("Round"))
            {
                XAttribute attrName = elem.Attribute("id");
                XElement YFcount = elem.Element("Yellow_Fish");
                XElement PFcount = elem.Element("Purple_Fish");

                if (attrName != null && YFcount != null && PFcount != null)
                {
                    DataPoint tmp1 = new DataPoint(double.Parse(attrName.Value), double.Parse(YFcount.Value));
                    DataPoint tmp2 = new DataPoint(double.Parse(attrName.Value), double.Parse(PFcount.Value));
                    YellowFish.Add(tmp1);
                    PurpleFish.Add(tmp2); 
                }
            }
        }

    }
}
