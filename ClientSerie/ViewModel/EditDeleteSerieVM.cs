using ClientSerie.Models;
using ClientSerie.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerie.ViewModel
{
    public class EditDeleteSerieVM : ObservableObject
    {
        private Serie laSerie;
        public Serie LaSerie
        {
            get
            {
                return laSerie;
            }
            set
            {
                laSerie = value;
                OnPropertyChanged();
            }
        }

        private int idSerie;
        public int IdSerie
        {
            get
            {
                return idSerie;
            }
            set
            {
                idSerie = value;
                OnPropertyChanged();
            }
        }

        public IRelayCommand BtnSearch { get; }
        public IRelayCommand BtnDelete { get; }
        public IRelayCommand BtnEdit { get; }

        public EditDeleteSerieVM()
        {
            LaSerie = new Serie();

            BtnSearch = new RelayCommand(ActionSearch);
            BtnDelete = new RelayCommand(ActionDelete);
            BtnEdit = new RelayCommand(ActionEdit);
        }

        public async void ActionEdit()
        {
            if(String.IsNullOrEmpty(LaSerie.Titre))
            {
                await ShowMessage("Erreur Saisie", "Le titre de la série ne peut pas être vide");
                return;
            }
            if(LaSerie.Nbsaisons <= 0)
            {
                await ShowMessage("Erreur Saisie", "Le nombre de saison doit être strictement positif");
                return;
            }
            if (LaSerie.Nbepisodes <= 0)
            {
                await ShowMessage("Erreur Saisie", "Le nombre de saison doit être strictement positif");
                return;
            }
            if (LaSerie.Anneecreation <= 0)
            {
                await ShowMessage("Erreur Saisie", "Le nombre de saison doit être strictement positif");
                return;
            }
            WSSerie service = new WSSerie("http://localhost:5185/api/");
            bool res = await service.EditSerieAsync("Series", IdSerie, LaSerie);
            if(res)
            {
                await ShowMessage("Modification", "La série a correctement été modifiée");
            } else
            {
                await ShowMessage("Modification", "La série n'as pas pu être modifié");
            }

        }

        public async void ActionDelete()
        {
            WSSerie service = new WSSerie("http://localhost:5185/api/");
            bool res = await service.DeleteSerieAsync("Series", LaSerie);
            if(res)
            {
                await ShowMessage("Suppression", "Votre série à correctement été suprimée");
            } else
            {
                await ShowMessage("Suppression", "Une erreur est survenu lors de la suppression");
            }
        }

        public async void ActionSearch()
        {
            WSSerie service = new WSSerie("http://localhost:5185/api/");
            var res = await service.GetSerieAsync("Series", IdSerie);
            if (res is Serie)
            {
                LaSerie = (Serie)res;
            } else
            {
                await ShowMessage("Serie non-trouvée", "Votre série n'as pas été trouvée");
            }
        }

        private async Task ShowMessage(string title, string message)
        {

            ContentDialog contentDialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            contentDialog.XamlRoot = App.MainRoot.XamlRoot;


            ContentDialogResult result = await contentDialog.ShowAsync();
        }
    }
}
