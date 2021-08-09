using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.WebUI.Helpers
{
    public class JsonRedirect : JsonResult
    {
        public JsonRedirect(Link link) : this(link.ToString(), null) { }
        public JsonRedirect(string error) : this(null, error) { }
        public JsonRedirect(string link, string error) : base(Format(link, error))
        {
            this.Link = link;
            this.Error = error;
        }

        public string Error { get; }
        public string Link { get; }

        private static object Format(string link, string error)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.link = link;
            obj.error = error;
            return obj;
        }
    }
}