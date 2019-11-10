using MVVM.Common.ViewModels;
using System;

namespace UITestApp.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        private static readonly string StringPool = "aaiv iunsldv.as,dt'm0pqu4gpu3 qgwi#f{uQ34 'TQgWEfQ" 
                                                  + "WE6G51 3ES4R6TH4 1A35ESGA 5H03A E5FA W6GA'DFAW6G A"
                                                  + "AR 65HTU5LH,G H5KI6P'I.UI0Y95LFND4 tum ;xaw7fc noa"
                                                  + "7wokyePBN9w8 36yrv;o8 2I RYPF N ;0qwna;ois rkjg n;"
                                                  + "sdvy pa98wo;htap0 48w yfapw498yt hvqn 3;o4igyabe98"
                                                  + "nl02 9834gyhb;p8ei  c';qvt7anwcp92EW (*awn ^rylN i"
                                                  + "5HTU5LH,G H5KI6P'I.UI0Y95 LFNDT6T6.RHG SDFVBVBMbjk"
                                                  + "vynpq39.RHG SDFVB VBMbjk;HUGIJDFTHBFY. ILKD REEW5F"
                                                  + "ncalmoe iyflqc387t vqnl9ow4fymlair7tfcg boqn938;oy"
                                                  + "WAd6,u s5u,dt6mudt56.tr54  sr.5y .r34nus.erby0eh a";

        private string _StringValue;
        public string StringValue
        {
            get => _StringValue;
            set { _StringValue = value; NotifyPropertyChanged(); }
        }

        private int _IntValue;
        public int IntValue
        {
            get => _IntValue;
            set { _IntValue = value; NotifyPropertyChanged(); }
        }

        private double _DoubleValue;
        public double DoubleValue
        {
            get => _DoubleValue;
            set { _DoubleValue = value; NotifyPropertyChanged(); }
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set { _Id = value; NotifyPropertyChanged(); }
        }

        public TestViewModel(Random rng)
        {
            RandomizeForTesting(rng);
        }

        public void RandomizeForTesting(Random rng)
        {
            IntValue = rng.Next();
            DoubleValue = rng.NextDouble();
            int length = rng.Next(100);
            int startIndex = rng.Next(500 - length);
            StringValue = StringPool.Substring(startIndex, length);
        }
    }
}
