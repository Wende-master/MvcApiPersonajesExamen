using MvcApiPersonajesExamen.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajesExamen.Services
{
    public class ServicePersonajes
    {
        private string ApiUrl;
        private MediaTypeWithQualityHeaderValue Header;
        public ServicePersonajes(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.ApiUrl =
                configuration.GetValue<string>("ApiUrls:ApiPersonajes");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Personajes";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            string request = "api/Personajes/" + idpersonaje;
            Personaje personaje =
                await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task<List<Personaje>> FindPersonajesSeriesBySerie(string serie)
        {
            string request = "api/Personajes/PersonajesSeries/" + serie;
            List<Personaje> personajesSerie =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajesSerie;
        }

        public async Task<List<string>> GetAllSeries()
        {
            string request = "api/Personajes/Series";
            List<string> data =
                await this.CallApiAsync<List<string>>(request);
            return data;
        }

        public async Task InsertPersonajeAsync(string nombre, string imagen, string serie)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/InsertPersonaje";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Personaje personaje = new Personaje();
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.Serie = serie;

                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(int id, string nombre, string imagen, string serie)
        {

            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/UpdatePersonaje";
                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Personaje personaje = new Personaje();
                personaje.IdPersonaje = id;
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.Serie = serie;

                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/DeletePersonaje/" + idpersonaje;

                client.BaseAddress = new Uri(this.ApiUrl);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
