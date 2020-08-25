using Dictor.UI.ViewModels;
using ReactiveUI;
using System.Windows;

namespace Dictor.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainWindowViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel",
            typeof(MainWindowViewModel), typeof(MainWindow), new PropertyMetadata(null));        
        
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;
            //if ever decide to have binding in codebehind, it should be like this:

            //this
            //.WhenActivated(disposables => {
            //this
            //.BindCommand(this.ViewModel, vm => vm.TranslateAllProvidersCommand, v => v.TestButton)
            //.DisposeWith(disposables);
            //});

        }

        public MainWindowViewModel ViewModel
        {
            get { return (MainWindowViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainWindowViewModel)value; }
        }


    }
}
