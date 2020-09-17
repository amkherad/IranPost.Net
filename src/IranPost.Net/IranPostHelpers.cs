using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
using IranPost.Net.PersianDateTime;

namespace IranPost.Net
{
    public static class IranPostHelpers
    {
        private static readonly Regex MatchIranianPostalCode =
            new Regex(@"^(\d{5}-?\d{5})$", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);


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

        public static bool IsValidInvoiceNumber(
            string invoiceNumber
        )
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                return false;
            }

            if (invoiceNumber.Length != 20)
            {
                return false;
            }

            return invoiceNumber.All(char.IsNumber);
        }

        public static void ValidateInvoiceNumber(
            string invoiceNumber
        )
        {
            if (!IsValidInvoiceNumber(invoiceNumber))
            {
                throw new IranPostException($"Invalid invoice number. invoice was: '{invoiceNumber}'")
                {
                    Type = IranPostException.ExceptionType.ValidationInvoiceNumber
                };
            }
        }

        public static void ValidateWeight(
            int weight
        )
        {
            if (weight < 500)
            {
                throw new IranPostException($"The minimum value for weight is 500g.")
                {
                    Type = IranPostException.ExceptionType.ValidationWeight
                };
            }
        }

        public static void ValidateProductPrice(
            int productPrice
        )
        {
            if (productPrice < 50000)
            {
                throw new IranPostException($"The minimum value for productPrice is 50000.")
                {
                    Type = IranPostException.ExceptionType.ValidationProductPrice
                };
            }
        }

        public static bool IsValidPostalCode(
            string postalCode
        )
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                return false;
            }

            return MatchIranianPostalCode.IsMatch(postalCode);
        }

        public static void ValidatePostalCode(
            string postalCode
        )
        {
            if (!IsValidPostalCode(postalCode))
            {
                throw new IranPostException($"Invalid postal code is given. postal-code: '{postalCode}'")
                {
                    Type = IranPostException.ExceptionType.ValidationPostalCode
                };
            }
        }

        public static void AssertNotNull(
            string fieldName,
            string value
        )
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new IranPostException($"{fieldName} should not be empty or whitespace.")
                {
                    Type = IranPostException.ExceptionType.ValidationEmpty
                };
            }
        }

        public static string FormatDate(
            DateTimeOffset dateTimeOffset
        )
        {
            var persianDateTime = dateTimeOffset.ToPersianDateTime();

            return persianDateTime.ToString("");
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
                case PostErrors.InvalidAuthInfo:
                    return "خطا در اطلاعات شناسایی ( نام کاربري ، Password Api ( و یا Ip سرور.";
                case PostErrors.InvalidShCode: return "خطا در شناسایی نام کاربري فروشنده";
                case PostErrors.MerchantExpiredOrBlocked: return "فروشنده مورد نظر منقضی و یا مسدود شده است.";
                case PostErrors.OrderIdNotFound: return "درخواست / شناسه مورد نظر یافت نشد.";
                case PostErrors.ForbiddenDeliveryType:
                    return "امکان استفاده از این سرویس ارسال براي این فروشگاه امکان پذیر نمیباشد.";
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
                    Error = (PostErrors) errorCodeInt
                };
            }

            var invoiceNumber = parts[1];
            var originalTrackingCode = parts[2];

            return new BaseResponseDto<NewOrder2ResponseDto>
            {
                Success = IsValidInvoiceNumber(invoiceNumber),
                Result = new NewOrder2ResponseDto
                {
                    PostInvoiceNumber = invoiceNumber,
                    OrderId = originalTrackingCode,
                }
            };
        }

        public static BaseResponseDto<EditOrderResponseDto> ParseEditOrderResponse(
            string responseText
        )
        {
            var parts = responseText.Split(';');

            if (parts.Length != 4)
            {
                return new BaseResponseDto<EditOrderResponseDto>
                {
                    Success = false,
                    Error = PostErrors.NoError,
                };
            }

            var invoiceNumber = parts[0];

            if (!IsValidInvoiceNumber(invoiceNumber))
            {
                return new BaseResponseDto<EditOrderResponseDto>
                {
                    Success = false
                };
            }

            long.TryParse(parts[0], out var price);
            long.TryParse(parts[1], out var priceNoInPlacePayment);
            long.TryParse(parts[2], out var tax);

            if (priceNoInPlacePayment == 0 && tax == 0)
            {
                return new BaseResponseDto<EditOrderResponseDto>
                {
                    Success = false,
                    Error = (PostErrors) price
                };
            }

            return new EditOrderResponseDto
            {
                InvoiceNumber = invoiceNumber,

                PriceWithoutCod = priceNoInPlacePayment,
                Tax = tax,
                Price = price
            };
        }

        public static BaseResponseDto<ChangeStatusResponseDto> ParseChangeStatusResponse(
            string responseText
        )
        {
            if (responseText == "0")
            {
                return new BaseResponseDto<ChangeStatusResponseDto>
                {
                    Success = true,
                    
                };
            }
            
            return new BaseResponseDto<ChangeStatusResponseDto>
            {
                Success = false
            };
        }

        public static BaseResponseDto<GetStatusResponseDto[]> ParseGetStatusResponse(
            string responseText
        )
        {
            GetStatusResponseDto ParseRow(
                string row
            )
            {
                var parts = responseText.Split(';');

                if (parts.Length != 3)
                {
                    return null;
                }

                var latestStatus =(OrderStates) Enum.Parse(typeof(OrderStates), parts[0]);
                var latestStatusDatetime = DateTimeOffset.Parse(parts[1]);
                var invoiceNumber = parts[2];
                
                return new GetStatusResponseDto
                {
                    Id = invoiceNumber,
                    LatestStatus = latestStatus,
                    LatestStatusDateTime = latestStatusDatetime
                };
            }

            var rows = responseText.Split('-');
            var parsedRows = rows.Select(ParseRow).ToArray();

            return new BaseResponseDto<GetStatusResponseDto[]>
            {
                Success = parsedRows.All(r => !(r is null)),
                
                Result = parsedRows
            };
        }


        public static BaseResponseDto<DayPingResponseDto[]> ParseDayPingResponse(
            string responseText
        )
        {
            DayPingResponseDto ParseRow(
                string row
            )
            {
                var parts = row.Split('^');

                if (parts.Length != 7)
                {
                    return null;
                }

                var uniqueId = parts[0];
                var statusDateTime = parts[1];
                var invoiceNumber = parts[2];
                var orderId = parts[3];
                var status = parts[4];
                var settlementCode = parts[5];
                var price = parts[6];
                
                return new DayPingResponseDto
                {
                    
                };
            }

            var rows = responseText.Split(';');
            var parsedRows = rows.Select(ParseRow).ToArray();

            return new BaseResponseDto<DayPingResponseDto[]>
            {
                Success = parsedRows.All(r => !(r is null)),
                Result = parsedRows
            };
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