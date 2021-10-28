using Refit;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.Infrastructure.ApiContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterMobile.Infrastructure.Services
{
    public class PaymentCategoriesDataService
    {
        private List<PaymentCategory> _categories { get; set; }

        public PaymentCategoriesDataService()
        {
            _categories = new List<PaymentCategory>();
        }

        public async Task<IEnumerable<PaymentCategory>> GetCategoriesAsync(bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh)
                    _categories.Clear();

                var api = RestService.For<IPaymentApiController>("https://shake.azurewebsites.net");
                var result = await api.GetCategoriesAsync();

                var list = new List<PaymentCategory>();
                foreach (var cat in result)
                {
                    var newCat = new PaymentCategory()
                    {
                        Id = cat.Id,
                        Name = cat.Name,
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(cat.ImageSource)),
                        SubCategory = new List<PaymentCategory>()
                    };
                    foreach (var subCat in cat.SubCategory)
                    {
                        newCat.SubCategory.Add(new PaymentCategory()
                        {
                            Id = subCat.Id,
                            Name = subCat.Name,
                            ImageSource = ImageSource.FromStream(() => new MemoryStream(subCat.ImageSource)),
                        });
                    }
                    list.Add(newCat);
                }

                _categories = list;
                return list;
            }
            catch (Exception e)
            {
                throw new Exception("get error");
            }
        }
    }
}
