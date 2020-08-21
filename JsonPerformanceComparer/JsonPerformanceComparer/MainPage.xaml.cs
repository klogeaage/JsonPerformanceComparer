using JsonPerformanceComparer.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace JsonPerformanceComparer
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MeasureViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
