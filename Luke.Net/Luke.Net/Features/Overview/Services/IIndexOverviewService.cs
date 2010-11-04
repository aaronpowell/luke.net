using System.Collections.Generic;
using Luke.Net.Models;

namespace Luke.Net.Features.Overview.Services
{
    public interface IIndexOverviewService
    {
        IEnumerable<FieldByTermInfo> GetFieldsAndTerms();
        IndexInfo GetIndexInfo();
    }
}