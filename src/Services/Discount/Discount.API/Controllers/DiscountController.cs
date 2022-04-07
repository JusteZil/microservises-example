using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpGet("{productName}", Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);

            return coupon;
        }

        [HttpPost(Name = "CreateDiscount")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await _discountRepository.CreateDiscount(coupon);

            return CreatedAtAction("GetDiscount", new { coupon.ProductName }, coupon);
        }

        [HttpPut(Name = "UpdateDiscount")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            await _discountRepository.UpdateDiscount(coupon);

            return NoContent();
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
        {
            await _discountRepository.DeleteDiscount(productName);

            return NoContent();
        }
    }
}
