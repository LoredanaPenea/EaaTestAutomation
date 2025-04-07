using EaaFramework.Config;
using EaaFramework.Driver;
using Microsoft.Playwright;


namespace PlaywrightDemo
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
    }
}