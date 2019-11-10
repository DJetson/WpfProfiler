using MVVM.Common.Interfaces;
using MVVM.Common.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace UITestApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<IViewModel> _ItemsCollection = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> ItemsCollection
        {
            get => _ItemsCollection;
            set { _ItemsCollection = value; NotifyPropertyChanged(); }
        }

        public MainWindowViewModel(int maxTestItems = 100, bool randomizeTestItemCount = false, int randomSeed = -1)
        {
            GenerateRandomTestData(maxTestItems, randomizeTestItemCount, randomSeed);
        }

        public void GenerateRandomTestData(int MaxItemCount, bool RandomizeItemCount, int randomSeed)
        {
            Random rng;
            if (randomSeed == -1)
            {
                rng = new Random((int)DateTime.Now.Ticks);
            }
            else
            {
                rng = new Random(randomSeed);
            }
            int ItemCount = 0;

            ItemCount = RandomizeItemCount ? rng.Next(MaxItemCount) : MaxItemCount;

            for (int i = 0; i < ItemCount; i++)
            {
                ItemsCollection.Add(new TestViewModel(rng) { Id = i + 1 });
            }
        }
    }
}
