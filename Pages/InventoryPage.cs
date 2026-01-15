using Microsoft.Playwright;
using YourProject.Configuration;

namespace E2ETestAutomationFramework.Pages
{
    public class InventoryPage
    {
        public IPage Page { get; }
        public static string Url = $"{AppSettings.BaseUrl}/inventory.html";

        private ILocator InventoryTitle => Page.Locator("[data-test='title']");


        public InventoryPage(IPage page)
        {
            Page = page;
        }

        public static async Task<InventoryPage> Navigate(IPage page)
        {
            var instance = new InventoryPage(page);
            return instance;
        }

        private async Task NavigateToUrl(string url)
        {
            await Page.GotoAsync(url);
        }

        public async Task<string> GetInventoryTitle()
        {
            return await InventoryTitle.InnerTextAsync();
        }
    }
}
