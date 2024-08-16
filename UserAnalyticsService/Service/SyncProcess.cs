using Amazon.Runtime;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using UserAnalyticsService.DTOs;
using UserAnalyticsService.Models;
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
        private readonly ISecuritySyncRunRepository _securitySyncRunRepository;
        private readonly IPriceSyncRunRepository _priceSyncRunRepository;


        public SyncProcess(ISecuritySyncRepository securitySyncRepository, ISecuritySyncRunRepository securitySyncRunRepository,
            IPriceSyncRepository priceSyncRepository, IPriceSyncRunRepository priceSyncRunRepository,
            IHttpClientFactory httpClientFactory, IOptions<TokenSettings> tokenSettings)
        {
            _securitySyncRepository = securitySyncRepository;
            _priceSyncRepository = priceSyncRepository;
            _httpClientFactory = httpClientFactory;
            _tokenSettings = tokenSettings.Value;
            _securitySyncRunRepository = securitySyncRunRepository;
            _priceSyncRunRepository = priceSyncRunRepository;


        }

        public async Task SyncPricesAsync()

        {
            SyncPriceResponseViewModel apiResponseDto = new SyncPriceResponseViewModel();

            SyncRequestViewModel syncRequestViewModel = new SyncRequestViewModel();

            try
            {
                HttpClient client = _httpClientFactory.CreateClient("AnalyticsAPI");
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                string url = SD.DataAcquisition + "/api/SyncProcess/GetPrices";


                syncRequestViewModel.LastUpdatedDate = DateTime.Today.AddYears(-10);
                syncRequestViewModel.Token = _tokenSettings.ApiToken;


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
                        apiResponseDto = new() { Success = false, Message = "Not Found" };
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        apiResponseDto = new() { Success = false, Message = "Access Denied" };
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        apiResponseDto = new() { Success = false, Message = "Unauthorized" };
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        apiResponseDto = new() { Success = false, Message = "Internal Server Error" };
                        break;
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        apiResponseDto = JsonConvert.DeserializeObject<SyncPriceResponseViewModel>(apiContent);
                        break;
                }

            }
            catch (Exception ex)
            {

            }
            PriceSyncRun priceSyncRun = new PriceSyncRun();

            if (apiResponseDto.Success == true)
            {
            //    priceSyncRun = await _priceSyncRepository.StoreSecuritiesAsync(apiResponseDto.Data);
                priceSyncRun.UpdateTillDate = apiResponseDto.LastUpdatedDate;
                priceSyncRun.IsSuccess = true;

            }
            else
            {
                priceSyncRun.IsSuccess = false;
                priceSyncRun.ErrorMessage = apiResponseDto.Message;
            }

            priceSyncRun.ProcessUpdateTillDate = syncRequestViewModel.LastUpdatedDate;
            priceSyncRun.CreatedOn = DateTime.Now;
            priceSyncRun.ModifiedOn = DateTime.Now;
            await _priceSyncRunRepository.Add(priceSyncRun);

        }

        public async Task SyncSecuritiesAsync(SecuritySyncRun securitySyncRun)
        {
            SyncSecurityResponseViewModel apiResponseDto = new SyncSecurityResponseViewModel();

            SyncRequestViewModel syncRequestViewModel = new SyncRequestViewModel();

            try
            {
                HttpClient client = _httpClientFactory.CreateClient("AnalyticsAPI");
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                string url = SD.DataAcquisition + "/api/SyncProcess/GetSecurities";


                syncRequestViewModel.LastUpdatedDate = DateTime.Today.AddYears(-10);
                syncRequestViewModel.Token = _tokenSettings.ApiToken;


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
                        apiResponseDto = new() { Success = false, Message = "Not Found" };
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        apiResponseDto = new() { Success = false, Message = "Access Denied" };
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        apiResponseDto = new() { Success = false, Message = "Unauthorized" };
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        apiResponseDto = new() { Success = false, Message = "Internal Server Error" };
                        break;
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        apiResponseDto = JsonConvert.DeserializeObject<SyncSecurityResponseViewModel>(apiContent);
                        break;
                }

            }
            catch (Exception ex)
            {

            }

            if(apiResponseDto.Success == true)
            {
                securitySyncRun = await _securitySyncRepository.StoreSecuritiesAsync(apiResponseDto.Data);
                securitySyncRun.UpdateTillDate = apiResponseDto.LastUpdatedDate;
                securitySyncRun.IsSuccess = true;

            }
            else
            {
                securitySyncRun.IsSuccess = false;
                securitySyncRun.ErrorMessage = apiResponseDto.Message;
            }
           
            securitySyncRun.ProcessUpdateTillDate = syncRequestViewModel.LastUpdatedDate;
            securitySyncRun.CreatedOn = DateTime.Now;
            securitySyncRun.ModifiedOn = DateTime.Now;
           await  _securitySyncRunRepository.Add(securitySyncRun);

        }
    }
}
