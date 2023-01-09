using EnginCan.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnginCan.Entity.Models.Users
{
    /// <summary>
    /// Kullanıcı Rollerinin tutulduğu tablodur.
    /// </summary>
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// Kullanıcı rolü tekil bilgisidir.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Yetkilendirilen kullanıcı tekil bilgisidir.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Rol tekil bilgisidir.
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Kullanıcı bilgilerini döndürür.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Rol bilgilerini döndürür.
        /// </summary>
        public Role Role { get; set; }
    }
}
