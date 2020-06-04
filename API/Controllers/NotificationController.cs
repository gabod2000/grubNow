//using BusinessLayer;
//using CommonLayer;
//using EntityLayer.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace API.Controllers
//{
//    [AllowAnonymous]
//    public class NotificationController : BaseController
//    {
//        private BusinessBase<PushNotifications> _businessBase;
//        public NotificationController(BusinessWrapper businessWrapper) : base(businessWrapper)
//        {
//            _businessBase = new BusinessBase<PushNotifications>(businessWrapper._serviceProvider);
//        }
//        // GET: api/Notification
//        [HttpGet]
//        public BaseResponse Get()
//        {
//            return constructResponse(_businessWrapper.NotificationsBL.Get());
//        }

//        // GET: api/Notification/5
//        [HttpGet("{id}")]
//        public BaseResponse GetById(int id)
//        {
//            return constructResponse(_businessWrapper.NotificationsBL.GetById(id));
//        }

//        // POST: api/Notification
//        [HttpPost]
//        public async Task<BaseResponse> Post(string SenderId,string MesssageBody)
//        {
//            //Create the web request with fire base API  
//            //WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
//            //tRequest.Method = "post";
//            ////serverKey - Key from Firebase cloud messaging server  
//            //tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
//            ////Sender Id - From firebase project setting  
//            //tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
//            //tRequest.ContentType = "application/json";
//            //var payload = new
//            //{
//            //    to = deviceId,
//            //    priority = "high",
//            //    content_available = true,
//            //    notification = new
//            //    {
//            //        body = txtmsg,
//            //        title = txttitle.Replace(":", ""),
//            //        sound = "sound.caf",
//            //        badge = badgeCounter
//            //    },
//            //};
//            //var serializer = new JavaScriptSerializer();
//            //Byte[] byteArray = Encoding.UTF8.GetBytes(payload);
//            //tRequest.ContentLength = byteArray.Length;
//            //using (Stream dataStream = tRequest.GetRequestStream())
//            //{
//            //    dataStream.Write(byteArray, 0, byteArray.Length);
//            //    using (WebResponse tResponse = tRequest.GetResponse())
//            //    {
//            //        using (Stream dataStreamResponse = tResponse.GetResponseStream())
//            //        {
//            //            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
//            //                {
//            //                    String sResponseFromServer = tReader.ReadToEnd();
//            //                    result.Response = sResponseFromServer;
//            //                }
//            //        }
//            //    }
//            //}





//            PushNotifications model = new PushNotifications();

//            return constructResponse(await _businessBase.Post(model));
//        }

//        // PUT: api/Notification/5
//        [HttpPut("{id}")]
//        public async Task<BaseResponse> Put(int id, [FromBody] PushNotifications model)
//        {
//            return constructResponse(await _businessBase.Put(id, model));
//        }

//        // DELETE: api/ApiWithActions/5
//        [HttpDelete("{id}")]
//        public BaseResponse Delete(int id)
//        {
//            return constructResponse(_businessBase.Delete(id));
//        }
//    }
//}
