using MasterMobile.Domain.Model.Payments;
using MasterMobile.MVVM;
using MasterMobile.Pages.PaymentPagesView;
using MasterMobile.Templates.PageState;

namespace MasterMobile.Popups.PaymentDetail
{
    public class PaymentDetailPopupViewModel : BasePaymentViewModel
    {
        private int _paymentId;
        public int PaymentId
        {
            get => _paymentId;
            set => OnSetNewValue(ref _paymentId, value);
        }

        private PaymentDetailModel _paymentDetail;
        public PaymentDetailModel PaymentDetail
        {
            get => _paymentDetail;
            set => OnSetNewValue(ref _paymentDetail, value);
        }

        public RelyCommand RefreshDataCommand { get; set; }

        public PaymentDetailPopupViewModel(int paymentId)
        {
            PaymentId = paymentId;
            RefreshDataCommand = new RelyCommand((param) => LoadPaymentDetail(PaymentId));
        }

        public void OnAppearing()
        {
            PageState = PageStates.Loading;
            LoadPaymentDetail(PaymentId);
        }

        private async void LoadPaymentDetail(int paymentId)
        {
            PaymentDetail = await PaymentService.GetPaymentDetail(paymentId);
            IsBusy = false;
            PageState = PageStates.Normal;
        }
    }
}
