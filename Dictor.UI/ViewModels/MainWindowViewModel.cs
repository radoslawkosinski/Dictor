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

        public MainWindowViewModel()
        {

            this.canExecute = false;
            this.canPlay = true;

            this.translationService = App.TranslationService;

            TranslateAllProvidersCommand = ReactiveCommand.CreateFromTask<bool>(_ => TranslateAllProviders(), this.WhenAnyValue(x => x.CanExecute));

            PlayAudioCommand = ReactiveCommand.CreateFromTask<string>(_ => PlaySound(_), this.WhenAnyValue(x => x.CanPlay));

            //var t = Task.Run(() => translationService.TranslateAllProviders("apple"));
            //t.Wait();
        }


        private async Task TranslateAllProviders()
        {
            Translations.Clear();

            var lst = await translationService.TranslateAllProviders(this._phrase);
            var translatedList = new ObservableCollection<TranslationResult>(lst);
          //  MessageBox.Show("zzz");

            if (Translations.Count == 0)
                Translations.AddRange(translatedList);
            //else {
            //    foreach (var translated in translatedList)
            //    {
            //        var existingTranslation = Translations
            //            .Where(x => x.ProviderName ==  translated.ProviderName).FirstOrDefault();

            //        if (existingTranslation == null)
            //            Translations.Add(translated);
            //        else
            //            existingTranslation = translated;
            //    }
            //}
        }

        private async Task PlaySound(string soundUrl)
        {
            //await translationService.ListenAudio(_selectedProvider.ProviderName, soundUrl);
            //this.canPlay = !string.IsNullOrEmpty(soundUrl);
            MessageBox.Show(soundUrl);
        }
    }
}
