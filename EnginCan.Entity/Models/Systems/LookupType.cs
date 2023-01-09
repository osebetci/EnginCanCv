using EnginCan.Entity.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnginCan.Entity.Models.Systems
{
    /// <summary>
    /// Tutulan tip tablolarının niteleyici tip tablosudur.
    /// </summary>
    public class LookupType : IEntity
    {
        /// <summary>
        /// Niteleyici tip tekil bilgisidir.
        /// </summary>
        public LookupTypes Id { get; set; }

        /// <summary>
        /// Niteleyici tipin tam adıdır.
        /// </summary>
        [Required, MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Niteleyici tip açıklama alanıdır.
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Niteleyici tipe bağlı olan tüm tip verilerini döndürür.
        /// </summary>
        public ICollection<Lookup> Lookup { get; set; }
    }

    /// <summary>
    /// Tip tiplerin tutulduğu enum değeridi.
    /// </summary>
    public enum LookupTypes
    {
        /// <summary>
        /// Cinsiyet durum tipidir.
        /// </summary>
        Cinsiyet = 1,

        /// <summary>
        /// Kan grubu durum tipidir.
        /// </summary>
        KanGrup,

        /// <summary>
        /// Din bilgisi durum tipidir.
        /// </summary>
        Din,

        /// <summary>
        /// Ehliyet durum tipidir.
        /// </summary>
        EhliyetTip,

        /// <summary>
        /// Uyruk durum tipidir.
        /// </summary>
        Uyruk,

        /// <summary>
        /// Meslek durum tipidir.
        /// </summary>
        Meslek,

        /// <summary>
        /// Eğitim durum  tipidir.
        /// </summary>
        EgitimDurum,

        /// <summary>
        /// Kişi cinsiyet bilgisi tipidir.
        /// </summary>
        Gender,

        /// <summary>
        /// Kişi adresi ülke bilgisi tipidir.
        /// </summary>
        Country,

        /// <summary>
        /// Kişi adresi il bilgisi tipidir.
        /// </summary>
        Province,

        /// <summary>
        /// Kişi adresi ilçe bilgisi tipidir.
        /// </summary>
        County,

        /// <summary>
        /// Para birim tipidir.
        /// </summary>
        Currency,
    }
}
