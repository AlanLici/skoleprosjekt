using System;
using UiS.Dat240.Lab3.Core.Domain.Ordering;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;

public record OrderOverviewDto
(
    Guid Id,
    string CustomerName,
    string Building,
    string RoomNumber,
    DateTime OrderDate,
    Status Status
);