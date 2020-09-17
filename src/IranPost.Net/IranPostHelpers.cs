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
                case 3: return "امکان تغییر وضعیت این سفارش وجود ندارد.";
                case 101: return "کد پستی وارد شده معتبر نمیباشد.";
                case 401: return "خطا در اطلاعات شناسایی ( نام کاربري ، Password Api ( و یا Ip سرور.";
                case 402: return "خطا در شناسایی نام کاربري فروشنده";
                case 403: return "فروشنده مورد نظر منقضی و یا مسدود شده است.";
                case 404: return "درخواست / شناسه مورد نظر یافت نشد.";
                case 405: return "امکان استفاده از این سرویس ارسال براي این فروشگاه امکان پذیر نمیباشد.";
                case 502: return "خطا در شناسایی کد استان / شهرستان ارسال شده.";
                case 503: return "امکان ارسال مرسوله براي این مقصد میسر نمیباشد.";
                case 505: return "شناسه سفارش ارسال شده توسط شما تکراري میباشد.";
                case 600: return "این درخواست قبلا ثبت شده است.";
                case 601: return "تغییر وضعیتی براي نمایش وجود ندارد.";
                case 800: return "String تولید شده براي ثبت سفارش معتبر نمیباشد.";
                case 801: return "تعداد سفارش ارسالی بیشتر از حد مجاز میباشد. ( متد Order New (";
                case 802: return "روش ارسال / سرویس درخواستی نامعتبر میباشد.";
                case 803: return "روش پرداخت درخواستی نامعتبر میباشد.";
                case 804: return "پارامترهاي الزامی به سامانه ارسال نگردیده است.";
                case 805: return "وزن ارسال شده نامعتبر میباشد.";
                case 806: return "قیمت ارسال شده براي سفارش نامعتبر میباشد.";
                case 807: return "هزینه ارسال ارسال شده نامعتبر میباشد.";
                case 808: return "مالیات بر ارزش افزوده ارسال شده نامعتبر میباشد.";
                case 900: return "تعداد سفارش ارسالی بیشتر از حد مجاز میباشد.";
                case 5000: return "حداقل مبلغ سفارش باید ۵۰۰۰ هزار تومان باشد.";
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