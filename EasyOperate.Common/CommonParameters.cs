namespace EasyOperate.Common
{
    /// <summary>
    /// 程序中用到的宏定义
    /// </summary>
    public static class CommonParameters
    {
        public const string CAPTURE_EMPTY_IMAGE = "/EasyOperateStatic/imgs/sentry_camera_error.jpg";
        public const string DATE_FORMAT = "yyyy-MM-dd";
        /// <summary>
        /// 时间格式 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public const string COOKIE_LOGIN_USER_ID = "CookieParkLoginUserId";
        public const string CAPTURED_IMAGE_SAVE_DIR_NAME = "CapturedImages";

        public static string ServerRootDir = string.Empty;
        public const string ServerImagePath = "UserPhotos";
        /// <summary>
        /// 手动开闸号码
        /// </summary>
        public static string ExceptionPlateNumber = "0000000";

        public const int AUDIT_CHECKED_ACCEPT = 1;
        public const int AUDIT_CHECKED_FORBIDDEN = 0;
        public const decimal CARD_INSUFFICIENT_BALANCE_LINE = 0;
    }

    public static class RoleType
    {
        public const string SYSTEM = "system";
        public const string ADMIN = "admin";
        public const string FINANCE = "finance";
        public const string SENTRY = "sentry";
        public const string CUSTOMER = "customer";
    }   
}