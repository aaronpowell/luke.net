﻿using Luke.Net.Models;
using Magellan.Framework;

namespace Luke.Net.Contollers
{
    public class PopupController : Controller
    {
        public ActionResult OpenIndex(string indexPath)
        {
            return Page(new IndexModel()
            {
                IndexPath = indexPath
            });
        }

        public ActionResult OpenIndex()
        {
            return OpenIndex(@"C:\_Projects\bitbucket.org\Private Works\BlogManager\BlogManager\bin\Debug\LuceneIndex");
        }
    }
}
