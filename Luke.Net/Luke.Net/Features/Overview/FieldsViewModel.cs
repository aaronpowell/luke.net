using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;

namespace Luke.Net.Features.Overview
{
    class FieldsViewModel : NotificationObject
    {
        public IEnumerable<FieldInfo> Fields { get; set; }
    }
}
