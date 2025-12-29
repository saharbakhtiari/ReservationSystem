using Domain.Common;
using Domain.UnitOfWork;
using Exceptions;
using Extensions;
using Infrastructure.Externals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class GenericApiCallerRepository<TEntity> : IGenericApiCallerRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly IApiContext _apiContext;

        public GenericApiCallerRepository(IApiContext context)
        {
            _apiContext = context;
        }
        public TEntity OwnerEntity { get; set; }
        public async Task<TResponse> CallService<TResponse>(string url)
        {
            string contentJson = JsonSerializer.Serialize(OwnerEntity, OwnerEntity.GetType());
            var content = new StringContent(contentJson, Encoding.UTF8, "application/json");
            _apiContext.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiContext.Token);

            try
            {
                var response = await _apiContext.Client.PostAsync(url, content);
                if (response.IsSuccessStatusCode.Not())
                {
                    string error = response.Content.ReadAsStringAsync().Result;
                    var errorJson = JsonSerializer.Deserialize<ProblemDetails>(error);
                    throw new ExternalServiceException((int)response.StatusCode, $"{errorJson.Detail}", null);
                }
                return response.Content.ReadFromJsonAsync<TResponse>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            }
            catch (ExternalServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExternalServiceException(ex.ToJson());
            }
        }

        protected void SetAccessToken(string accessToken)
        {
            _apiContext.Token = accessToken;
        }

        protected string GetAccessToken()
        {
            return _apiContext.Token;
        }
    }
}
