using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerie.Models
{
    public class Serie
    {
        public int Serieid { get; set; }
        public string Titre { get; set; } = null!;
        public string Resume { get; set; }
        public int Nbsaisons { get; set; }
        public int Nbepisodes { get; set; }
        public int Anneecreation { get; set; }
        public string Network { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Serie serie &&
                   Serieid == serie.Serieid &&
                   Titre == serie.Titre &&
                   Resume == serie.Resume &&
                   Nbsaisons == serie.Nbsaisons &&
                   Nbepisodes == serie.Nbepisodes &&
                   Anneecreation == serie.Anneecreation &&
                   Network == serie.Network;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Serieid, Titre, Resume, Nbsaisons, Nbepisodes, Anneecreation, Network);
        }

        public Serie(int _serieid, string _titre, string _resume, int nbSaison, int nbEpisode, int anneecreation, string network)
        {
            Serieid = _serieid;
            Titre = _titre;
            Resume = _resume;
            Nbsaisons = nbSaison;
            Nbepisodes = nbEpisode;
            Anneecreation = anneecreation;
            Network = network;
        }

        public Serie() : this(1, "", "", 0, 0, 0, "") { }
    }
}
