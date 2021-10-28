using Microcharts;
using SkiaSharp;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.MVVM;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MasterMobile.Pages.PaymentPagesView.PaymentAnayzePage
{
    [QueryProperty(nameof(MonthNumber), nameof(MonthNumber))]
    public class PaymentAnayzePageViewModel : BasePaymentViewModel
    {

        private Chart _distanceChart;
        public Chart DistanceChart
        {
            get => _distanceChart;
            set => OnSetNewValue(ref _distanceChart, value);
        }

        private int _monthNumber;
        public int MonthNumber
        {
            get => _monthNumber;
            set => OnSetNewValue(ref _monthNumber, value);
        }

        private PaymentAnalyzeRes _monthData;
        public PaymentAnalyzeRes MonthData
        {
            get => _monthData;
            set => OnSetNewValue(ref _monthData, value);
        }

        public ObservableCollection<CategoryPay> CategoriesPay { get; set; }

        public RelyCommand RefreshDataCommand { get; set; }
        public RelyCommand LoadPrevMonthDataCommand { get; set; }
        public RelyCommand LoadNextMonthDataCommand { get; set; }

        public PaymentAnayzePageViewModel()
        {
            CategoriesPay = new ObservableCollection<CategoryPay>();
            Title = "Payment analyze";
            RefreshDataCommand = new RelyCommand((param) => LoadMonthAnalyze(MonthNumber));

            LoadPrevMonthDataCommand = new RelyCommand((param) =>
            {
                MonthNumber--;
                LoadMonthAnalyze(MonthNumber);
            }
            );
            LoadNextMonthDataCommand = new RelyCommand((param) =>
            {
                MonthNumber++;
                LoadMonthAnalyze(MonthNumber);
            }
            );
        }

        public void OnAppearing()
        {
            IsBusy = true;
            LoadMonthAnalyze(MonthNumber);
        }

        private async void LoadMonthAnalyze(int monthNumber)
        {
            IsBusy = true;

            MonthData = await PaymentAnalyzService.GetMonthAnalyzeAsync(monthNumber);

            CategoriesPay.Clear();
            MonthData.CategoryPays.ForEach(cat => CategoriesPay.Add(cat));

            DistanceChart = new DonutChart()
            {
                LabelTextSize = 25,
                LabelMode = LabelMode.LeftAndRight,
                GraphPosition = GraphPosition.AutoFill,
                Entries = MonthData.CategoryPays.Select(x => new ChartEntry(x.Pay)
                {
                    Color = SKColor.Parse(PaymentAnalyzService.GetColorForCategory(x.CategoryName)),
                    Label = $"{x.CategoryName}",
                    ValueLabel = $"{x.Pay}"
                }).ToList()
            };

            IsBusy = false;
        }
    }
}
