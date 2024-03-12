using System;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;

public record OrderLineDto
(
    Guid FoodItemId,
    string FoodItemName,
    int Amount,
    decimal Price
);


