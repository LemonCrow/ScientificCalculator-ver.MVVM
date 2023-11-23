using System.Windows;
using ScientificCalculator_ver.MVVM.ViewModels;

namespace ScientificCalculator_ver.MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }
}
