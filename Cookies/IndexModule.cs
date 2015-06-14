namespace Cookies
{
    using Nancy;
    using Nancy.Cookies;
    using System;
    using System.Collections.Concurrent;

    public class IndexModule : NancyModule
    {
        private const string COOKIE = "session";

        private static ConcurrentDictionary<string, ConsoleColor> _sessions = new ConcurrentDictionary<string, ConsoleColor>();

        public IndexModule()
            : base()
        {
            After.AddItemToEndOfPipeline(ctx =>
            {
                if (ctx.Request.Path == "/" && !ctx.Request.Cookies.ContainsKey(COOKIE))
                {
                    // sending simple session cookie
                    var sessionId = Guid.NewGuid().ToString();
                    ctx.Response.AddCookie(new NancyCookie(COOKIE, sessionId, false));
                    _sessions.TryAdd(sessionId, ColorSpinner.GetNext());
                }
            });

            Get["/"] = parameters =>
            {
                return View["index"];
            };

            Get["/iframe"] = premeters =>
            {
                return View["iframe"];
            };

            Post["/poll"] = parameters =>
            {
                if (Context.Request.Cookies.ContainsKey(COOKIE))
                {
                    ShowSessionCookie(Context.Request);
                    return new { status = "Ok" };
                }
                else
                {
                    return new { status = "Not Session Cookie" }; 
                }
            };
        }

        private static void ShowSessionCookie(Request req) {
            var session = Nancy.Helpers.HttpUtility.UrlDecode(req.Cookies[COOKIE]);
            var agent = string.Join("", req.Headers["User-Agent"]);

            var sessionKey = session;
            if (sessionKey.Contains("==>"))
            {
                sessionKey = sessionKey.Remove(session.IndexOf("==>"));
            }
            _sessions.TryAdd(sessionKey, ColorSpinner.GetNext());

            Console.ForegroundColor = _sessions[sessionKey];
            Console.WriteLine(agent);
            Console.WriteLine(session);
        }
    }
}