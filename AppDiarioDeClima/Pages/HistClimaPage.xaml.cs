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
            // Limpar qualquer gráfico existente
            chartLayout.Children.Clear();
            chartFrame.IsVisible = true;
            chartLayout.IsVisible = true;

            // Criar dados fictícios para o gráfico
            var temperatures = new List<ChartEntry>();
            for (int i = 0; i < 5; i++)
            {
                temperatures.Add(new ChartEntry(20 + i * 2)
                {
                    Label = selectedDate.AddDays(-i).ToString("dd/MM"),
                    ValueLabel = (20 + i * 2) + "°C",
                    Color = SKColor.Parse("#FF6F61")
                });
            }

            // Configurar o gráfico
            var chart = new LineChart
            {
                Entries = temperatures,
                PointSize = 35,
                PointAreaAlpha = 10,
                LabelTextSize = 40,
                LineSize = 8,
                BackgroundColor = SKColor.Parse("#FFFFFF"),
                Margin = 20,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelOrientation = Orientation.Horizontal,

            };

            // Adicionar o gráfico ao layout
            var chartView = new Microcharts.Forms.ChartView
            {
                Chart = chart,
                HeightRequest = 300
                
            };

            chartLayout.Children.Add(chartView);
        }


        private void LoadData()
        {
            List<InfoClima> lastSearches = BuscaHistorico();
            ultimosClimas.ItemsSource = lastSearches;
        }

        private List<InfoClima> BuscaHistorico()
        {
            return new List<InfoClima>
            {
                new InfoClima { Cidade = "São Paulo", Temperatura = "25°C", Clima = "Ensolarado", DataBusca = DateTime.Now.AddDays(-1) },
                new InfoClima { Cidade = "Rio de Janeiro", Temperatura = "30°C", Clima = "Nublado", DataBusca = DateTime.Now.AddDays(-2) },
                new InfoClima { Cidade = "Belo Horizonte", Temperatura = "22°C", Clima = "Chuvoso", DataBusca = DateTime.Now.AddDays(-3) },
                new InfoClima { Cidade = "Curitiba", Temperatura = "18°C", Clima = "Frio", DataBusca = DateTime.Now.AddDays(-4) },
                new InfoClima { Cidade = "Salvador", Temperatura = "28°C", Clima = "Parcialmente Nublado", DataBusca = DateTime.Now.AddDays(-5) }
            };
        }
        #endregion

        #region 4- EVENTOS DE CONTROLE
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is InfoClima selectedData)
            {
                // Remover a seleção do ListView
                ((ListView)sender).SelectedItem = null;

                // Exibir o gráfico
                ExibeGrafico(selectedData.DataBusca);
            }
        }
        #endregion
    }
}
