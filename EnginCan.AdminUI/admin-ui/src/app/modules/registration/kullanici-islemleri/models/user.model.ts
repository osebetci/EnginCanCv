import { BaseEntity } from "src/app/shared/models/base-entity.model";

/**Tüm kullanıcıların kaydının tutulduğu tablodur. */
export class User extends BaseEntity {
  /**Kayıt tekil bilgisidir. */
  id: number;

  /**Kullanıcı adı. */
  name: string;

  /**Kullanıcı soyadı. */
  surname: string;

  /**Kullanıcı adı soyadı */
  fullName: string;

  /**İsteğe bağlı kullanıcı adı ile giriş yapılmak istenirse */
  username: string;

  /**Kullanıcı fotoğrafıdır. */
  photo: string;

  /**Kullanıcı İletişim Numarasıdır. */
  phoneNumber: string;

  /**Kullanıcı E-Posta adresi. */
  email: string;

  /**Kullanıcı parolası. */
  password: string;
}
