using EaaFramework.Config;
using Microsoft.Playwright;
using System;


namespace EaaFramework.Driver
{
    public class PlaywrightDriver :IDisposable
    {
        private readonly AsyncTask<IBrowser> _browser;
        private readonly AsyncTask<IBrowserContext> _browserContext;
        private readonly TestSettings _testSettings;
        private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly AsyncTask<IPage> _page;
        private bool _isDisposed;

        public PlaywrightDriver (TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = testSettings;
            _playwrightDriverInitializer = playwrightDriverInitializer;
            _browser = new AsyncTask<IBrowser>(InitializePlaywrightDriver);
            _browserContext = new AsyncTask<IBrowserContext>(CreateBrowserContext);
            _page = new AsyncTask<IPage>(CreateNewPage);
        }
        public Task<IPage> Page => _page.Value;
        public Task<IBrowser> Browser => _browser.Value;
        public Task<IBrowserContext> BrowserContext => _browserContext.Value;
        private async Task<IBrowser> InitializePlaywrightDriver( )
        {
            switch (_testSettings.DriverType)
            {
                case DriverType.Chromium:
                    return await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings);
                case DriverType.Firefox:
                    return await _playwrightDriverInitializer.GetFirefoxDriverAsync(_testSettings);
                case DriverType.Edge:
                    return await _playwrightDriverInitializer.GetEdgeDriverAsync(_testSettings);
                default:
                    return await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings);
            }
            //return await GetBrowserAsync(_testSettings);
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await _browser).NewContextAsync();
        }

        private async Task<IPage> CreateNewPage()
        {
            return await (await _browserContext).NewPageAsync();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                if (_browser.IsValueCreated)
                    // TODO: dispose managed state (managed objects)
                    Task.Run(async () => 
                    { 
                        await (await Browser).CloseAsync();
                        await (await Browser).DisposeAsync();
                    });

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _isDisposed = true;
            }
        }
    }
}
