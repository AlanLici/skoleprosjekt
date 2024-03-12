using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Pipelines;
using MediatR;

namespace UiS.Dat240.Lab3.Pages.Order;

public class IndexModel : PageModel
{
     private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public OrderOverviewDto[] Orders { get; private set; } = Array.Empty<OrderOverviewDto>();
    public async Task OnGetAsync()
    {
        Orders = await _mediator.Send(new GetOverview.Request());
    }

}


