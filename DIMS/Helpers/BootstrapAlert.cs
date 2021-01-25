using System.Text;
using System.Web.Mvc;

namespace DIMS.Helpers
{
    public enum BootstrapAlertType
    {
        Success,
        Information,
        Warning,
        Danger
    }


    public class BootstrapAlert
    {

        // LINK TO FOLLWO www.codeproject.com/Articles/1091641/Bootstrap-Alert-from-ASP-NET-Server-Side

        public static MvcHtmlString BootstrapAlertHtmlString(string message,
            BootstrapAlertType messageType = BootstrapAlertType.Information,
            bool dismissable = false)
        {
            string style = null;
            string icon = null;
            switch (messageType)
            {
                case BootstrapAlertType.Success:
                    style = "success";
                    icon = "check";
                    break;
                case BootstrapAlertType.Information:
                    style = "info";
                    icon = "info-circle";
                    break;
                case BootstrapAlertType.Warning:
                    style = "warning";
                    icon = "warning";
                    break;
                case BootstrapAlertType.Danger:
                    style = "danger";
                    icon = "remove";
                    break;
            }


            var ulMsg = new TagBuilder("div");
            ulMsg.MergeAttribute("data-alert", "alert  alert-lg");
            ulMsg.MergeAttribute("class", "alert-message alert-" + style);
            var sb = new StringBuilder();
            sb.Append("<a class=\"close\" data-dismiss=\"alert\" href=\"#\">×</a>");
            sb.AppendFormat("<i class='fa fa-{0}'></i><span>  {1}</span>", icon, message);
            ulMsg.InnerHtml = sb.ToString();
            return MvcHtmlString.Create(ulMsg.ToString(TagRenderMode.Normal));

            //if (dismissable)
            //{
            //    str = "<div class='alert alert-" + style + " alert-dismissible fade in><i class='fa fa-" + icon + "'></i>" + message + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>";
            //}

            //return str;
        }

        public static MvcHtmlString BootstrapCustomAlertHtmlString(string message,
            BootstrapAlertType messageType = BootstrapAlertType.Information,
            bool dismissable = false)
        {
            string style = null;
            string title = null;
            switch (messageType)
            {
                case BootstrapAlertType.Success:
                    style = "success";
                    title = "success";
                    break;
                case BootstrapAlertType.Information:
                    style = "info";
                    title = "info";
                    break;
                case BootstrapAlertType.Warning:
                    style = "warning";
                    title = "warning";
                    break;
                case BootstrapAlertType.Danger:
                    style = "danger";
                    title = "error";
                    break;
            }


            var ulMsg = new TagBuilder("div");
            ulMsg.MergeAttribute("data-alert", "alert alert-lg");
            ulMsg.MergeAttribute("class", " alert fade in alert-dismissible show alert-" + style);
            var sb = new StringBuilder();
            sb.AppendFormat("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">\r\n<span class=\"la la-times\" aria-hidden=\"true\">x</span>\r\n                    </button>");
            sb.AppendFormat("<p>{0}</p>", message);
            ulMsg.InnerHtml = sb.ToString();
            return MvcHtmlString.Create(ulMsg.ToString(TagRenderMode.Normal));

            //if (dismissable)
            //{
            //    str = "<div class='alert alert-" + style + " alert-dismissible fade in><i class='fa fa-" + icon + "'></i>" + message + "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button></div>";
            //}

            //return str;
        }
    }
}