using Refit;
using MasterMobile.Domain.DTO.Payment;
using MasterMobile.Domain.DTO.User;
using MasterMobile.Domain.Query.Payment;
using MasterMobile.Domain.Query.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MasterMobile.Infrastructure.ApiContract
{
    [Headers("Accept: application/json")]
    public interface IPaymentApiController
    {
        [Get("/payment/cat")]
        Task<IEnumerable<PaymentCategoryDto>> GetCategoriesAsync(
            CancellationToken ct = default);

        [Get("/payment/pt")]
        Task<IEnumerable<LastPaymentDto>> GetLastPaymentAsync(
            [AliasAs("lastDate")] DateTime? lastDate, CancellationToken ct = default);

        [Get("/payment/pt-detail")]
        Task<PaymentDetailDto> GetPaymentDetailAsync(
          [AliasAs("payId")] int payId, CancellationToken ct = default);

        [Post("/payment")]
        Task AddPaymentAsync(
          [Body] AddPaymentQuery query, CancellationToken ct = default);

        [Delete("/payment")]
        Task RemovePaymentAsync(
          [AliasAs("paymentId")] int paymentId, CancellationToken ct = default);



        [Get("/payment/check")]
        Task<PaymentAnalyzeDto> GetPaymentAnalyzAsync(
            [AliasAs("monthNumber")] int monthNumber, CancellationToken ct = default);
        [Get("/payment/check/pays")]
        Task<IEnumerable<MonthPaymentDto>> GetMonthPaymentAsync(
            [AliasAs("monthNumber")] int monthNumber, CancellationToken ct = default);

        //TODO
        /// <summary>
        /// not used
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="paymentId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [Post("/user/login")]
        Task<UserDto> LogInAsync(
            [Body] LogInQuery query, CancellationToken ct = default);

        //TODO
        /// <summary>
        /// not used
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [Get("/user/test")]
        Task TestLogInAsync(CancellationToken ct = default);
    }
}
