using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using MasterMobile.Domain.Model;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.MVVM;
using MasterMobile.Popups.ChooseCategory;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterMobile.Pages.PaymentPagesView.NewPaymentPage
{
    public class NewPaymentViewModel : BasePaymentViewModel
    {
        public ObservableCollection<PaymentCategory> Categories { get; set; }
        private PaymentCategory selectedCategory;
        public PaymentCategory SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                OnSetNewValue(ref selectedCategory, value);
                SelectedSubCategory = null;
                SubCategories.Clear();
                foreach (var sub in value.SubCategory)
                    SubCategories.Add(sub);
            }
        }

        public ObservableCollection<PaymentCategory> SubCategories { get; set; }
        private PaymentCategory selectedSubCategory;
        public PaymentCategory SelectedSubCategory
        {
            get { return selectedSubCategory; }
            set { OnSetNewValue(ref selectedSubCategory, value); }
        }

        private string _coast;
        public string Coast
        {
            get { return _coast; }
            set { OnSetNewValue(ref _coast, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { OnSetNewValue(ref _description, value); }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { OnSetNewValue(ref _selectedDate, value); }
        }

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get { return _selectedTime; }
            set { OnSetNewValue(ref _selectedTime, value); }
        }

        public RelyCommand CancelCommand { get; set; }
        public RelyCommand SaveCommand { get; set; }
        public RelyCommand ShowCategoryPopupCommand { get; set; }
        public RelyCommand ShowSubCategoryPopupCommand { get; set; }

        public NewPaymentViewModel()
        {
            Title = "add new payment";

            Categories = new ObservableCollection<PaymentCategory>();
            SubCategories = new ObservableCollection<PaymentCategory>();

            var CategorySelectedCommand = new RelyCommand((param) => SelectedCategory = param as PaymentCategory);
            ShowCategoryPopupCommand = new RelyCommand((param) =>
            {
                PopupPage popup = new ChooseCategoryPopup(SelectedCategory, Categories.ToList(), CategorySelectedCommand);
                PopupNavigation.Instance.PushAsync(popup);
            });

            var SubCategorySelectedCommand = new RelyCommand((param) => SelectedSubCategory = param as PaymentCategory);
            ShowSubCategoryPopupCommand = new RelyCommand((param) =>
            {
                PopupPage popup = new ChooseCategoryPopup(SelectedSubCategory, SubCategories.ToList(), SubCategorySelectedCommand);
                PopupNavigation.Instance.PushAsync(popup);
            });

            SaveCommand = new RelyCommand(
                async (param) => await OnSave(),
                (param) =>
                {
                    return !string.IsNullOrWhiteSpace(_coast);
                });
            CancelCommand = new RelyCommand(async (param) => await GoBack());

            LoadCategories();
            Coast = "500";
            SelectedDate = DateTime.Today;
            SelectedTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
        }

        private async void LoadCategories()
        {
            try
            {
                Categories.Clear();
                var items = await CategoryService.GetCategoriesAsync();
                foreach (var item in items)
                    Categories.Add(item);
                SelectedCategory = Categories.First();
                SelectedSubCategory = Categories.First().SubCategory.First();
            }
            catch (Exception e)
            {
                var error = new ErrorMessage(e.Message);
                MessagingCenter.Send(error, "Error");
            }
        }

        private async Task OnSave()
        {
            try
            {
                await PaymentService.AddPymentAsync(
                    selectedCategory.Id, selectedSubCategory.Id, Convert.ToInt32(Coast),
                    SelectedDate, SelectedTime, Description);
            }
            catch (Exception e)
            {
                var error = new ErrorMessage(e.Message);
                MessagingCenter.Send(error, "Error");
            }
            finally
            {
                await GoBack();
            }
        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
