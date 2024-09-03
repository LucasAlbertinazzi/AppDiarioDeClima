using Microcharts;
using SkiaSharp;
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
    public partial class BuscaClimaPage : ContentPage
    {
        #region 1- VARIÁVEIS

        #endregion

        #region 2- CONSTRUTORES
        public BuscaClimaPage()
        {
            InitializeComponent();
        }
        #endregion

        #region 3- MÉTODOS
        private void ExibeGrafico()
        {
            chartLayout.Children.Clear();
            frameClima.IsVisible = true;
            chartFrame.IsVisible = true;
            chartLayout.IsVisible = true;

            var titleLabel = new Label
            {
                Text = $"Variações de Temperatura do dia {DateTime.Now:dd/MM/yyyy}",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 10, 0, 10)
            };

            chartLayout.Children.Add(titleLabel);

            var temperatures = ObterTemperaturasPorHora(DateTime.Now);

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

            // Obter a hora atual
            int horaAtual = DateTime.Now.Hour;

            for (int hour = 0; hour <= horaAtual; hour += 2)
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


        #endregion

        #region 4- EVENTOS DE CONTROLE
        private void btnClima_Clicked(object sender, EventArgs e)
        {
            ExibeGrafico();
        }
        #endregion
    }
}