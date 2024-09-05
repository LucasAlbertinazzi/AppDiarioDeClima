using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDiarioDeClima.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        #region 1- VARIÁVEIS

        #endregion

        #region 2- CONSTRUTORES
        public MenuPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 3- MÉTODOS

        #endregion

        #region 4- EVENTOS DE CONTROLE
        private async void OnBuscaClima(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BuscaClimaPage());
        }

        private async void OnHistoricoClima(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistClimaPage());
        }
        #endregion
    }
}