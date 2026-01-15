using Allure.Net.Commons;
using Microsoft.Playwright;
using NLog;
using NUnit.Framework.Interfaces;

namespace E2ETestAutomationFramework.Tests
{
    public class BaseTest
    {
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        protected IBrowserContext Context;
        protected IPage Page;
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            TestContext.Out.WriteLine("Starting Playwright...");

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 50
            });
        }

        [SetUp]
        public async Task SetUp()
        {
            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                }
            });

            Page = await Context.NewPageAsync();
            TestContext.Out.WriteLine("New page created for test.");
        }

        [TearDown]
        public async Task TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    if (Page != null && !Page.IsClosed)
                    {
                        var screenshotBytes = await Page.ScreenshotAsync(new PageScreenshotOptions
                        {
                            FullPage = true
                        });

                        AllureApi.AddAttachment(
                            "Failure Screenshot",
                            "image/png",
                            screenshotBytes);
                    }
                    else
                    {
                        Console.WriteLine("Page is null or already closed; screenshot skipped.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
            }
            finally
            {
                // Only close per-test Context
                if (Context != null) await Context.CloseAsync();

            }
        }


        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            if (Browser != null) await Browser.CloseAsync();
            Playwright?.Dispose();
        }
    }
}
