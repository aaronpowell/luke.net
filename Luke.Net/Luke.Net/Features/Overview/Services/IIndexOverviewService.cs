using System.Collections.Generic;

namespace Luke.Net.Features.Overview.Services
{
    public interface IIndexOverviewService
    {
        IEnumerable<FieldByTermInfo> GetFieldsAndTerms();
        IndexInfo GetIndexInfo();
    }
}