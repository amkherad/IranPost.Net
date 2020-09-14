using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IranPost.Net.Dto;
using IranPost.Net.Dto.Billing;
using IranPost.Net.Dto.Billing2;
using IranPost.Net.Dto.ChangeStatus;
using IranPost.Net.Dto.DayPing;
using IranPost.Net.Dto.EditOrder;
using IranPost.Net.Dto.GetPrice;
using IranPost.Net.Dto.GetStatus;
using IranPost.Net.Dto.NewOrder2;
using IranPost.Net.Dto.RejectExp;
using IranPost.Net.Dto.RejectId;

namespace IranPost.Net
{
    public class IranPostRestClient : IIranPostClient
    {
        public HttpClient HttpClient { get; }

        public AuthInfo AuthInfo { get; }


        public IranPostRestClient(
            HttpClient httpClient,
            AuthInfo authInfo
        )
        {
            if (httpClient is null) throw new ArgumentNullException(nameof(httpClient));

            if (string.IsNullOrWhiteSpace(authInfo.Username) || string.IsNullOrWhiteSpace(authInfo.Password))
            {
                throw new ArgumentException("authInfo should be a valid username/password pair.");
            }
            
            HttpClient = httpClient;
            AuthInfo = authInfo;
        }

        
        public Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<NewOrder2ResponseDto>> NewOrder2(
            NewOrder2RequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<EditOrderResponseDto>> EditOrder(
            EditOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<ChangeStatusResponseDto>> ChangeStatus(
            ChangeStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<GetStatusResponseDto>> GetStatus(
            GetStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<DayPingResponseDto>> DayPing(
            DayPingRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<BillingResponseDto>> Billing(
            BillingRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<Billing2ResponseDto>> Billing2(
            Billing2RequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<RejectExpResponseDto>> RejectExp(
            RejectExpRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto<RejectIdResponseDto>> RejectId(
            RejectIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }
    }
}