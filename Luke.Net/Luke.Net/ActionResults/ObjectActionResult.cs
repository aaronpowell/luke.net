using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Magellan.Framework;

namespace Luke.Net.ActionResults
{
    public class ObjectActionResult : ActionResult
    {
        public object Data { get; set; }

        protected override void ExecuteInternal(ControllerContext controllerContext)
        {
            throw new NotImplementedException();
        }
    }
}
