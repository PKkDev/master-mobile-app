using Refit;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.Domain.Query.Payment;
using MasterMobile.Infrastructure.ApiContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterMobile.Infrastructure.Services
{
    public class PaymentDataService
    {
        private List<GroupedLastPyments> _lastPayments { get; set; }

        public PaymentDataService()
        {
            _lastPayments = new List<GroupedLastPyments>();
        }

        public async Task<IEnumerable<GroupedLastPyments>> GetLastPaymentAsync(
            DateTime? lastDate, bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh)
                    _lastPayments.Clear();

                var api = RestService.For<IPaymentApiController>("https://shake.azurewebsites.net");
                var result = await api.GetLastPaymentAsync(lastDate);

                var groupByDate = result.GroupBy(x => x.Day)
                    .Select(x => new GroupedLastPyments(x.Key.ToString("dd MMMM"), x.Select(y => new LastPayment()
                    {
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(y.ImageSource)),
                        Cost = y.Cost,
                        Day = y.Day,
                        Time = y.Time,
                        Id = y.Id
                    }).ToList()));

                _lastPayments.AddRange(groupByDate);
                return groupByDate;
            }
            catch (Exception e)
            {
                throw new Exception("get error");
            }
        }

        public async Task RemovePymentAsync(int paymentId)
        {
            try
            {
                var api = RestService.For<IPaymentApiController>("https://shake.azurewebsites.net");
                await api.RemovePaymentAsync(paymentId);
            }
            catch (Exception e)
            {
                throw new Exception("not removed");
            }
        }

        public async Task AddPymentAsync(
            int categoryId, int subCategoryId, int coast, DateTime date, TimeSpan time, string description)
        {
            try
            {
                var insDate = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);

                var query = new AddPaymentQuery()
                {
                    CategoryId = categoryId,
                    SubCategoryId = subCategoryId,
                    Coast = coast,
                    DatePay = insDate,
                    Description = description
                };
                var api = RestService.For<IPaymentApiController>("https://shake.azurewebsites.net");
                await api.AddPaymentAsync(query);
            }
            catch (Exception)
            {
                throw new Exception("not added");
            }
        }

        public async Task<PaymentDetailModel> GetPaymentDetail(int paymentId)
        {
            try
            {
                var api = RestService.For<IPaymentApiController>("https://shake.azurewebsites.net");
                var dto = await api.GetPaymentDetailAsync(paymentId);
                var result = new PaymentDetailModel()
                {
                    ImageSource = ImageSource.FromStream(() => new MemoryStream(dto.ImageSource)),
                    Cost = dto.Cost,
                    Date = dto.Date,
                    CategoryName = dto.CategoryName,
                    SubCategoryName = dto.SubCategoryName,
                    Description = dto.Description ?? ""
                };
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("detail error");
            }
        }
    }
}
