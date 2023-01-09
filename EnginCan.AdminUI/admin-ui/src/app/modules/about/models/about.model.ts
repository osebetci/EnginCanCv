import { BaseEntity } from "src/app/shared/models/base-entity.model";

 /**Hakkımda bilgisinin tutulduğu tablodur */
    export class About extends BaseEntity {
        /**Tablo tekil bilgisidir */
        id: number;

        /**İsim Bilgisidir */
        fullName: string;

        /**Doğum tarih bilgisidir. */
        dogumTarih:Date| string;

        /**Mezuniyet Bilgisidir */
        mezuniyetDurum: string;

        /**İş deneyim süresi */
        deneyimSuresi: number;

        /**Email bilgisidir. */
        email: string;

        /**Hakkımda özet yazı */
        ozetMetin: string;
    }