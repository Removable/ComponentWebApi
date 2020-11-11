﻿using ComponentWebApi.Model.Base;

namespace ComponentWebApi.Model.Identity
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User: IEntity<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
    }
}