namespace IranPost.Net.Enums
{
    public enum PostErrors
    {
        NoError = 0,
        BadRequest = 1,
        UnableToChangeState = 3,
        InvalidPostalCode = 101,
        InvalidAuthInfo = 401,
        InvalidShCode = 402,
        MerchantExpiredOrBlocked = 403,
        OrderIdNotFound = 404,
        ForbiddenDeliveryType = 405,
        InvalidCityIdOrStateId = 502,
        UnableToSendToDestination = 503,
        OrderIdIsDuplicate = 505,
        RequestDuplicate = 600,
        NoStateChangeToReport = 601,
        InvalidStringForOrder = 800,
        NewOrderLimit = 801,
        InvalidDeliveryType = 802,
        InvalidPaymentType = 803,
        MissingParameters = 804,
        InvalidWeight = 805,
        InvalidProductPrice = 806,
        InvalidTransportPrice = 807,
        InvalidTax = 808,
        OrderLimit = 900
    }
}