using System.Collections.Generic;
using System.Linq;

public class Link
{
    public Link(string controller, string action = "Index")
    {
        if (controller.ToLower().Contains(nameof(controller)) && controller.Substring(controller.Length - nameof(controller).Length).ToLower() == nameof(controller))
        {
            controller = controller.Substring(0, controller.Length - nameof(controller).Length);
        }
        this.Controller = controller;
        this.Action = action;
    }

    public string Controller { get; set; }
    public string Action { get; set; }
    public Dictionary<string, string> Params { get; } = new Dictionary<string, string>();

    public override string ToString()
    {
        var fullParams = new Dictionary<string, string>(Params);

        var argsParts = fullParams.Select(f => $"{f.Key}={f.Value}");
        var argsStr = string.Join("&", argsParts);

        argsStr = $"?{argsStr}";
        if (fullParams.Count == 0) argsStr = string.Empty;
        return $"/{Controller}/{Action}{argsStr}";
    }

    public static string ToString(string controller, string action = "Index")
    {
        var link = new Link(controller, action);
        return link.ToString();
    }
}