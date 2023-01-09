using EnginCan.Entity.Shared;
using System.ComponentModel.DataAnnotations;

namespace EnginCan.Entity.Models.Users
{
    /// <summary>
    /// Kullanıcı Rollerinin tutulduğu tablodur.
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Kullanıcı rolü tekil bilgisidir.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rolü niteleyen isim bilgisidir.
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}
