
/**Genel olarak kullanıcı taraflı değişikliklerin yapıldığı tablolarda basit değerlerin tutulduğu alanlardır. */
export class BaseEntity {
  /**Kaydı oluşturan kişi kullanıcı tekil bilgisidir. */
  createdUserId: number | null;

  /**Kaydı son güncelleyen kullanıcı tekil bilgisidir. */
  lastUpdatedUserId: number | null;

  /**Kaydın aktiflik, pasiflik ve silinme durumlarının tutulduğu alandır. */
  dataStatus: DataStatus|string;

  /**Kaydın oluşturulma zaman bilgisidir. */
  createdAt: Date | string;

  /**Kaydın güncellenme zaman bilgisidir. */
  lastUpdatedAt: Date | string | null;

}

/**Veri işlem durumlarının enum değeridir. */
export enum DataStatus {
  /**Verinin silinme durumudur. */
  Deleted = 1,

  /**Verinin aktiflik durumudur. */
  Activated,

  /**Verinin pasiflik durumudur. */
  DeActivated,
}
