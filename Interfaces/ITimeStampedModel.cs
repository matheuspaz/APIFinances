using System;

namespace APIFinancas.Interfaces
{
    interface ITimeStampedModel
    {
        DateTime CreatedAt { get; set; }
        DateTime LastModified { get; set; }
    }
}