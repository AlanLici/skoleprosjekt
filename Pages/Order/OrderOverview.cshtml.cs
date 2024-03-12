using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Pipelines;
using UiS.Dat240.Lab3.Core.Exceptions;

namespace UiS.Dat240.Lab3.Pages.Orders;

public class DetailsModel : PageModel
{
    private readonly IMediator _mediator;
    public DetailsModel(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    
    public OrderDetailsDto OrderDetails { get; private set; } = null!;

    [BindProperty]
    public string NewShipperName { get; set; } = string.Empty;
    
    public async Task<IActionResult> OnGetAsync(Guid orderId)
    {
        try
        {
            OrderDetails = await _mediator.Send(new GetDetails.Request(orderId));
            Console.WriteLine(JsonSerializer.Serialize(OrderDetails));
            return Page ();
        }
        catch(EntityNotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync(Guid orderId)
    {
        try
        {
            await _mediator.Send(new AssignShipper.Request(orderId, NewShipperName));

            

            return RedirectToPage();
        }
        catch(EntityNotFoundException)
        {
            return NotFound();
        }
    }
}
