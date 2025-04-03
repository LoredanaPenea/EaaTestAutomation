using EaaFramework.Config;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaaFramework.Driver
{
    public class PlaywrightDriverInitializer : IPlaywrightDriverInitializer
    {
        public const float DEFAULT_TIMEOUT = 70f;
        public async Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings)
        {
            var browserOptions = GetBrowserOptions(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
            browserOptions.Channel = "chrome";
            return await GetBrowserAsync(DriverType.Chromium, browserOptions);
        }
        public async Task<IBrowser> GetEdgeDriverAsync(TestSettings testSettings)
        {
            var browserOptions = GetBrowserOptions(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
            browserOptions.Channel = "msedge";
            return await GetBrowserAsync(DriverType.Chromium, browserOptions);
        }
        public async Task<IBrowser> GetFirefoxDriverAsync(TestSettings testSettings)
        {
            var browserOptions = GetBrowserOptions(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
            browserOptions.Channel = "firefox";
            return await GetBrowserAsync(DriverType.Firefox, browserOptions);
        }

        public async Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings)
        {
            var browserOptions = GetBrowserOptions(testSettings.Args, testSettings.Timeout, testSettings.Headless, testSettings.SlowMo);
            browserOptions.Channel = "chromium";
            return await GetBrowserAsync(DriverType.Chromium, browserOptions);
        }

        private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions browserOptions)
        {
            var playwrightDriver = await Playwright.CreateAsync();
            return await playwrightDriver[driverType.ToString().ToLower()].LaunchAsync(browserOptions);
        }

        private BrowserTypeLaunchOptions GetBrowserOptions(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
        {
            return new BrowserTypeLaunchOptions
            {
                Args = args,
                Timeout = ToMilliseconds(timeout),
                Headless = headless,
                SlowMo = slowmo
            };
        }

        private static float? ToMilliseconds(float? seconds)
        {
            return seconds * 1000;
        }

    }
}
