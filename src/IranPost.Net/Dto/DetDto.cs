using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IranPost.Net.Dto
{
    public class DetDto
    {
        private static readonly Regex MatchIranianPostalCode =
            new Regex(@"^(\d{5}-?\d{5})$", options: RegexOptions.Compiled | RegexOptions.IgnoreCase);


        private static readonly Dictionary<string, string> NormalNumberChars = new Dictionary<string, string>
        {
            {"۰", "0"},
            {"۹", "9"},
            {"۸", "8"},
            {"۷", "7"},
            {"۶", "6"},
            {"۵", "5"},
            {"۴", "4"},
            {"۳", "3"},
            {"۲", "2"},
            {"۱", "1"},
        };


        public string Phone { get; set; }
        public string OrderId { get; set; }
        public string ShCode { get; set; }
        public int WeightInGrams { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPriceInIrr { get; set; }
        public string Address { get; set; }
        public string AddressPostalCode { get; set; }
        public string Email { get; set; }
        public string BuyerIpAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Tax { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }

        public int Tip { get; set; }
        public bool IsCod { get; set; }


        private bool IsValidIranPostalCode(
            string postalCode
        )
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                return false;
            }

            return MatchIranianPostalCode.IsMatch(postalCode);
        }

        private string NormalizeUnicodeNumbers(
            string number
        )
        {
            // replace persian characters
            foreach (var kv in NormalNumberChars)
            {
                if (number.Contains(kv.Key))
                {
                    number = number.Replace(kv.Key, kv.Value);
                }
            }

            return number;
        }

        public string ValidPostalCode
        {
            get
            {
                var result = NormalizeUnicodeNumbers(AddressPostalCode);
                if (!IsValidIranPostalCode(result) || result.Equals("1111111111"))
                {
                    result = GeneratePostalCode(CityId);

                    if (result.Equals("1111111111"))
                    {
                        result = "1311111111";
                    }
                }

                return result;
            }
        }


        private string GeneratePostalCode(
            int cityId
        )
        {
            var postalCode = cityId.ToString().PadRight(10, '1');
            return postalCode;
        }

        public string DetString
        {
            get
            {
                var productName = ProductName;

                productName = productName.Replace('^', ' ');

                var receiverFirstName = ReceiverFirstName.Replace('^', ' ');
                var receiverLastName = ReceiverLastName.Replace('^', ' ');
                var addressPostalCode = ValidPostalCode.Replace('^', ' ');
                var address = Address.Replace('^', ' ');
                var shCode = ShCode.Replace('^', ' ');
                var phone = Phone.Replace('^', ' ');
                var email = Email?.Replace('^', ' ') ?? "--";
                var buyerIpAddress = BuyerIpAddress?.Replace('^', ' ') ?? "0";
                var orderId = OrderId.Replace('^', ' ');


                var sb = new StringBuilder(200);

                sb.Append(shCode).Append('^'); // ShCode
                sb.Append(phone).Append('^'); // PharmacyMobile
                sb.Append(orderId).Append('^'); // OrderId
                sb.Append(ProvinceId).Append('^'); //ProvinceId
                sb.Append(CityId).Append('^'); // CityId
                sb.Append(productName).Append('^'); // ProductName
                sb.Append(WeightInGrams.ToString("0")).Append('^'); // WeightInGrams
                sb.Append(ProductPriceInIrr.ToString("0")).Append('^'); // ProductPriceInIrr
                sb.Append(Tip.ToString()).Append('^'); // Tip
                sb.Append(IsCod ? "0" : "1").Append('^'); // IsCod
                sb.Append(receiverFirstName).Append('-').Append(receiverLastName).Append('^'); // ReceiverFirstName
                sb.Append(address).Append('^'); // Address
                sb.Append(addressPostalCode).Append('^'); // AddressPostalCode
                sb.Append(email).Append('^'); // Email
                sb.Append(buyerIpAddress).Append('^'); // BuyerIpAddress
                sb.Append(TotalPrice.ToString("0")).Append('^'); // TotalPrice
                sb.Append(Tax.ToString("0")); // Tax

                return sb.ToString();
            }
        }

        public override string ToString() => DetString;
    }
}