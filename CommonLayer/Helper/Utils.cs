using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static CommonLayer.Enums;

namespace CommonLayer.Helper
{
    public class Utils
    {
        #region OtherConstants 
        public static class OtherConstants
        {
            public static string messageType = MessageType.Success;
            public static string responseMsg = "";
            public static bool isSuccessful;
        }
            #endregion





        #region Constants
        public static class Paths
        {
            public static string CBTFolderPath = "/Docs/Uploads/CBTs/";
        }
        #endregion
    }
}
