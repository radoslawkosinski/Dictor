using Dictor.Lib;
using Dictor.Lib.Model;
using Dictor.Lib.Provider;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;

namespace Dictor.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        
        private ITranslationService translationService;

        bool canExecute = true; //dev only
        public bool CanExecute
        {
            get { return canExecute; }
            set { this.RaiseAndSetIfChanged(ref canExecute, value); }
        }





        //private Visibility onlineExamplesVisible;
        //public Visibility OnlineExamplesVisible
        //{
        //    get { return onlineExamplesVisible; }
        //    set
        //    {
        //        this.RaiseAndSetIfChanged(ref onlineExamplesVisible, value);
        //    }
        //}

        private TranslationResult _selectedProvider;
        public TranslationResult SelectedProvider
        {
            get { return _selectedProvider; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedProvider, value);
            }
        }

        bool canPlay = true; //dev only
        public bool CanPlay
        {
            get { return canPlay; }
            set { this.RaiseAndSetIfChanged(ref canPlay, value); }
        }

        //private bool _isLoading;

        //public bool IsLoading
        //{
        //    get { return _isLoading; }
        //    set { this.RaiseAndSetIfChanged(ref _isLoading, value); }
        //}




        readonly ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading { get { return _isLoading.Value; } }


        public ReactiveCommand<string, Unit> PlayAudioCommand { get; }


        public ReactiveCommand<bool,Unit> TranslateAllProvidersCommand { get; }
        public IObservableCollection<TranslationResult> Translations { get; } = new ObservableCollectionExtended<TranslationResult>();

        private TranslationResult _selectedTranslation;
        public TranslationResult SelectedTranslation
        {
            get { return _selectedTranslation; }
            set
            {

                this.RaiseAndSetIfChanged(ref _selectedTranslation, value);
                //CanExecute = (_selectedTranslation != null);
            }
        }


        private string _phrase;
        public string Phrase
        {
            get { return _phrase; }
            set
            {

                this.RaiseAndSetIfChanged(ref _phrase, value);
                CanExecute = (!string.IsNullOrEmpty(_phrase));
            }
        }


        private readonly SourceList<TranslationResult> myList = new SourceList<TranslationResult>();
        public IObservableCollection<TranslationResult> MyListBindable { get; } = new ObservableCollectionExtended<TranslationResult>();


        //tests: https://csharp.hotexamples.com/examples/-/ReactiveCommand/Execute/php-reactivecommand-execute-method-examples.html

        public MainWindowViewModel()
        {

            //this.IsLoading = false;
            this.canExecute = false;
            this.canPlay = true;
            //this.onlineExamplesVisible = NotEmptyVisibility;


            this.translationService = App.TranslationService;

            TranslateAllProvidersCommand = ReactiveCommand.CreateFromTask<bool>(_ => TranslateAllProviders(), this.WhenAnyValue(x => x.CanExecute));
            TranslateAllProvidersCommand.IsExecuting.ToProperty(this, x => x.IsLoading, out _isLoading);


            /*
            private readonly ObservableAsPropertyHelper<bool> _isRefreshing;        
             public bool IsRefreshing => _isRefreshing.Value;
            
            _isRefreshing =
                            LoadMovies
                                .IsExecuting
                                .ToProperty(this, x => x.IsRefreshing, true);




                         */

            //TranslateAllProvidersCommand.IsExecuting
            //.ToProperty(this, x => x.IsLoading);

            PlayAudioCommand = ReactiveCommand.CreateFromTask<string>(_ => PlaySound(_), this.WhenAnyValue(x => x.CanPlay));

            //var t = Task.Run(() => translationService.TranslateAllProviders("apple"));
            //t.Wait();
        }


        private async Task TranslateAllProviders()
        {
            Translations.Clear();

            var lst = await translationService.TranslateAllProviders(this._phrase);
            var translatedList = new ObservableCollection<TranslationResult>(lst);

            if (Translations.Count == 0)
                Translations.AddRange(translatedList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soundUrl"></param>
        /// <returns></returns>
        private async Task PlaySound(string soundUrl)
        {
            //await translationService.ListenAudio(_selectedProvider.ProviderName, soundUrl);
            //this.canPlay = !string.IsNullOrEmpty(soundUrl);
            MessageBox.Show(soundUrl);
        }
    }
}
