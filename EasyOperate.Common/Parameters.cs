using System.Collections.Generic;

namespace EasyOperate.Common
{
    public class Parameters
    {
        public const string CAPTURE_EMPTY_IMAGE = "/ParkStatic/imgs/sentry_camera_error.jpg";
        public const string DATE_FORMAT = "yyyy-MM-dd";
        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public const string COOKIE_LOGIN_USER_ID = "CookieParkLoginUserId";
        public const string CAPTURED_IMAGE_SAVE_DIR_NAME = "CapturedImages";

        public static string ServerRootDir = string.Empty;
        public static string ExceptionPlateNumber = "0000000";
        public const string DefaultCustomerID = "00000000-0000-0000-0000-000000000000";
        public const string DefaultCustomerName = "临停客户";

        public const int AUDIT_CHECKED_ACCEPT = 1;
        public const int AUDIT_CHECKED_FORBIDDEN = 0;

        public const decimal CARD_INSUFFICIENT_BALANCE_LINE = 0;
        /// <summary>
        /// 字典类型对像【KEY】【VALE】
        /// </summary>
        public static Dictionary<int, string> FinalPlateNumber = new Dictionary<int, string>();
    }
}
