using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Luke.Net.Features.LuceneIndex
{
    internal class SelectedFieldChangedEvent : CompositePresentationEvent<IEnumerable<FieldInfo>>
    {
    }
}