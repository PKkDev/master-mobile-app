using Rg.Plugins.Popup.Pages;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.MVVM;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Popups.ChooseCategory
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseCategoryPopup : PopupPage
    {
        ChooseCategoryPopupViewModel _viewModel;

        public ChooseCategoryPopup(
            PaymentCategory selectedCategory, List<PaymentCategory> categories, RelyCommand categorySelectedCommand)
        {
            InitializeComponent();
            BindingContext = _viewModel = new ChooseCategoryPopupViewModel(selectedCategory, categories, categorySelectedCommand);
        }
    }
}