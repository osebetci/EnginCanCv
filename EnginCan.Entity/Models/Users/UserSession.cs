using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginCan.Entity.Models.Users
{
    /// <summary>
    /// Usere ait giriş bilgilerinin saklandığı tablodur.
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// User Refresh token tekil bilgisidir.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// User tekil bilgisidir.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Giriş için oluşturulmuş refresh token tekil değeridir.
        /// </summary>
        [MaxLength(1500)]
        public string Token { get; set; }

        /// <summary>
        /// Giriş işleminin yapıldığı isteğin header değeridir.
        /// </summary>
        public string RequestHeader { get; set; }

        /// <summary>
        /// Giriş isteğinin yapıldığı ip adresidir.
        /// </summary>
        [MaxLength(30)]
        public string RemoteIpAddress { get; set; }

        /// <summary>
        /// Giriş zamanıdır.
        /// </summary>
        public DateTime LoginAt { get; set; }

        /// <summary>
        /// User değerlerini döndürür.
        /// </summary>
        public User User { get; set; }
    }
}
