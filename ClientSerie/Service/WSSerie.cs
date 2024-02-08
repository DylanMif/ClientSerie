using ClientSerie.Models;
using ClientSerie.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientSerie.Service
{
    public class WSSerie : IService
    {
        private HttpClient client;

        public HttpClient Client
        {
            get { return client; }
            set { client = value; }
        }

        public WSSerie(string uri)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(uri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public WSSerie() : this("http://localhost:5185/api/") { }

        public async Task<List<Serie>> GetSeriesAsync(string nomControleur)
        {
            try
            {
                return await Client.GetFromJsonAsync<List<Serie>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Serie> GetSerieAsync(string nomControleur, int serieId)
        {
            try
            {
                return await Client.GetFromJsonAsync<Serie>(String.Concat(nomControleur, "/", serieId));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> PostSerieAsync(string nomControleur, Serie serie)
        {
            var response = await Client.PostAsJsonAsync(nomControleur, serie);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSerieAsync(string nomControler, Serie serie)
        {
            var response = await Client.DeleteAsync(String.Concat(nomControler, "/", serie.Serieid));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EditSerieAsync(string nomControler, int idToEdit, Serie serie)
        {
            var response = await Client.PutAsJsonAsync(String.Concat(nomControler, "/", idToEdit), serie);
            return response.IsSuccessStatusCode;
        }
    }
}
