using ProfilerCharts.ViewModels;
using ProfilerCharts.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace ProfilerCharts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            List<long> data = new List<long>();
            if (e.Args.Count() != 0)
            {
                foreach (var item in e.Args[0].Split(','))
                {
                    if (long.TryParse(item, out long val))
                        data.Add(val);
                }
            }

            MainWindow = new MainWindow() { DataContext = new MainWindowViewModel(data) };
            MainWindow.Show();
        }
    }
}
