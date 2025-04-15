using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaaApplicationTest.Pages
{
    public class ProductListPage
    {
        private readonly IPage _page;
        public ProductListPage(IPage page)
        {
            _page = page;
        }

        private ILocator _linkProductList => _page.GetByRole(AriaRole.Link, new() { Name = "Product" });
        private ILocator _linkCreateProduct => _page.GetByRole(AriaRole.Link, new() { Name = "Create" });

        public async Task CreateProductAsync()
        {
            await _linkProductList.ClickAsync();
            await _linkCreateProduct.ClickAsync();
        }

        public async Task ClickProductFromList(string name)
        {
            await _page.GetByRole(AriaRole.Row, new() { Name = name }).GetByRole(AriaRole.Link, new() { Name = "Details" }).ClickAsync();
            
        }
        public ILocator  IsProductCreated(string product)
        {
            return _page.GetByText(product, new() { Exact = true});

        }
    }
}
