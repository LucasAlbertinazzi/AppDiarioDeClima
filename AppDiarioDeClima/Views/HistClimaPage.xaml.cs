using AppDiarioDeClima.Classes;
using AppDiarioDeClima.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDiarioDeClima.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistClimaPage : ContentPage
    {
        #region 1- VARIÁVEIS
        ClimaServices apiclima = new ClimaServices();
        #endregion

        #region 2- CONSTRUTORES
        public HistClimaPage()
        {
            InitializeComponent();
            ultimosClimas.ItemTapped += OnItemTapped;
            LoadData();
        }
        #endregion

        #region 3- MÉTODOS

        private void ExibeGrafico(InfoClima clima)
        {
            Grid.SetRowSpan(ultimosClimas, 1);
            chartLayout.Children.Clear();
            chartFrame.IsVisible = true;
            chartLayout.IsVisible = true;

            lblTitulo.Text = $"Variações de Temperatura do dia {clima.DataHora.ToShortDateString()}";

            var temperatures = ObterTemperaturasPorHora(clima.DataHora, clima);

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
                HeightRequest = 200,
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

            if (date.Date < DateTime.Now.Date)
            {
                horaAtual = 24;
            }

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

        private async void LoadData()
        {
            List<InfoClima> lastSearches = await apiclima.ObterUltimosRegistros(Global.CodUser);

            if (lastSearches == null) { await DisplayAlert("Aviso", "Nenhuma informação encontrada!", "OK"); return; };

            ultimosClimas.ItemsSource = lastSearches;
        }

        #endregion

        #region 4- EVENTOS DE CONTROLE
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is InfoClima selecionado)
            {
                ((ListView)sender).SelectedItem = null;

                ExibeGrafico(selecionado);
            }
        }
        #endregion
    }
}
