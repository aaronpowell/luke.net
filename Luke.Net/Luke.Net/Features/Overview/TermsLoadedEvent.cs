using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Luke.Net.Features.Overview
{
    class TermsLoadedEvent : CompositePresentationEvent<IEnumerable<TermInfo>>
    {
    }
}
