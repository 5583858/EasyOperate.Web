using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;



namespace EasyOperate.Common
{
    public static class CommonFunctions
    {
        //public static SentryViewModels SVMHeartbeat { get; set; }
        public static bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,4,5,6,7,8,9]+\d{9}");
        }
       
        /// <summary>
        /// 根据权限名返回对应文字信息
        /// </summary>
        /// <param name="strRoleName">权限名</param>
        /// <returns>文字信息</returns>
        public static string GetStrByRoleName(string strRoleName)
        {
            string strResult = string.Empty;

            if (!string.IsNullOrEmpty(strRoleName))
            {
                switch (strRoleName)
                {
                    case "System":
                        strResult = "系统管理员";
                        break;
                    case "Admin":
                        strResult = "管理员";
                        break;
                    case "Sentry":
                        strResult = "岗亭操作员";
                        break;
                    default:
                        break;
                }
            }

            return strResult;
        }

        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = "0分钟";
            try
            {
                dateDiff = string.Empty;

                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                string days = ts.Days.ToString(), hours = ts.Hours.ToString(), minutes = ts.Minutes.ToString();
                if (ts.Days > 0)
                {
                    dateDiff += days + "天";
                }
                if (ts.Hours > 0)
                {
                    dateDiff += hours + "小时";
                }
                if (ts.Minutes > 0)
                {
                    dateDiff += minutes + "分钟";
                }
            }
            catch
            {

            }
            return dateDiff;
        }

        public static string Time2String(DateTime? time, bool onlyDate = false)
        {
            if (time != null && time != new DateTime(1970, 1, 1))
            {
                if (onlyDate)
                {
                    return time.Value.ToString(CommonParameters.DATE_FORMAT);
                }
                else
                {
                    return time.Value.ToString(CommonParameters.DATETIME_FORMAT);
                }
            }
            else
            {
                return "-";
            }
        }
        /// <summary>
        /// 获取类型(描述)  获取有关成员属性的信息并提供对成员元数据的访问
        /// </summary>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumName)
        {
            string _description = string.Empty;
            FieldInfo _fieldInfo = enumName.GetType().GetField(enumName.ToString());
            DescriptionAttribute[] _attributes = _fieldInfo.GetDescriptAttr();
            if (_attributes != null && _attributes.Length > 0)
                _description = _attributes[0].Description;
            else
                _description = enumName.ToString();
            return _description;
        }

        public static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            if (fieldInfo != null)
            {
                return (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            }
            return null;
        }

        public static ArrayList ToArrayList(this Type type)
        {
            if (type.IsEnum)
            {
                ArrayList _array = new ArrayList();
                Array _enumValues = Enum.GetValues(type);
                foreach (Enum value in _enumValues)
                {
                    _array.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
                }
                return _array;
            }
            return null;
        }

        public static void GetBetweenDate(DateTime dt1, DateTime dt2, out int y, out int m, out int d)
        {
            y = m = d = 0;

            if (dt1.Day > dt2.Day)
            {
                d = (dt2 - dt2.AddMonths(-1)).Days + (dt2.Day - dt1.Day);
                dt2 = dt2.AddMonths(-1);
            }
            else
            {
                d = dt2.Day - dt1.Day;
            }

            if (dt1.Month > dt2.Month)
            {
                m = 12 + dt2.Month - dt1.Month;
                dt2 = dt2.AddYears(-1);
            }
            else
            {
                m = dt2.Month - dt1.Month;
            }

            y = dt2.Year - dt1.Year;
        }

        public static void GetBetweenDate(DateTime dt1, DateTime dt2, out int m, out int d)
        {
            #region 
            dt2 = dt2.AddDays(1);
            m = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
            d = dt2.Day - dt1.Day;
            if (d < 0)
            {
                m -= 1;
                DateTime tmpTime = dt2.AddMonths(-1);
                d = (int)(dt2 - tmpTime).TotalDays + d;
            }
            #endregion
        }

        private static void GetBetweenMonthDay(DateTime dt1, DateTime dt2, out int m, out int d)
        {
            m = d = 0;

            bool flag = true;
            // 时间段相差天数
            var days = new TimeSpan(dt1.Ticks).Subtract(new TimeSpan(dt2.Ticks)).Duration().Days;
            do
            {
                d = days;

                // 获取开始时间当月天数
                var currMonthDays = DateTime.DaysInMonth(dt1.Year, dt1.Month);

                days -= currMonthDays;

                if (days > 0)
                {
                    m++;
                    dt1.AddMonths(1);
                }
                else
                {
                    flag = false;
                }

            } while (flag);
        }
        /// <summary>
        /// 获取时间戳13位字符串类型
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp13(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt13(time);
            return ts.ToString();
        }/// <summary>
         /// 获取时间戳10位字符串类型
         /// </summary>
         /// <returns></returns>
        public static string GetTimeStamp10(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt10(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt13(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
        public static long ConvertDateTimeToInt10(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000000;   //除10000调整为10位      
            return t;
        }
        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        private static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }


    }
}