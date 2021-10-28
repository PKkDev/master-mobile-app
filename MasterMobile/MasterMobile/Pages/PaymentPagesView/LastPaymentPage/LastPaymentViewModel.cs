using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using MasterMobile.Domain.Model;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.MVVM;
using MasterMobile.Pages.PaymentPagesView.PaymentAnayzePage;
using MasterMobile.Popups.PaymentDetail;
using MasterMobile.Templates.PageState;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterMobile.Pages.PaymentPagesView.LastPaymentPage
{
    public class LastPaymentViewModel : BasePaymentViewModel
    {
        public ObservableCollection<GroupedLastPyments> Payments { get; set; }
        public RelyCommand RefreshItemsCommand { get; set; }
        public RelyCommand LoadMoreItemsCommand { get; set; }

        public RelyCommand DeletePaymentCommand { get; set; }
        public RelyCommand ViewDetailCommand { get; set; }

        public RelyCommand ViewAnalyzeCommand { get; set; }
        public RelyCommand AddPaymentCommand { get; set; }

        public DateTime LastViewDate { get; set; } = default;

        public LastPaymentViewModel()
        {
            Title = "last payment";
            Payments = new ObservableCollection<GroupedLastPyments>();

            RefreshItemsCommand = new RelyCommand(async (a) => await OnRefreshItemsCommand(true));
            LoadMoreItemsCommand = new RelyCommand(async (a) => await OnLoadMoreItemsCommand());

            AddPaymentCommand = new RelyCommand(async (param)
                => await Shell.Current.GoToAsync(nameof(NewPaymentPage)));

            ViewAnalyzeCommand = new RelyCommand(async (param)
                => await Shell.Current.GoToAsync($"{nameof(PaymentAnayzePage)}?{nameof(PaymentAnayzePageViewModel.MonthNumber)}={DateTime.Today.Month}"));

            ViewDetailCommand = new RelyCommand(async (param) =>
            {
                PopupPage popup = new PaymentDetialPopup(((LastPayment)param).Id);
                await PopupNavigation.Instance.PushAsync(popup);
            });


            DeletePaymentCommand = new RelyCommand(async (param) =>
            {
                try
                {
                    var item = param as LastPayment;
                    await PaymentService.RemovePymentAsync(item.Id);
                    IsBusy = true;
                }
                catch (Exception e)
                {
                    var error = new ErrorMessage(e.Message);
                    MessagingCenter.Send(error, "Error");
                }
            });
        }

        private async Task OnRefreshItemsCommand(bool forceRefresh)
        {
            // IsBusy = true;
            try
            {
                Payments.Clear();
                LastViewDate = default;
                var items = await PaymentService.GetLastPaymentAsync(null, forceRefresh);
                foreach (var item in items)
                    Payments.Add(item);
                PageState = PageStates.Normal;
            }
            catch (Exception e)
            {
                var error = new ErrorMessage(e.Message);
                MessagingCenter.Send(error, "Error");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnLoadMoreItemsCommand()
        {
            try
            {
                if (Payments.Any())
                {
                    var lastGroup = Payments.Last();
                    var lastDay = lastGroup.Last().Day;

                    if (LastViewDate != lastDay)
                    {
                        LastViewDate = lastDay;

                        var items = await PaymentService.GetLastPaymentAsync(lastDay);
                        foreach (var item in items)
                            Payments.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                var error = new ErrorMessage(e.Message);
                MessagingCenter.Send(error, "Error");
            }
        }

        public async void OnAppearing()
        {
            // IsBusy = true;
            PageState = PageStates.Loading;
            await OnRefreshItemsCommand(true);
        }
    }
}