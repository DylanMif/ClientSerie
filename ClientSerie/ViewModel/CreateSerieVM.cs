using ClientSerie.Models;
using ClientSerie.Service;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerie.ViewModel
{
    public class CreateSerieVM
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
            }
        }

        public IRelayCommand BtnAdd { get; }

        public CreateSerieVM()
        {
            LaSerie = new Serie();
            BtnAdd = new RelayCommand(ActionAjouter);
        }

        public async void ActionAjouter()
        {
            if(String.IsNullOrEmpty(LaSerie.Titre))
            {
                ShowMessage("Erreur", "Vous devez entrer un titre pour la série");
                return;
            }
            if (String.IsNullOrEmpty(LaSerie.Resume))
            {
                ShowMessage("Erreur", "Vous devez entrer un résumé pour la série");
                return;
            }
            if (LaSerie.Nbsaisons <= 0)
            {
                ShowMessage("Erreur", "Vous devez entrer un nombre de saison strictement positif");
                return;
            }
            if (LaSerie.Nbepisodes <= 0)
            {
                ShowMessage("Erreur", "Vous devez entrer un nombre d'épisode strictement positif");
                return;
            }
            if (LaSerie.Anneecreation <= 0)
            {
                ShowMessage("Erreur", "Vous devez entrer une année de création strictement positif");
                return;
            }
            if (String.IsNullOrEmpty(LaSerie.Network))
            {
                ShowMessage("Erreur", "Vous devez entrer une chaine pour la série");
                return;
            }

            WSSerie service = new WSSerie("http://localhost:5185/api/");
            bool res = await service.PostSerieAsync("Series", LaSerie);
            if(res)
            {
                ShowMessage("Information", "La série a correctement été ajoutée");
            } else
            {
                ShowMessage("Information", "La série n'a pas été ajoutée");
            }
        }


        private async void ShowMessage(string title, string message)
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

