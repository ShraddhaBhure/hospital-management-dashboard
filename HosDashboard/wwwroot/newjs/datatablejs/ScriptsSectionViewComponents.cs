using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenterApplication.ViewComponents
{
    public class ScriptsSectionViewComponent : ViewComponent
    {


        public ScriptsSectionViewComponent()
        {

        }


        public IViewComponentResult Invoke()
        {


            return View();
        }

    }
}