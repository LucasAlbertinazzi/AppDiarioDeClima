using AppDiarioDeClima.Classes;
using AppDiarioDeClima.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if(await servicesUser.CadastrarUsuarioAsync(infos))
            {
                await DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Aviso", "Este usuário já está cadastrado.", "OK");
            }
        }

        private void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
        {
            // Alterna a visibilidade da senha
            isPasswordVisible = !isPasswordVisible;
            passwordEntry.IsPassword = !isPasswordVisible;

            // Atualiza o ícone do botão (eye_icon.png = mostrar, eye_off_icon.png = ocultar)
            ((ImageButton)sender).Source = isPasswordVisible ? "eye_off_icon.png" : "eye_icon.png";
        }
        #endregion
    }
}