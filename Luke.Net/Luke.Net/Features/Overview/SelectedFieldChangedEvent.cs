using System.Collections.Generic;
using Luke.Net.Models;
using Microsoft.Practices.Prism.Events;

namespace Luke.Net.Features.Overview
{
    internal class SelectedFieldChangedEvent : CompositePresentationEvent<IEnumerable<FieldByTermInfo>>
    {
    }
}