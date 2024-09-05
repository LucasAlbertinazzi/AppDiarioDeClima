using AppDiarioDeClima.Classes;
using AppDiarioDeClima.Services;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDiarioDeClima.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuscaClimaPage : ContentPage
    {
        #region 1- VARIÁVEIS
        ClimaServices apiclima = new ClimaServices();
        #endregion

        #region 2- CONSTRUTORES
        public BuscaClimaPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 3- MÉTODOS
        private void PreencheCardClima(InfoClima clima)
        {
            lblCidade.Text = clima.Cidade;
            lblData.Text = clima.DataHora.ToShortDateString();
            lblTemp.Text = clima.Temperatura;
            lblTempMin.Text = $"Min: {clima.TemperaturaMinima}";
            lblTempMax.Text = $"Max: {clima.TemperaturaMaxima}";
            lblClima.Text = clima.Descricao;
        }

        private async Task ExibeGrafico(InfoClima clima)
        {
            if (clima == null) { await DisplayAlert("Aviso","Nenhuma informação encontrada!","OK"); return; };

            chartLayout.Children.Clear();
            frameClima.IsVisible = true;
            chartFrame.IsVisible = true;
            chartLayout.IsVisible = true;

            PreencheCardClima(clima);

            lblTitulo.Text = $"Variações de Temperatura do dia {DateTime.Now:dd/MM/yyyy}";

            var temperatures = ObterTemperaturasPorHora(DateTime.Now,clima);

            var chart = new LineChart
            {
                Entries = temperatures,
                PointSize = 20,
                PointAreaAlpha = 50,
                LabelTextSize = 35,
                LineSize = 6,
                BackgroundColor = SKColor.Parse("#FFFFFF"),
                Margin = 20,
                IsAnimated = true,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
                MinValue = 10, 
                MaxValue = 40  
            };

            var chartView = new Microcharts.Forms.ChartView
            {
                Chart = chart,
                HeightRequest = 250,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = temperatures.Count * 100 
            };

            chartLayout.Children.Add(chartView);
        }


        private List<ChartEntry> ObterTemperaturasPorHora(DateTime date, InfoClima clima)
        {
            var temperatures = new List<ChartEntry>();
            Random random = new Random();

            int horaAtual = DateTime.Now.Hour;

            string min = clima.TemperaturaMinima.Replace("°C", "");
            string max = clima.TemperaturaMaxima.Replace("°C", "");

            int tempMin = (int)Math.Round(Convert.ToDouble(min)) -1;
            int tempMax = (int)Math.Round(Convert.ToDouble(max)) +1;

            for (int hour = 0; hour <= horaAtual; hour += 1)
            {
                float temperature = random.Next(tempMin, tempMax);
                temperatures.Add(new ChartEntry(temperature)
                {
                    Label = hour.ToString("00") + "h",
                    ValueLabel = temperature.ToString("0º"),
                    Color = SKColor.Parse("#FF6F61")
                });
            }

            return temperatures;
        }


        #endregion

        #region 4- EVENTOS DE CONTROLE
        private async void btnClima_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cidadeEntry.Text))
            {
                btnClima.IsVisible = false;
                loading.IsVisible = true;
                loading.IsRunning = true;

                await ExibeGrafico(await apiclima.BuscarClima(cidadeEntry.Text, Global.CodUser));

                btnClima.IsVisible = true;
                loading.IsVisible = false;
                loading.IsRunning = false;
            }
        }
        #endregion
    }
}