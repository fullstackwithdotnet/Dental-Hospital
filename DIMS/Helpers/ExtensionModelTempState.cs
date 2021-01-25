using System.Web.Mvc;

namespace DIMS.Helpers
{
    public class StoreTempToModelState : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            filterContext.Controller.TempData["ModelState"] =
                filterContext.Controller.ViewData.ModelState;
        }
    }

    public class RestoreTempToModelState : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.Controller.TempData.ContainsKey("ModelState"))
            {
                filterContext.Controller.ViewData.ModelState.Merge(
                    (ModelStateDictionary)filterContext.Controller.TempData["ModelState"]);
            }
        }
    }
}