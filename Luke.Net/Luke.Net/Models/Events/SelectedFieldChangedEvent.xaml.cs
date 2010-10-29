using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Luke.Net.Models.Events
{
    internal class SelectedFieldChangedEvent : CompositePresentationEvent<IEnumerable<FieldInfo>>
    {
    }
}