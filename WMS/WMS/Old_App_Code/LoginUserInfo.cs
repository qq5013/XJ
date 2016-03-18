using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WMS.App_Code
{
    public class LoginUserInfo
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public LoginUserInfo()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //       
        }
        private static string m_UserCode;

        /// <summary>
        /// 用户编码
        /// </summary>
        public static string UserCode
        {
            get { return m_UserCode; }
            set { m_UserCode = value; }
        }
        private static string m_UserName;

        /// <summary>
        /// 用户名称
        /// </summary>
        public static string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
        private static string m_UserID;
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UserID
        {
            get { return LoginUserInfo.m_UserID; }
            set { LoginUserInfo.m_UserID = value; }
        }
    }
}