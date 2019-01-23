using System;

namespace APIFinances.Interfaces
{
    interface ITimeStampedModel
    {
        DateTime CreatedAt { get; set; }
        DateTime LastModified { get; set; }
    }
}