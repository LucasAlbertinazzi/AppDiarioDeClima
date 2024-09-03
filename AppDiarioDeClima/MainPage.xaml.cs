using AppDiarioDeClima.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppDiarioDeClima
{
    public partial class MainPage : ContentPage
    {
        #region 1- VARIÁVEIS
        private bool isPasswordVisible = false;
        #endregion

        #region 2- CONSTRUTORES
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 3- MÉTODOS
        private bool ValidarLogin(string username, string password)
        {
            return username == "lucas" && password == "01234";
        }
        #endregion

        #region 4- EVENTOS DE CONTROLE
        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            // Validar o login
            if (ValidarLogin(username, password))
            {
                // Salvar o nome do usuário nas preferências
                Preferences.Set("user", username);

                // Navegar para a tela do MenuPage
                await Navigation.PushAsync(new MenuPage());
            }
            else
            {
                // Exibir alerta de erro se a validação falhar
                await DisplayAlert("Erro", "Usuário ou senha inválidos", "OK");
            }
        }

        private async void btnCadastrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroPage());
        }

        private void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            passwordEntry.IsPassword = !isPasswordVisible;

            ((ImageButton)sender).Source = isPasswordVisible ? "eye_off_icon.png" : "eye_icon.png";
        }
        #endregion
    }
}
