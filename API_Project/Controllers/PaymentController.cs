using Core.DataTransferObjects;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        const string endpointSecret = "whsec_2ce207a926a8dab3274d3272f2b1e160970ef152019ddcf38ea3c3b9b723e811";

        public PaymentController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreatePaymentIntent(BasketDto input)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(input);
            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdatePaymentStatusFailed(paymentIntent.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdatePaymentStatusSucceeded(paymentIntent.Id);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }    
}
