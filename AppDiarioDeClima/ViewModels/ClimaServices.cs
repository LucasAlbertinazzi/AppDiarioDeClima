using AppDiarioDeClima.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppDiarioDeClima.ViewModels
{
    public class ClimaServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = Global.UrlApi;

        public ClimaServices()
        {
            _httpClient = new HttpClient();
        }

        public async Task<InfoClima> BuscarClima(string cidade, int codUsuario)
        {
            var url = $"{_baseUrl}/ClimaSup/busca-clima?cidade={cidade}&codusuario={codUsuario}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InfoClima>(json);
                }
                
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao se comunicar com a API: {ex.Message}");
            }
        }

        public async Task<List<InfoClima>> ObterUltimosRegistros(int codUsuario)
        {
            var url = $"{_baseUrl}/ClimaSup/ultimos-registros?codusuario={codUsuario}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<InfoClima>>(json);
                }
                else
                {
                    throw null;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao se comunicar com a API: {ex.Message}");
            }
        }

        public async Task<List<InfoClima>> BuscarHistoricoPorHora(double lat, double lon, long start, long end)
        {
            var url = $"{_baseUrl}/api/ClimaSup/historico-horario?lat={lat}&lon={lon}&start={start}&end={end}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<InfoClima>>(json);
                }
                else
                {
                    throw new Exception($"Erro ao buscar histórico: {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Erro ao se comunicar com a API: {ex.Message}");
            }
        }
    }
}
