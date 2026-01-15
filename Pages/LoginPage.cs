using Microsoft.Playwright;
using YourProject.Configuration;

namespace E2ETestAutomationFramework.Pages
{
    public class LoginPage
    {
        public IPage Page { get; }
        public static string Url = $"{AppSettings.BaseUrl}";

        #region Elements
        private ILocator UsernameInput => Page.Locator("[data-test='username']");
        private ILocator PasswordInput => Page.Locator("[data-test='password']");
        private ILocator LoginButton => Page.Locator("[data-test='login-button']");
        private ILocator LoginErrorMessage => Page.Locator("[data-test='error']");

        #endregion

        public LoginPage(IPage page)
        {
            Page = page;
        }

        public static async Task<LoginPage> Navigate(IPage page)
        {
            var instance = new LoginPage(page);
            await instance.NavigateToUrl(Url);
            return instance;
        }

        public async Task NavigateToUrl(string url)
        {
            await Page.GotoAsync(url);
        }

        public async Task Login(string username, string password)
        {
            await UsernameInput.FillAsync(username);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public async Task<string> GetLoginError()
        {
            return await LoginErrorMessage.InnerTextAsync();
        }
    }
}