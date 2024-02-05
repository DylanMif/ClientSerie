using ClientSerie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerie.Service
{
    public interface IService
    {
        public Task<List<Serie>> GetSeriesAsync(string nomControleur);
        public Task<Serie> GetSerieAsync(string nomControleur, int serieId);
        public Task<bool> PostSerieAsync(string nomControleur, Serie serie);
    }
}
