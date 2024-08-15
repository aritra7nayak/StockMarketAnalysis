using Amazon.Runtime;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Repository.IRepository;
using UserAnalyticsService.Service.IService;
using UserAnalyticsService.Utilities;

namespace UserAnalyticsService.Service
{
    public class SyncProcess:ISyncProcess
    {
        private readonly ISecuritySyncRepository _securitySyncRepository;
        private readonly IPriceSyncRepository _priceSyncRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenSettings _tokenSettings;


        public SyncProcess(ISecuritySyncRepository securitySyncRepository, IPriceSyncRepository priceSyncRepository,IHttpClientFactory httpClientFactory, IOptions<TokenSettings> tokenSettings)
        {
            _securitySyncRepository = securitySyncRepository;
            _priceSyncRepository = priceSyncRepository;
            _httpClientFactory = httpClientFactory;
            _tokenSettings = tokenSettings.Value;

        }

        public Task SyncPricesAsync(List<PriceData> securities)
        {
            throw new NotImplementedException();
        }

        public async Task<SyncSecurityResponseViewModel?> SyncSecuritiesAsync()
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("AnalyticsAPI");
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                string url = SD.DataAcquisition + "/api/SyncProcess/GetSecurities";

                SyncRequestViewModel syncRequestViewModel = new SyncRequestViewModel()
                {
                    LastUpdatedDate = DateTime.Today.AddYears(-10),
                    Token = _tokenSettings.ApiToken

                };

                message.RequestUri = new Uri(url);
                if (syncRequestViewModel != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(syncRequestViewModel), System.Text.Encoding.UTF8, "application/json");
                }
                message.Method = HttpMethod.Post;
                HttpResponseMessage? apiResponse = null;

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { Success = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { Success = false, Message = "Access Denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { Success = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { Success = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<SyncSecurityResponseViewModel>(apiContent);
                        return apiResponseDto;
                }

            }
            catch (Exception ex)
            {

            }

        }
    }
}
