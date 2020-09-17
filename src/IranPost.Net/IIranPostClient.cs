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
    public interface IIranPostClient
    {
        Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<NewOrder2ResponseDto>> NewOrder2(
            NewOrder2RequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<EditOrderResponseDto>> EditOrder(
            EditOrderRequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<ChangeStatusResponseDto>> ChangeStatus(
            ChangeStatusRequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<GetStatusResponseDto>> GetStatus(
            GetStatusRequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<GetStatusResponseDto[]>> GetStatus(
            GetStatusRequestDto[] request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<DayPingResponseDto[]>> DayPing(
            DayPingRequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<BillingResponseDto>> Billing(
            BillingRequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<Billing2ResponseDto>> Billing2(
            Billing2RequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<RejectExpResponseDto>> RejectExp(
            RejectExpRequestDto request,
            CancellationToken cancellationToken
        );

        Task<BaseResponseDto<RejectIdResponseDto>> RejectId(
            RejectIdRequestDto request,
            CancellationToken cancellationToken
        );
    }
}