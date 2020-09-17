using System;
using System.Collections.Generic;
using System.Linq;
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
using IranPost.Net.Enums;

namespace IranPost.Net
{
    public class IranPostRestClient : IIranPostClient, IDisposable
    {
        private const string GetPriceUrl = "Price.asp";
        private const string PingUrl = "Ping.asp";
        private const string NewOrder2Url = "NewOrder2.asp";
        private const string ChangeStatusUrl = "Change.asp";
        private const string EditOrderUrl = "Edit.asp";
        private const string DayPingUrl = "DayPing.asp";
        private const string BillingUrl = "Billing.asp";
        private const string Billing2Url = "Billing2.asp";
        private const string RejectExpUrl = "RejectExp.asp";
        private const string RejectIdUrl = "RejectId.asp";


        public Uri RemoteServiceUri { get; }

        public HttpClient HttpClient { get; }

        public bool DisposeHttpClient { get; set; } = true;

        public AuthInfo AuthInfo { get; }

        public IRetryHandler RetryHandler { get; }

        public RemoteServiceEndpoints Endpoints { get; set; }


        public IranPostRestClient(
            Uri remoteServiceUri,
            HttpClient httpClient,
            AuthInfo authInfo,
            IRetryHandler retryHandler
        )
        {
            if (remoteServiceUri is null) throw new ArgumentNullException(nameof(remoteServiceUri));
            if (httpClient is null) throw new ArgumentNullException(nameof(httpClient));

            if (string.IsNullOrWhiteSpace(authInfo.Username) || string.IsNullOrWhiteSpace(authInfo.Password))
            {
                throw new ArgumentException("authInfo should be a valid username/password pair.");
            }

            RemoteServiceUri = remoteServiceUri;
            HttpClient = httpClient;
            AuthInfo = authInfo;
            RetryHandler = retryHandler;
        }


        public virtual void Dispose()
        {
            if (DisposeHttpClient)
            {
                HttpClient.Dispose();
            }
        }

        protected virtual string CreatePath(
            string relativePath
        )
        {
            return IranPostHelpers.JoinUrls(RemoteServiceUri.AbsoluteUri, relativePath);
        }

#pragma warning disable 1998
        protected virtual async Task<BaseResponseDto<T>> ThrowOnInvalidStatusCode<T>(
#pragma warning restore 1998
            HttpResponseMessage response
        )
        {
            return new BaseResponseDto<T>
            {
                Error = PostErrors.NetworkError,
                Success = false
            };
        }


        protected virtual Task<HttpResponseMessage> Send(
            string uri,
            Dictionary<string, object> values,
            CancellationToken cancellationToken
        )
        {
            var formattedValues = values.ToDictionary(
                k => k.Key,
                v => v.Value?.ToString()
            );

            formattedValues.TryAdd("Username", AuthInfo.Username);
            formattedValues.TryAdd("Password", AuthInfo.Password);

            var req = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new FormUrlEncodedContent(formattedValues)
            };

            var response = HttpClient.SendAsync(req, cancellationToken);

            return response;
        }


        public virtual async Task<BaseResponseDto<GetPriceResponseDto>> GetPrice(
            GetPriceRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            IranPostHelpers.ValidateWeight(request.Weight);
            IranPostHelpers.ValidateProductPrice(request.Price);
            IranPostHelpers.AssertNotNull("Shcode", request.Shcode);
            if (request.StateId == 0)
            {
                throw new IranPostException("StateId is zero.")
                {
                    Type = IranPostException.ExceptionType.ValidationEmpty
                };
            }

            if (request.CityId == 0)
            {
                throw new IranPostException("CityId is zero.")
                {
                    Type = IranPostException.ExceptionType.ValidationEmpty
                };
            }


            var path = Endpoints.GetPriceRelativeUrl ?? GetPriceUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Weight", request.Weight},
                            {"Price", request.Price},
                            {"Shcode", request.Shcode},
                            {"State", request.StateId},
                            {"City", request.CityId},
                            {"Tip", (int) request.Tip},
                            {"Cod", (int) request.PaymentType},
                            {"Showtype", 1},
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<GetPriceResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseGetPriceResponse(responseText);
        }

