using AppDiarioDeClima.Classes;
using AppDiarioDeClima.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDiarioDeClima.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPage : ContentPage
    {
        #region 1- VARIÁVEIS
        private bool isPasswordVisible = false;
        UserServices servicesUser = new UserServices();
        #endregion

        #region 2- CONSTRUTORES
        public CadastroPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 3- MÉTODOS

        #endregion

        #region 4- EVENTOS DE CONTROLE
        private async void btnCad_Clicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Erro", "Preencha todos os campos", "OK");
                return;
            }

            InfoUser infos = new InfoUser();
            infos.Nome = username;
            infos.Senha = password;

            btnCad.IsVisible = false;
            loading.IsVisible = true;
            loading.IsRunning = true;

            if (await servicesUser.CadastrarUsuarioAsync(infos))
            {
                await DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Aviso", "Este usuário já está cadastrado.", "OK");
            }

            btnCad.IsVisible = true;
            loading.IsVisible = false;
            loading.IsRunning = false;
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