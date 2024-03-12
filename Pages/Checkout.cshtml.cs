using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UiS.Dat240.Lab3.Core.Domain.Cart;
using UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;
using UiS.Dat240.Lab3.Helpers;

namespace UiS.Dat240.Lab3.Pages;
public class CheckoutModel : PageModel
{
    private readonly IMediator _mediator;

    public CheckoutModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
    }

    [BindProperty]
	public string? Building { get; set; } = string.Empty;

	[BindProperty]
	public string? RoomNumber { get; set; } = string.Empty;

	[BindProperty]
	public string? CustomerName { get; set; } = string.Empty;

	[BindProperty]
	public string? LocationNotes { get; set; } = string.Empty;

    public ShoppingCart? Cart { get; private set; }

    public List<string> Errors { get; set; } = new List<string>();

    public decimal TotalSum
    {
        get 
        {
            decimal sum = 0;
            foreach (var item in Cart.Items)
            {
                sum += item.Sum;
            }
            return sum;
        }
    }

    public async Task OnGetAsync()
    {
        var cartId = HttpContext.Session.GetGuid("CartId");
        if (cartId is null) return;

        Cart = await _mediator.Send(new Get.Request(cartId.Value));
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var cartId = HttpContext.Session.GetGuid("CartId");
        if (cartId is null) return RedirectToPage("/Index");

        Cart = await _mediator.Send(new Get.Request(cartId.Value));

        await _mediator.Send(new CartCheckout.Request(Cart.Id, Building, RoomNumber, LocationNotes, CustomerName));

        return RedirectToPage("/Index");
    }
}