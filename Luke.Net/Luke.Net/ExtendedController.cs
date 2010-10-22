using Luke.Net.ActionResults;
using Magellan.Framework;

namespace Luke.Net
{
    public class ExtendedController : Controller
    {
        public ActionResult Object(object data)
        {
            return new ObjectActionResult { Data = data };
        }
    }
}
