using EaaFramework.Config;
using Microsoft.Playwright;

namespace EaaFramework.Driver
{
    public interface IPlaywrightDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetEdgeDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings);
    }
}