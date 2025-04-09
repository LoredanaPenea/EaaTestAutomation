using EaaFramework.Config;
using EaaFramework.Driver;
using Microsoft.Playwright;


namespace EaaApplicationTest
{
    public class Tests: IClassFixture<PlaywrightDriverInitializer>
    {
        private readonly PlaywrightDriver _playwrightDriver;
        private readonly PlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly TestSettings _testSettings;

        public Tests(PlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = ConfigReader.ReadConfig();
            _playwrightDriverInitializer = playwrightDriverInitializer;
            _playwrightDriver = new PlaywrightDriver(_testSettings, _playwrightDriverInitializer);
            
        }

        [Fact]
        public async Task TestLogin()
        {
            var page = await _playwrightDriver.Page;
            //Console.WriteLine($"URL is: {_testSettings.ApplicationUrl}");
            await page.GotoAsync(_testSettings.ApplicationUrl);
            // await page.ClickAsync("text=Login");
            await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login"}).ClickAsync();
            await page.GetByLabel("UserName").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");

            await page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in"}).ClickAsync();

            await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Employee List" }).ClickAsync();
        }

        [Fact]
        public async Task Test2()
        {
            var page = await _playwrightDriver.Page;
            await page.GotoAsync("http://localhost:33084/");
            await page.GetByRole(AriaRole.Link, new() { Name = "Product" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Create" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).FillAsync("UPS");
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Description" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Description" }).FillAsync("Uninterrupted Power Supply backup");
            await page.Locator("#Price").ClickAsync();
            await page.Locator("#Price").FillAsync("1570");
            await page.GetByLabel("ProductType").SelectOptionAsync(new[] { "2" });
            await page.GetByRole(AriaRole.Button, new() { Name = "Create" }).ClickAsync();
            await page.GetByRole(AriaRole.Row, new() { Name = "68 UPS Uninterrupted Power" }).GetByRole(AriaRole.Link).Nth(1).ClickAsync();
            await page.GetByText("UPS").ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Back to List" }).ClickAsync();

        }
    }
}