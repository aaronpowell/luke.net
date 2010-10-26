using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Luke.Net.Features.Overview
{
    internal class SelectedFieldChangedEvent : CompositePresentationEvent<IEnumerable<FieldInfo>>
    {
    }
}