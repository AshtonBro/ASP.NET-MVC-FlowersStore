public class Url
{
    public Url(string controllerName, string actionName, object @params = null)
    {
        ControllerName = controllerName;
        ActionName = actionName;
        Params = @params;
    }

    public string ControllerName { get; set; }

    public string ActionName { get; set; }

    public object Params { get; set; }

    public static Url OfIndex(string controllerName)
    {
        return new Url(controllerName, "Index", new { showLayout = false });
    }
}