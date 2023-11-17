using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace Challenge.Domain.Common
{
    public class Response<T>
    {
        public T Content { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public Dictionary<string, string> Headers { get; set; }

        public List<Notify> Notifications { get; }

        public bool IsValid => !Notifications.Any();

        public Response()
        {
            Notifications = new List<Notify>();
            Headers = new Dictionary<string, string>();
        }

        public void AddNotifications(IList<Notify> notifies)
        {
            Notifications.AddRange(notifies);
        }

        public void AddNotification(Notify notification)
        {
            Notifications.Add(notification);
        }

        public void AddNotification(string code, string property, string message)
        {
            Notifications.Add(new Notify
            {
                Code = code,
                Message = message,
                Property = property
            });
        }
    }
}
