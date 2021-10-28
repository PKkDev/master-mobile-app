using Rg.Plugins.Popup.Services;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MasterMobile.Popups.ChooseCategory
{
    public class ChooseCategoryPopupViewModel : BaseViewModel
    {
        public ObservableCollection<PaymentCategory> VisibleCategories { get; }
        private static List<PaymentCategory> _categories;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnSearchCategoryCommand(value);
            }
        }

        private PaymentCategory _selectedCategoryInList;
        public PaymentCategory SelectedCategoryInList
        {
            get => _selectedCategoryInList;
            set
            {
                OnSetNewValue(ref _selectedCategoryInList, value);
                SelectedCategory = value;
            }
        }


        private PaymentCategory _selectedCategory;
        public PaymentCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                OnSetNewValue(ref _selectedCategory, value);
            }
        }

        public RelyCommand CancelCommand { get; set; }
        public RelyCommand ConfirmCommand { get; set; }
        public RelyCommand SearchCategoryCommand { get; set; }
        public RelyCommand CategorySelectedCommand { get; set; }

        public ChooseCategoryPopupViewModel(
            PaymentCategory selectedCategory, List<PaymentCategory> categories, RelyCommand categorySelectedCommand)
        {
            _categories = categories;
            VisibleCategories = new ObservableCollection<PaymentCategory>(categories);

            SearchCategoryCommand = new RelyCommand((param) =>
            {
                OnSearchCategoryCommand((string)param);
            });

            CategorySelectedCommand = categorySelectedCommand;

            CancelCommand = new RelyCommand((param) => PopupNavigation.Instance.PopAsync());
            ConfirmCommand = new RelyCommand((param) =>
            {
                CategorySelectedCommand?.Execute(SelectedCategory);
                PopupNavigation.Instance.PopAsync();
            });

            SelectedCategory = selectedCategory;
        }

        private void OnSearchCategoryCommand(string text)
        {
            VisibleCategories.Clear();
            var filteredCountries = string.IsNullOrWhiteSpace(text)
                ? _categories
                : _categories.Where(country => country.Name.Contains(text));
            filteredCountries.ToList().ForEach(сountry => VisibleCategories.Add(сountry));
        }
    }
}
