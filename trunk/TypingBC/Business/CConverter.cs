﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Business
{
    /// <summary>
    /// Lớp này có nhiệm vụ chuyển mã của 1 chuỗi Unicode về các kiểu gõ tương ứng.
    /// Lớp này là static nên khi gọi hàm chỉ cần CConverter.String2VNI(...)...
    /// </summary>
    public static class CConverter
    {
        #region CodeTable
        private static readonly char[] UnicodeTable = 
        {
            'đ','á','à','ả','ã','ạ',
            'â','ấ','ầ','ẩ','ẫ','ậ',
            'ă','ắ','ằ','ẳ','ẵ','ặ',
            'ê','ế','ề','ể','ễ','ệ',
            'ơ','ớ','ờ','ở','ỡ','ợ',
            'ư','ứ','ừ','ử','ữ','ự'
        };

        private static readonly string[] VNITable = 
        {
            "d9","a1","a2","a3","a4","a5",
            "a6","a61","a62","a63","a64","a65",
            "a8","a81","a82","a83","a84","a85",
            "e6","e61","e62","e63","e64","e65",
            "o7","o71","o72","o73","o74","o75",
            "u7","u71","u72","u73","u74","u75"
        };


        #endregion

        public static string Str2VNI(string sString)
        {

            return string.Empty;
        }

        public static string Str2Telex(string sString)
        {
            return string.Empty;
        }

        public static string Str2NoMark(string sString)
        {
            return string.Empty;
        }
    }
}
