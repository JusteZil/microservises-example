using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderingService _orderingService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderingService orderingService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderingService = orderingService ?? throw new ArgumentNullException(nameof(orderingService));
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket = await _basketService.GetBasket(userName);

            foreach(var basketItem in basket.Items)
            {
                var product = await _catalogService.GetCatalog(basketItem.ProductId);

                // set additional product fields onto basket item
                basketItem.ProductName = product.Name;
                basketItem.Category = product.Category;
                basketItem.Summary = product.Summary;
                basketItem.Description = product.Description;
                basketItem.ImageFile = product.ImageFile;
            }

            var orders = await _orderingService.GetOrdersByUserName(userName);

            return new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };
        }
    }
}
