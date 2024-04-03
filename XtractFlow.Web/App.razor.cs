using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace XtractFlow.Web
{
    public partial class App
    {
        [Parameter]
        public string? ApiKey { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ProtectedLocalStorage ProtectedLocalStorageStore { get; set; }

        private const string STORAGE_KEY = ".AspNetCore.Id";

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        // protected override async Task OnAfterRenderAsync(bool firstRender)
        // {
        //     if (firstRender)
        //     {
        //         await InitialiseProtectedLocalStoreAsync();
        //         StateHasChanged();
        //     }
        // }

        private async Task InitialiseProtectedLocalStoreAsync()
        {
            if (!string.IsNullOrWhiteSpace(ApiKey))
            {
                await ProtectedLocalStorageStore.SetAsync(STORAGE_KEY, ApiKey);
            }
            else
            {
                string? apiKey = await GetProtectedLocalStorageStoreValueAsync(STORAGE_KEY);

                if (!IsValidApiKey(apiKey))
                {
                    await ProtectedLocalStorageStore.SetAsync(STORAGE_KEY, "");
                    NavigationManager.NavigateTo("Error");
                }
            }
        }

        private async Task<string?> GetProtectedLocalStorageStoreValueAsync(string key)
        {
            var result = await ProtectedLocalStorageStore.GetAsync<string>(key);
            if (result.Success)
            {
                return result.Value;
            }

            return null;
        }

        private static bool IsValidApiKey(string? apiKeyValue)
        {
            if (string.IsNullOrWhiteSpace(apiKeyValue))
            {
                return false;
            }

            return apiKeyValue == "WELCOME-TO-THE-FUTURE";// TODO: Get value from appConfig.
        }
    }
}