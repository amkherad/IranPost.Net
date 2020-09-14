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
    public class IranPostSoapClient : IIranPostClient
    {
        public Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<NewOrder2ResponseDto>> NewOrder2(
            NewOrder2RequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<EditOrderResponseDto>> EditOrder(
            EditOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<ChangeStatusResponseDto>> ChangeStatus(
            ChangeStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<GetStatusResponseDto>> GetStatus(
            GetStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<DayPingResponseDto>> DayPing(
            DayPingRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<BillingResponseDto>> Billing(
            BillingRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<Billing2ResponseDto>> Billing2(
            Billing2RequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<RejectExpResponseDto>> RejectExp(
            RejectExpRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseDto<RejectIdResponseDto>> RejectId(
            RejectIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            throw new System.NotImplementedException();
        }
    }
}