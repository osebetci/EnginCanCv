using EnginCan.Entity.Shared;
using System.ComponentModel.DataAnnotations;

namespace EnginCan.Entity.Models.Systems
{
    /// <summary>
    /// Tüm tip verilerinin saklandığı tablodur.
    /// </summary>
    public class Lookup : BaseEntity
    {
        /// <summary>
        /// Kayıt tekil bilgisidir.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tip niteleyici bilgilerinin tutultuğu tip enum bilgisidir.
        /// </summary>
        public int? LookupTypeId { get; set; }

        /// <summary>
        /// Üst tip tekil bilgisidir.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Tip verisidir.
        /// </summary>
        [Required, MaxLength(500)]
        public string Name { get; set; }

        /// <summary>
        /// Tipe ait özel kod alanıdır.
        /// </summary>
        [MaxLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// Ek kullanılabilir bit alan degeridir.
        /// </summary>
        public bool? BooleanValue1 { get; set; }

        /// <summary>
        /// Ek kullanılabilir bit alan degeridir.
        /// </summary>
        public bool? BooleanValue2 { get; set; }

        /// <summary>
        /// Tip bilgilerini döndürür.
        /// </summary>
        public LookupType LookupType { get; set; }

        /// <summary>
        /// Üst tip veri bilgilerini döndürür
        /// </summary>
        public Lookup Parent { get; set; }
    }
}
