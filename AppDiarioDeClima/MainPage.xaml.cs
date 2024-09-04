using AppDiarioDeClima.Classes;
using AppDiarioDeClima.Pages;
using AppDiarioDeClima.Services;
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
        UserServices servicesUser = new UserServices();
        #endregion

        #region 2- CONSTRUTORES
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 3- MÉTODOS
        
        #endregion

        #region 4- EVENTOS DE CONTROLE
        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            InfoUser infos = new InfoUser();
            infos.Nome = username;
            infos.Senha = password;

            if (await servicesUser.AutenticarUsuarioAsync(infos))
            {
                Preferences.Set("user", username);

                await Navigation.PushAsync(new MenuPage());
            }
            else
            {
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
