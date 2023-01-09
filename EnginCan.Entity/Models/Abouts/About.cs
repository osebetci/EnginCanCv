using EnginCan.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Entity.Models.Abouts
{
    /// <summary>
    /// Hakkımda bilgisinin tutulduğu tablodur
    /// </summary>
    public class About : BaseEntity
    {
        /// <summary>
        /// Tablo tekil bilgisidir
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// İsim Bilgisidir
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Doğum tarih bilgisidir.
        /// </summary>
        public string DogumTarih { get; set; }

        /// <summary>
        /// Mezuniyet Bilgisidir
        /// </summary>
        public string MezuniyetDurum { get; set; }

        /// <summary>
        ///  İş deneyim süresi
        /// </summary>
        public short DeneyimSuresi { get; set; }

        /// <summary>
        /// Email bilgisidir.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hakkımda özet yazı
        /// </summary>
        public string OzetMetin { get; set; }
    }
}
