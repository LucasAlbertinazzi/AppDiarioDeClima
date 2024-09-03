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
            // Lógica para cadastro do usuário
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Erro", "Preencha todos os campos", "OK");
                return;
            }

            // Aqui você pode adicionar a lógica de cadastro, como salvar no banco de dados, etc.
            await DisplayAlert("Sucesso", "Cadastro realizado com sucesso!", "OK");

            // Navegar de volta para a página de login, por exemplo
            await Navigation.PopAsync();
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