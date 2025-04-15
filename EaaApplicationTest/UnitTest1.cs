using EaaApplicationTest.Models;
using EaaApplicationTest.Pages;
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

        [Theory]
        [InlineData("Speaker", "Gaming Speaker", 1700, "2")]
        [InlineData("USB", "USB 3.0", 578, "3")]
        [InlineData("Webcam", "Camera", 2500, "2")]
        public async Task TestWithInlineData(string name, string description, int price, string productType)
        {
            var page = await _playwrightDriver.Page;
            await page.GotoAsync("http://localhost:33084/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);
            
            await productListPage.CreateProductAsync();
            //await productPage.CreateProduct("Speaker", "Gaming Speaker", 144, "2");
            //await productPage.CreateProduct(name, description, price, productType);
            await productPage.ClickCreateProduct();
            await productListPage.ClickProductFromList(name);

            // Assertion
            // Assert.True( await productListPage.IsProductCreatedAsync("Speaker"));

            var element = productListPage.IsProductCreated("Speaker");
            await Assertions.Expect(element).ToBeVisibleAsync();
        }

        [Fact]
        public async Task TestWithConcreteTypes()
        {
            var page = await _playwrightDriver.Page;

            var product = new Product()
            {
                Name = "Test CPU",
                Description = "Test CPU details",
                Price = 4500,
                ProductType = ProductType.CPU
            };
            await page.GotoAsync("http://localhost:33084/");

            ProductListPage productListPage = new ProductListPage(page);
            ProductPage productPage = new ProductPage(page);

            await productListPage.CreateProductAsync();
            //await productPage.CreateProduct("Speaker", "Gaming Speaker", 144, "2");
            await productPage.CreateProduct(product);
            await productPage.ClickCreateProduct();
            await productListPage.ClickProductFromList(product.Name);

            // Assertion
            // Assert.True( await productListPage.IsProductCreatedAsync("Speaker"));

            var element = productListPage.IsProductCreated(product.Name);
            await Assertions.Expect(element).ToBeVisibleAsync();
        }
    }
}