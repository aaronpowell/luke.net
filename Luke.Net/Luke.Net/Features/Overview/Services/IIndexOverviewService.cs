using System.Collections.Generic;

namespace Luke.Net.Features.Overview.Services
{
    public interface IIndexOverviewService
    {
        IEnumerable<FieldInfo> GetFields();
        IEnumerable<TermInfo> GetTerms();
        IndexInfo GetIndexInfo();
    }
}