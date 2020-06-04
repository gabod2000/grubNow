﻿using System;
using System.Collections.Generic;
using System.Text;
using static CommonLayer.Enums;

namespace CommonLayer
{
    public class BaseResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public bool isSuccessfull { get; set; }
        public string errorMessage { get; set; }
        public string messageType { get; set; }
        public dynamic dynamicResult { get; set; }

        public BaseResponse()
        {
            statusCode = 200;
            message = string.Empty;
            isSuccessfull = true;
            messageType = MessageType.Success;
            errorMessage = string.Empty;
        }
    }

}
