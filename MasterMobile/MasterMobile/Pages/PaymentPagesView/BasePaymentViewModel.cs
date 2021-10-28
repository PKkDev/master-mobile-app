using MasterMobile.Infrastructure.Services;
using MasterMobile.MVVM;
using Xamarin.Forms;

namespace MasterMobile.Pages.PaymentPagesView
{
    public abstract class BasePaymentViewModel : BaseViewModel
    {
        public PaymentDataService PaymentService => DependencyService.Get<PaymentDataService>();
        public PaymentCategoriesDataService CategoryService => DependencyService.Get<PaymentCategoriesDataService>();
        public PaymentAnalyzDataService PaymentAnalyzService => DependencyService.Get<PaymentAnalyzDataService>();

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => OnSetNewValue(ref _isBusy, value);
        }
    }
}
