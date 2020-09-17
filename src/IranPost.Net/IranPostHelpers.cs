using System;
using System.Linq;
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
using IranPost.Net.Enums;

namespace IranPost.Net
{
    public static class IranPostHelpers
    {
        public static string JoinUrls(
            string left,
            string right
        )
        {
            if (left is null) return right;
            if (right is null) return left;

            if (left[left.Length - 1] == '/')
            {
                if (right[0] == '/')
                {
                    return left + right.Substring(1);
                }

                return left + right;
            }

            if (right[0] == '/')
            {
                return left + right;
            }

            return left + '/' + right;
        }

        public static string GetPostErrorMessage(
            PostErrors errorNumber
        )
        {
            switch (errorNumber)
            {
                case PostErrors.BadRequest: return "اطلاعات درخواستی ناقص میباشد.";
                case PostErrors.NetworkError: return "خطا در سطح شبکه رخ داده است.";
                case PostErrors.UnableToChangeState: return "امکان تغییر وضعیت این سفارش وجود ندارد.";
                case PostErrors.InvalidPostalCode: return "کد پستی وارد شده معتبر نمیباشد.";
                case PostErrors.InvalidAuthInfo: return "خطا در اطلاعات شناسایی ( نام کاربري ، Password Api ( و یا Ip سرور.";
                case PostErrors.InvalidShCode: return "خطا در شناسایی نام کاربري فروشنده";
                case PostErrors.MerchantExpiredOrBlocked: return "فروشنده مورد نظر منقضی و یا مسدود شده است.";
                case PostErrors.OrderIdNotFound: return "درخواست / شناسه مورد نظر یافت نشد.";
                case PostErrors.ForbiddenDeliveryType: return "امکان استفاده از این سرویس ارسال براي این فروشگاه امکان پذیر نمیباشد.";
                case PostErrors.InvalidCityIdOrStateId: return "خطا در شناسایی کد استان / شهرستان ارسال شده.";
                case PostErrors.UnableToSendToDestination: return "امکان ارسال مرسوله براي این مقصد میسر نمیباشد.";
                case PostErrors.OrderIdIsDuplicate: return "شناسه سفارش ارسال شده توسط شما تکراري میباشد.";
                case PostErrors.RequestDuplicate: return "این درخواست قبلا ثبت شده است.";
                case PostErrors.NoStateChangeToReport: return "تغییر وضعیتی براي نمایش وجود ندارد.";
                case PostErrors.InvalidStringForOrder: return "String تولید شده براي ثبت سفارش معتبر نمیباشد.";
                case PostErrors.NewOrderLimit: return "تعداد سفارش ارسالی بیشتر از حد مجاز میباشد. ( متد Order New (";
                case PostErrors.InvalidDeliveryType: return "روش ارسال / سرویس درخواستی نامعتبر میباشد.";
                case PostErrors.InvalidPaymentType: return "روش پرداخت درخواستی نامعتبر میباشد.";
                case PostErrors.MissingParameters: return "پارامترهاي الزامی به سامانه ارسال نگردیده است.";
                case PostErrors.InvalidWeight: return "وزن ارسال شده نامعتبر میباشد.";
                case PostErrors.InvalidProductPrice: return "قیمت ارسال شده براي سفارش نامعتبر میباشد.";
                case PostErrors.InvalidTransportPrice: return "هزینه ارسال ارسال شده نامعتبر میباشد.";
                case PostErrors.InvalidTax: return "مالیات بر ارزش افزوده ارسال شده نامعتبر میباشد.";
                case PostErrors.OrderLimit: return "تعداد سفارش ارسالی بیشتر از حد مجاز میباشد.";
                case PostErrors.InvalidMinimumPrice: return "حداقل مبلغ سفارش باید ۵۰۰۰ هزار تومان باشد.";
                default:
                    return "خطای ناشناخته سرویس پست.";
            }
        }


        public static BaseResponseDto<GetPriceResponseDto> ParseGetPriceResponse(
            string responseText
        )
        {
            var parts = responseText.Split(';').ToArray();

            long.TryParse(parts[0], out var price);
            long.TryParse(parts[1], out var priceNoInPlacePayment);
            long.TryParse(parts[2], out var tax);

            if (priceNoInPlacePayment == 0 && tax == 0)
            {
                return new BaseResponseDto<GetPriceResponseDto>
                {
                    Success = false,
                    Error = (PostErrors) price
                };
            }

            return new GetPriceResponseDto
            {
                PriceWithoutCod = priceNoInPlacePayment,
                Tax = tax,
                Price = price
            };
        }
        
        public static BaseResponseDto<NewOrder2ResponseDto> ParseNewOrder2Response(
            string responseText
        )
        {
            var parts = responseText.Split(';').ToArray();

            var errorCode = parts[0];

            if (!int.TryParse(errorCode, out var errorCodeInt) || errorCodeInt != 0 || parts[1].Length < 20)
            {
                return new BaseResponseDto<NewOrder2ResponseDto>
                {
                    Success = false,
                    Error = (PostErrors)errorCodeInt
                };
            }

            var originalTrackingCode = parts[2];

            return new BaseResponseDto<NewOrder2ResponseDto>
            {
                Success = parts[1].Length >= 20,
                Result = new NewOrder2ResponseDto
                {
                    PostInvoiceNumber = parts[1],
                    OrderId = originalTrackingCode,
                }
            };
        }

        public static BaseResponseDto<EditOrderResponseDto> ParseChangeResponse(
            string responseText
        )
        {
            throw new NotImplementedException();
        }

        public static BaseResponseDto<DayPingResponseDto> ParseDayPingResponse(
            string responseText
        )
        {
            throw new NotImplementedException();
        }

        public static BaseResponseDto<ChangeStatusResponseDto> ParseChangeStatusResponse(
            string responseText
        )
        {
            throw new NotImplementedException();
        }

        public static BaseResponseDto<GetStatusResponseDto> ParseGetStatusResponse(
            string responseText
        )
        {
            throw new NotImplementedException();
        }

        public static BaseResponseDto<BillingResponseDto> ParseBillingResponse(
            string responseText
        )
        {
            throw new NotImplementedException();
        }

        public static BaseResponseDto<Billing2ResponseDto> ParseBilling2Response(
            string responseText
        )
        {
            throw new NotImplementedException();
        }

        public static BaseResponseDto<RejectExpResponseDto> ParseRejectExpResponse(
            string responseText
        )
        {
            throw new NotImplementedException();
        }

        public static BaseResponseDto<RejectIdResponseDto> ParseRejectIdResponse(
            string responseText
        )
        {
            throw new NotImplementedException();
        }
    }
}