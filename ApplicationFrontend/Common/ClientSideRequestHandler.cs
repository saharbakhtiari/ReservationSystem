using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application_Frontend.Common
{
    public class ClientSideRequestHandler : IClientSideRequestHandler
    {
        private readonly HttpClient _http;

        public ClientSideRequestHandler(HttpClient http)
        {
            _http = http;
        }
        /// <summary>
        /// Default handler for Post requests to API. It hass success message and error message toast display functionality embedded
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TRequst"></typeparam>
        /// <param name="registerModel"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<TResponse> PostAsync<TResponse, TRequst>(TRequst registerModel, string url)
        {
            var response = await _http.PostAsJsonAsync(url, registerModel);

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseText))
                {
                    return default;
                }
                if (typeof(TResponse) == typeof(string))
                {
                    return (TResponse)Convert.ChangeType(responseText, typeof(TResponse)); ;
                }
                var createResult = JsonSerializer.Deserialize<TResponse>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return createResult;
            }
            else
            {
                await HandleError(response);
                return default;
            }

        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await _http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseText))
                {
                    return default;
                }
                if (typeof(TResponse) == typeof(string))
                {
                    return (TResponse)Convert.ChangeType(responseText, typeof(TResponse)); ;
                }
                var createResult = JsonSerializer.Deserialize<TResponse>(responseText, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return createResult;
            }
            else
            {
                await HandleError(response);
                return default;
            }
        }

        public async Task<HttpResponseMessage> GetPagingListAsync(string url)
        {
            var response = await _http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                await HandleError(response);
                return default;
            }
        }

        public async Task<HttpResponseMessage> GetPagingListAsync<TRequst>(TRequst queryModel, string url)
        {
            var response = await _http.PostAsJsonAsync(url, queryModel);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                await HandleError(response);
                return default;
            }

        }

        private async Task<ProblemDetails> HandleError(HttpResponseMessage responseMessage)
        {
            List<string> errors = new List<string>();
            var createResult = JsonSerializer.Deserialize<ProblemDetails>(await responseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (createResult.Errors != null && createResult.Errors.Count > 0)
            {

                foreach (var error in createResult.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        errors.Add(message);
                    }
                }
            }
            else
            {
                errors.Add(createResult.Detail);
            }
            throw new ClientSideException(createResult, errors);
        }
    }
}
