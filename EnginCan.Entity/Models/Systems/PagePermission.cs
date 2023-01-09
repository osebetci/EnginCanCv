using EnginCan.Entity.Models.Users;
using EnginCan.Entity.Shared;

namespace EnginCan.Entity.Models.Systems
{
    /// <summary>
    /// Sayfaların tüm listelenme ve erişim yetkilerinin tutulduğu tablodur.
    /// </summary>
    public class PagePermission : BaseEntity
    {
        /// <summary>
        /// Kayıt tekil bilgisidir.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sayfaya erişimi olan kişi tekil bilgisidir.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Sayfaya erişimi olan kullanıcı rol tekil bilgisidir.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Sayfa tekil bilgisidir.
        /// </summary>
        public int PageId { get; set; }

        /// <summary>
        /// Sayfaya erişimi yasaklı mı?
        /// </summary>
        public bool Forbidden { get; set; }

        /// <summary>
        /// User bilgilerini döndürür.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// User Rol bilgilerini döndürür.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Sayfa bilgilerini döndürür.
        /// </summary>
        public Page Page { get; set; }
    }
}
