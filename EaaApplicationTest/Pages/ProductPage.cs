using EaaApplicationTest.Models;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaaApplicationTest.Pages
{
    public class ProductPage
    {
        private readonly IPage _page;
        public ProductPage(IPage page)
        {
            _page = page;
        }

        private ILocator _textName => _page.GetByLabel("Name");
        private ILocator _textDescription => _page.GetByLabel("Description");
        private ILocator _textPrice => _page.Locator("#Price");
        private ILocator _selectProduct => _page.GetByRole(AriaRole.Combobox, new PageGetByRoleOptions() { Name = "ProductType"});
        private ILocator _linkCreate => _page.GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = "Create" });
        
        public async Task CreateProduct(Product product)
        {
            await _textName.FillAsync(product.Name);
            await _textDescription.FillAsync(product.Description);
            await _textPrice.FillAsync(product.Price.ToString());
            await _selectProduct.SelectOptionAsync(product.ProductType.ToString());
        }

        public async Task ClickCreateProduct() => await _linkCreate.ClickAsync();
    }
}
