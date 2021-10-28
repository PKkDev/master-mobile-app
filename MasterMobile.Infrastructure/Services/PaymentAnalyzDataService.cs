using Refit;
using MasterMobile.Domain.Model.Payments;
using MasterMobile.Infrastructure.ApiContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMobile.Infrastructure.Services
{
    public class PaymentAnalyzDataService
    {
        private Dictionary<string, string> _colorMapper;

        public PaymentAnalyzDataService()
        {
            _colorMapper = new Dictionary<string, string>();
            _colorMapper.Add("products", "#f39c12");
            _colorMapper.Add("pharmacy", "#27ae60");
            _colorMapper.Add("automobile", "#2980b9");
            _colorMapper.Add("other", "#7f8c8d");
            _colorMapper.Add("entertainment", "#c0392b");
            _colorMapper.Add("vika", "#8c7ae6");
        }

        public string GetColorForCategory(string category)
        {
            if (_colorMapper.TryGetValue(category.ToLower(), out var color))
                return color;
            return "#000";
        }

        public async Task<PaymentAnalyzeRes> GetMonthAnalyzeAsync(int monthNumber)
        {
            try
            {
                var api = RestService.For<IPaymentApiController>("https://shake.azurewebsites.net");
                var responseDto = await api.GetPaymentAnalyzAsync(monthNumber);

                var result = new PaymentAnalyzeRes(responseDto.StartPeriod, responseDto.EndPeriod);
                result.TotalPay = new TotalPay(responseDto.MonthPay, responseDto.Dynamic);
                responseDto.CategoryPays
                    .OrderByDescending(x => x.Pay)
                    .ToList()
                    .ForEach(x => result.CategoryPays.Add(new CategoryPay(x.CategoryName, x.Pay)));
                return result;
            }
            catch (Exception)
            {
                throw new Exception("get error");
            }
        }
    }
}
