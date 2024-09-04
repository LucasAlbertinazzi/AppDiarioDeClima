using AppDiarioDeClima.Classes;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDiarioDeClima.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistClimaPage : ContentPage
    {
        #region 1- VARIÁVEIS
        string userLogado = Preferences.Get("user", "Usuário não encontrado");
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

        private void ExibeGrafico(DateTime selectedDate)
        {
            chartLayout.Children.Clear();
            chartFrame.IsVisible = true;
            chartLayout.IsVisible = true;

            var titleLabel = new Label
            {
                Text = $"Variações de Temperatura do dia {selectedDate:dd/MM/yyyy}",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 10, 0, 10)
            };

            chartLayout.Children.Add(titleLabel);

            var temperatures = ObterTemperaturasPorHora(selectedDate);

            var chart = new LineChart
            {
                Entries = temperatures,
                PointSize = 20,
                PointAreaAlpha = 50,
                LabelTextSize = 30,
                LineSize = 5,
                BackgroundColor = SKColor.Parse("#FFFFFF"),
                Margin = 15,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,
            };

            var chartView = new Microcharts.Forms.ChartView
            {
                Chart = chart,
                HeightRequest = 200
            };

            chartLayout.Children.Add(chartView);
        }


        private List<ChartEntry> ObterTemperaturasPorHora(DateTime date)
        {
            var temperatures = new List<ChartEntry>();
            Random random = new Random();

            for (int hour = 0; hour < 24; hour += 2) 
            {
                float temperature = random.Next(15, 35);
                temperatures.Add(new ChartEntry(temperature)
                {
                    Label = hour.ToString("00") + "h",
                    ValueLabel = temperature.ToString("0º"), 
                    Color = SKColor.Parse("#FF6F61")
                });
            }

            return temperatures;
        }


        private void LoadData()
        {
            List<InfoClima> lastSearches = BuscaHistorico();
            ultimosClimas.ItemsSource = lastSearches;
        }

        private List<InfoClima> BuscaHistorico()
        {
            return new List<InfoClima>();
        }
        #endregion

        #region 4- EVENTOS DE CONTROLE
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is InfoClima selectedData)
            {
                ((ListView)sender).SelectedItem = null;

                ExibeGrafico(selectedData.DataHora);
            }
        }
        #endregion
    }
}
