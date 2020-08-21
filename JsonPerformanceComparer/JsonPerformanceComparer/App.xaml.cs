using JsonPerformanceComparer.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JsonPerformanceComparer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage(new MeasureViewModel());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
