using System.IO;
using Lucene.Net.Store;
using Magellan.Framework;
using Magellan.Routing;

namespace Luke.Net.Models.Binders
{
    public class ActiveIndexModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ResolvedNavigationRequest request, ModelBindingContext bindingContext)
        {
            var indexModel = (IndexModel)request.RouteValues["indexInfo"];

            var model = new ActiveIndexModel();
            model.Path = indexModel.IndexPath;
            model.ReadOnly = indexModel.ReadOnly;

            model.Directory = FSDirectory.Open(new DirectoryInfo(model.Path));

            return model;
        }

        #endregion
    }
}
