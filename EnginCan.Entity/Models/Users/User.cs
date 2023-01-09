using EnginCan.Entity.Shared;
using System.ComponentModel.DataAnnotations;

namespace EnginCan.Entity.Models.Users
{
    /// <summary>
    /// Tüm kullanıcıların kaydının tutulduğu tablodur.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Kayıt tekil bilgisidir.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Kullanıcı adı.
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Kullanıcı soyadı.
        /// </summary>
        [Required, MaxLength(150)]
        public string Surname { get; set; }

        /// <summary>
        /// Kullanıcı adı soyadı
        /// </summary>
        [Required, MaxLength(250)]
        public string FullName { get; set; }

        /// <summary>
        /// İsteğe bağlı kullanıcı adı ile giriş yapılmak istenirse
        /// </summary>
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Kullanıcı fotoğrafıdır.
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Kullanıcı İletişim Numarasıdır.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Kullanıcı E-Posta adresi.
        /// </summary>
        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Kullanıcı parolası.
        /// </summary>
        [MaxLength(150)]
        public string Password { get; set; }
    }
}
