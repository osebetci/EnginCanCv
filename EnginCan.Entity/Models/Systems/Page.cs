using EnginCan.Entity.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnginCan.Entity.Models.Systems
{
    /// <summary>
    /// Sistemdeki tüm sayfaların tutulduğu alandır.
    /// </summary>
    public class Page : IEntity
    {
        /// <summary>
        /// Kayıt tekil bilgisidir.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Üst sayfa tekil bilgisidir.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Sayfa sıralama bilgisidir.
        /// </summary>
        public short Order { get; set; }

        /// <summary>
        /// Sayfa adının tutulduğu alandır.
        /// </summary>
        [Required, MaxLength(75)]
        public string Name { get; set; }

        /// <summary>
        /// Sayfaya ait açıklama alanıdır.
        /// </summary>
        [MaxLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// Sayfanın tüm üst bilgileri ile bir arada tutulduğu alandır. (Breadcrump)
        /// </summary>
        [MaxLength(250)]
        public string AllName { get; set; }

        /// <summary>
        /// Sayfanın yönlendirici link bilgisidir.
        /// </summary>
        [MaxLength(100)]
        public string RouterLink { get; set; }

        /// <summary>
        /// Sayfanın üst sayfa bilgileri birleştirilerek oluşturulan linkler bütünüdür.
        /// </summary>
        [MaxLength(250)]
        public string AllRouterLink { get; set; }

        /// <summary>
        /// Sayfaya ait ikon bilgisinin tutulduğu alandır.
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// Anasayfa hızlı erişim ikon bilgisidir.
        /// </summary>
        [MaxLength(50)]
        public string WidgetIcon { get; set; }

        /// <summary>
        /// Anasayfa hızlı erişim ikon renk bilgisidir.
        /// </summary>
        [MaxLength(10)]
        public string Color { get; set; }

        /// <summary>
        /// Sayfanın anasayfadaki hızlı erişim alanlarında gösterilip gösterilemeyeceği bilgisinin tutulduğu alandır.
        /// </summary>
        public bool HomeWidget { get; set; }

        /// <summary>
        /// Sayfanın menüde gözüküp gözükemeyeceği bilgisidir.
        /// </summary>
        public bool MenuShow { get; set; }

        /// <summary>
        /// Üst sayfa bilgilerini döndürür.
        /// </summary>
        public Page Parent { get; set; }

        /// <summary>
        /// Tüm alt sayfa bilgilerini döndürür.
        /// </summary>
        public ICollection<Page> Childs { get; set; }

        /// <summary>
        /// Sayfa tüm yetkileri döndürür.
        /// </summary>
        public ICollection<PagePermission> PagePermission { get; set; }
    }
}
