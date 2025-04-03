using EaaFramework.Config;
using EaaFramework.Driver;
using Microsoft.Playwright;


namespace PlaywrightDemo
{
    public class Tests
    {
       // private IPage _page;
        private PlaywrightDriver _driver;
        private PlaywrightDriverInitializer _playwrightDriverInitializer;

        public void Setup()
        {

            TestSettings testSettings = new TestSettings()
            {
                Headless = false,
                SlowMo = 1000,
                DriverType = DriverType.Chrome
            };

            _playwrightDriverInitializer = new PlaywrightDriverInitializer();
            _driver = new PlaywrightDriver(testSettings, _playwrightDriverInitializer);
            //await _driver.Page.GotoAsync("http://eaapp.somee.com");
        }       

        [Fact]
        public async Task TestClickLogin()
        {
            var page = await _driver.Page;
            await page.GotoAsync("http://eaapp.somee.com");
            await page.ClickAsync("text=Login");
        }

        [Fact]
        public async Task TestLogin()
        {
            var page = await _driver.Page;
            await page.GotoAsync("http://eaapp.somee.com");
            await page.ClickAsync("text=Login");
            await page.GetByLabel("UserName").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");

        }

        public async Task TearDown()
        {
            var browser = await _driver.Browser;
            await browser.CloseAsync();
            await browser.DisposeAsync();
        }
    }
}