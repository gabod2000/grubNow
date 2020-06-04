//using CommonLayer;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BusinessLayer
//{
//    public class NotificationsBL : BusinessBase<ExtendedUser>
//    {
//        private PlenumDbContext _context;
//        private BusinessBase<Notification> _BusinessBase;
//        private BaseResponse response;
//        public NotificationsBL(IServiceProvider serviceProvider) : base(serviceProvider)
//        {
//            _BusinessBase = new BusinessBase<Notification>(serviceProvider); 
//            response = new BaseResponse();
//            _context = serviceProvider.GetRequiredService<PlenumDbContext>();
//        }
      

       
//        public async Task<IEnumerable<Notification>> Get()
//        {
//            var notifications=_BusinessBase.Get().ToList();
//            List<Notification> notificationslst = null;
//            if (notifications != null)
//            {
//                notificationslst = new List<Notification>();
//                foreach (var item in notifications)
//                {
//                    Notification notification = new Notification();
//                    notification.GuidKey = item.GuidKey;
//                    notification.Id = item.Id;
//                    notification.IsRead = item.IsRead;
//                    notification.Message = item.Message;
//                    notification.Type = item.Type;
//                    notificationslst.Add(notification);
//                }
//            }
//            return notificationslst;
//        }


//        public async Task<BaseResponse> GetById(int Id)
//        {
//            var notifications = _BusinessBase.GetById(Id);
//            Notification notification = new Notification();
//            if (notifications != null)
//            {
//                    notification.GuidKey = notifications.GuidKey;
//                    notification.Id = notifications.Id;
//                    notification.IsRead = notifications.IsRead;
//                    notification.Message = notifications.Message;
//                    notification.Type = notifications.Type;
//            }
//            response.dynamicResult = notification;
//            return response;
//        }







//    }
//}