        public virtual async Task<BaseResponseDto<NewOrder2ResponseDto>> NewOrder2(
            NewOrder2RequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = Endpoints.NewOrder2Url ?? NewOrder2Url;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"OrderTip", (int) request.OrderTip},
                            {"Det", request.Det.DetString},
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<NewOrder2ResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseNewOrder2Response(responseText);
        }

        public virtual async Task<BaseResponseDto<EditOrderResponseDto>> EditOrder(
            EditOrderRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));


            IranPostHelpers.ValidateInvoiceNumber(request.Id);
            IranPostHelpers.ValidateWeight(request.Weight);
            IranPostHelpers.ValidateProductPrice(request.Price);
            IranPostHelpers.ValidatePostalCode(request.PostalCode);


            var path = Endpoints.EditOrderUrl ?? EditOrderUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Id", request.Id},
                            {"Pname", string.Join('-', request.ProductNames)},
                            {"Weight", request.Weight},
                            {"Cod", (int) request.PaymentType},
                            {"Price", request.Price},
                            {"Name", request.Name},
                            {"Address", request.Address},
                            {"Email", request.Email},
                            {"Tel", request.Tel},
                            {"Pcode", request.PostalCode},
                            {"Showtype", 1},
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<EditOrderResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseEditOrderResponse(responseText);
        }

        public virtual async Task<BaseResponseDto<ChangeStatusResponseDto>> ChangeStatus(
            ChangeStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (request.InvoiceNumbers is null) throw new ArgumentException(nameof(request));


            foreach (var invoice in request.InvoiceNumbers)
            {
                IranPostHelpers.ValidateInvoiceNumber(invoice);
            }


            var path = Endpoints.ChangeStatusUrl ?? ChangeStatusUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Id", string.Join(',', request.InvoiceNumbers)},
                            {"Status", (int) request.Status}
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<ChangeStatusResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseChangeStatusResponse(responseText);
        }

        public virtual async Task<BaseResponseDto<GetStatusResponseDto>> GetStatus(
            GetStatusRequestDto request,
            CancellationToken cancellationToken
        )
        {
            var result = await GetStatus(new[] {request}, cancellationToken);
            
            return new BaseResponseDto<GetStatusResponseDto>
            {
                Error = result.Error,
                Success = result.Success,
                Result = result.Result?[0]
            };
        }

        public virtual async Task<BaseResponseDto<GetStatusResponseDto[]>> GetStatus(
            GetStatusRequestDto[] request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));


            foreach (var rq in request)
            {
                IranPostHelpers.ValidateInvoiceNumber(rq.Id);
            }


            var path = Endpoints.PingUrl ?? PingUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Id", string.Join(',', request.Select(rq => rq.Id))},
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<GetStatusResponseDto[]>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseGetStatusResponse(responseText);
        }

        public virtual async Task<BaseResponseDto<DayPingResponseDto[]>> DayPing(
            DayPingRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = Endpoints.DayPingUrl ?? DayPingUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Changedate", IranPostHelpers.FormatDate(request.ChangeDate)},
                            {"Lid", request.LastId},
                            {"Number", request.Number},
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<DayPingResponseDto[]>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseDayPingResponse(responseText);
        }

        public virtual async Task<BaseResponseDto<BillingResponseDto>> Billing(
            BillingRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = Endpoints.BillingUrl ?? BillingUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Min", IranPostHelpers.FormatDate(request.Min)},
                            {"Max", IranPostHelpers.FormatDate(request.Max)},
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<BillingResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseBillingResponse(responseText);
        }

        public virtual async Task<BaseResponseDto<Billing2ResponseDto>> Billing2(
            Billing2RequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));


            IranPostHelpers.ValidateInvoiceNumber(request.Id);


            var path = Endpoints.Billing2Url ?? Billing2Url;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Id", request.Id},
                            {"Tip", (int) request.Tip},
                            {"Page", request.Page},
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<Billing2ResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseBilling2Response(responseText);
        }

        public virtual async Task<BaseResponseDto<RejectExpResponseDto>> RejectExp(
            RejectExpRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));


            IranPostHelpers.ValidateInvoiceNumber(request.Id);


            var path = Endpoints.RejectExpUrl ?? RejectExpUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                            {"Id", request.Id}
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<RejectExpResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseRejectExpResponse(responseText);
        }

        public virtual async Task<BaseResponseDto<RejectIdResponseDto>> RejectId(
            RejectIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var path = Endpoints.RejectIdUrl ?? RejectIdUrl;

            var retryContext = await RetryHandler.BeginTry(cancellationToken);

            HttpResponseMessage response = null;
            bool retry = false;

            path = CreatePath(path);

            do
            {
                try
                {
                    response = await Send(
                        path,
                        new Dictionary<string, object>
                        {
                        },
                        cancellationToken
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        return await ThrowOnInvalidStatusCode<RejectIdResponseDto>(response);
                    }

                    await RetryHandler.EndTry(retryContext, cancellationToken);
                }
                catch (Exception ex)
                {
                    retry = await RetryHandler.CatchException(retryContext, ex, cancellationToken);
                }
            } while (retry);

            if (response is null)
            {
                throw new InvalidOperationException();
            }

            var responseText = await response.Content.ReadAsStringAsync();

            return IranPostHelpers.ParseRejectIdResponse(responseText);
        }
    }
}