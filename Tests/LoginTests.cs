using E2ETestAutomationFramework.Pages;
using Allure.NUnit.Attributes;
using NUnit.Allure.Core;

using YourProject.Configuration;

namespace E2ETestAutomationFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    [AllureFeature("Login")]

    public class Tests : BaseTest
    {
        [AllureTag("testType", "smoke")]
        [Test]
        public async Task LoginPageValidUsernameAndPassword_InventoryPageIsShown()
        {
            // Arrange
            var loginPage = await LoginPage.Navigate(Page);
            var inventoryPage = await InventoryPage.Navigate(Page);

            // Act
            await loginPage.Login(AppSettings.Username, AppSettings.Password);

            // Assert
            var titleText = await inventoryPage.GetInventoryTitle();
            Assert.That(titleText.ToString(), Is.EqualTo("Products"));
        }

        [Test]
        public async Task LoginPageInvalidUsername_InvalidUsernameOrPasswordErrorThrown()
        {
            // Arrange
            var loginPage = await LoginPage.Navigate(Page);

            // Act
            await loginPage.Login("fake username", AppSettings.Password);

            // Assert
            var titleText = await loginPage.GetLoginError();

            Assert.That(titleText.ToString(), Is.EqualTo("Epic sadface: Username and password do not match any user in this service"));
        }

    }
}
