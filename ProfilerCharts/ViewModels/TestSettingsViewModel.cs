using MVVM.Common.Interfaces;
using MVVM.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProfilerCharts.ViewModels
{
    public class TestSettingsViewModel : ViewModelBase, IBrowseForFile
    {
        /// <summary>
        /// TestSettings may be set to readonly whenever they are used to show settings already applied to 
        /// a previously run test, as in a compiled test report.
        /// </summary>
        private bool _IsReadOnly;
        public bool IsReadOnly
        {
            get => _IsReadOnly;
            set { _IsReadOnly = value; NotifyPropertyChanged(); }
        }

        private int _OrderId;
        public int OrderId
        {
            get => _OrderId;
            set { _OrderId = value; NotifyPropertyChanged(); }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set { _Name = value; NotifyPropertyChanged(); }
        }

        private Type _Target;
        public Type Target
        {
            get => _Target;
            set { _Target = value; NotifyPropertyChanged(); }
        }

        private MockDataSetViewModel _MockDataSet = new MockDataSetViewModel();
        public MockDataSetViewModel MockDataSet
        {
            get => _MockDataSet;
            set { _MockDataSet = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// The number of Mock DataItems to create and display in controls that display collections
        /// </summary>
        private int _TestDataItemCount = 1000;
        public int TestDataItemCount
        {
            get => _TestDataItemCount;
            set { _TestDataItemCount = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<Type> _TargetTypes = new ObservableCollection<Type>();
        public ObservableCollection<Type> TargetTypes
        {
            get => _TargetTypes;
            set { _TargetTypes = value; NotifyPropertyChanged(); }
        }

        public void LoadFiles(IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                if (File.Exists(path))
                    PopulateControlsList(path, true);
            }
        }

        private void PopulateControlsList(string path, bool appendItems)
        {
            if(!appendItems)
            {
                TargetTypes.Clear();
            }

            var targetAssembly = Assembly.LoadFile(path);

            foreach(var type in targetAssembly.GetTypes().Where(e => e.IsClass && e.IsPublic && e.IsSubclassOf(typeof(FrameworkElement))))
            {
                TargetTypes.Add(type);
            }
        }

        public override object Clone()
        {
            var clone = new TestSettingsViewModel()
            {
                IsReadOnly = this.IsReadOnly,
                MockDataSet = (MockDataSetViewModel)this.MockDataSet.Clone(),
                Name = this.Name,
                OrderId = -1,
                Target = this.Target,
                TargetTypes = new ObservableCollection<Type>(this.TargetTypes),
                TestDataItemCount = this.TestDataItemCount
            };

            return clone;
        }
    }
}
