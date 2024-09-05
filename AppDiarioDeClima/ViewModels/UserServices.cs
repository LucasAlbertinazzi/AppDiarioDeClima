using AppDiarioDeClima.Classes;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDiarioDeClima.ViewModels
{
    public class UserServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = Global.UrlApi;

        public UserServices()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> CadastrarUsuarioAsync(InfoUser novoUsuario)
        {
            var url = $"{_baseUrl}/usuarios/cadastro-usuario";
            var jsonContent = JsonConvert.SerializeObject(novoUsuario);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AutenticarUsuarioAsync(InfoUser usuarioLogin)
        {
            var url = $"{_baseUrl}/Usuarios/autenticacao";
            var jsonContent = JsonConvert.SerializeObject(usuarioLogin);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var codUsuarioStr = await response.Content.ReadAsStringAsync();

                if (int.TryParse(codUsuarioStr, out int codUsuario))
                {
                    if(codUsuario > 0)
                    {
                        Global.CodUser = codUsuario;
                        Global.NomeUser = usuarioLogin.Nome;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
