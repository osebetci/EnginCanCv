import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  onCatch = (error: any) => {
    const objErr = error.error;
    environment.isLoading = false;
    environment.isRequesting = false;
    if (objErr) {
      if (objErr.error) {
        switch (error.status) {
          case 400:
            this.notifyCreator(
              objErr,
              'error',
              'İstek hatalı. Sorunun devam etmesi durumunda sistem yöneticisi ile iletişime geçiniz.'
            );
            break;
          case 401:
            localStorage.removeItem(environment.api.token);
            window.location.href =
              window.location.origin + environment.loginPath;
            break;
          case 402:
            this.notifyCreator(objErr, 'error', 'Ödeme gerekli.');
            break;
          case 403:
            this.notifyCreator(
              objErr,
              'error',
              'Ulaşmaya çalıştığınız kaynak yasaklanmıştır.'
            );
            break;
          case 404:
            this.notifyCreator(objErr, 'error', 'İşlem kaynağı bulunamadı.');
            break;
          case 405:
            this.notifyCreator(
              objErr,
              'error',
              'İstek kabul edilemez lütfen sistem yöneticiniz ile iletişime geçiniz.'
            );
            break;
          case 406:
            this.notifyCreator(
              objErr,
              'error',
              `İstemcinin Accept header'ında verilen özellik karşılanamıyor.`
            );
            break;
          case 407:
            this.notifyCreator(
              objErr,
              'error',
              'Proxy üzerinden yetkilendirme gerekiyor.'
            );
            break;
          case 408:
            this.notifyCreator(
              objErr,
              'error',
              'İstek zaman aşımına uğradı (belirli bir sürede istek tamamlanamadı).'
            );
            break;
          case 409:
            this.notifyCreator(objErr, 'error', 'İstek içinde çelişki var.');
            break;
          case 410:
            this.notifyCreator(objErr, 'error', 'Kaynak artık yok.');
            break;
          case 411:
            this.notifyCreator(
              objErr,
              'error',
              'İstekte "Content-Length" (içeriğin boyutu) belirtilmemiş.'
            );
            break;
          case 412:
            this.notifyCreator(
              objErr,
              'error',
              'Sunucu istekte belirtilen bazı önkoşulları karşılamıyor.'
            );
            break;
          case 413:
            this.notifyCreator(
              objErr,
              'error',
              'İsteğin boyutu çok büyük olduğu için işlenemedi.'
            );
            break;
          case 414:
            this.notifyCreator(objErr, 'error', 'URI (URL) fazla büyük.');
            break;
          case 416:
            this.notifyCreator(
              objErr,
              'error',
              'İstenilen kaynak istenilen medya tipini desteklemiyor.'
            );
            break;
          case 417:
            this.notifyCreator(
              objErr,
              'error',
              'İstek yapılan parça (bir dosyanın bir parçası vb..) sunucu tarafından verilebiliyor veya uygun değil.'
            );
            break;
          case 500:
            this.notifyCreator(
              objErr,
              'error',
              'Bir hata ile karşılaşıldı. Lütfen sistem yöneticinize danışınız ya da daha sonra tekrar deneyiniz!'
            );
            break;
          default:
            break;
        }
      }
    }
  };

  errorHandler = (e: any) => {
    environment.isLoading = false;
    environment.isRequesting = false;
    switch (e.httpStatus) {
      case 400:
        this.notifyCreator(
          e,
          'error',
          'İstek hatalı. Sorunun devam etmesi durumunda sistem yöneticisi ile iletişime geçiniz.'
        );
        break;
      case 401:
        localStorage.removeItem(environment.api.token);
        window.location.href = window.location.origin + environment.loginPath;
        break;
      case 402:
        this.notifyCreator(e, 'error', 'Ödeme gerekli.');
        break;
      case 403:
        this.notifyCreator(
          e,
          'error',
          'Ulaşmaya çalıştığınız kaynak yasaklanmıştır.'
        );
        break;
      case 404:
        this.notifyCreator(e, 'error', 'İşlem kaynağı bulunamadı.');
        break;
      case 405:
        this.notifyCreator(
          e,
          'error',
          'İstek kabul edilemez lütfen sistem yöneticiniz ile iletişime geçiniz.'
        );
        break;
      case 406:
        this.notifyCreator(
          e,
          'error',
          `İstemcinin Accept header'ında verilen özellik karşılanamıyor.`
        );
        break;
      case 407:
        this.notifyCreator(
          e,
          'error',
          'Proxy üzerinden yetkilendirme gerekiyor.'
        );
        break;
      case 408:
        this.notifyCreator(
          e,
          'error',
          'İstek zaman aşımına uğradı (belirli bir sürede istek tamamlanamadı).'
        );
        break;
      case 409:
        this.notifyCreator(e, 'error', 'İstek içinde çelişki var.');
        break;
      case 410:
        this.notifyCreator(e, 'error', 'Kaynak artık yok.');
        break;
      case 411:
        this.notifyCreator(
          e,
          'error',
          'İstekte "Content-Length" (içeriğin boyutu) belirtilmemiş.'
        );
        break;
      case 412:
        this.notifyCreator(
          e,
          'error',
          'Sunucu istekte belirtilen bazı önkoşulları karşılamıyor.'
        );
        break;
      case 413:
        this.notifyCreator(
          e,
          'error',
          'İsteğin boyutu çok büyük olduğu için işlenemedi.'
        );
        break;
      case 414:
        this.notifyCreator(e, 'error', 'URI (URL) fazla büyük.');
        break;
      case 416:
        this.notifyCreator(
          e,
          'error',
          'İstenilen kaynak istenilen medya tipini desteklemiyor.'
        );
        break;
      case 417:
        this.notifyCreator(
          e,
          'error',
          'İstek yapılan parça (bir dosyanın bir parçası vb..) sunucu tarafından verilebiliyor veya uygun değil.'
        );
        break;
      case 500:
        this.notifyCreator(
          e,
          'error',
          'Bir hata ile karşılaşıldı. Lütfen sistem yöneticinize danışınız ya da daha sonra tekrar deneyiniz!'
        );
        break;
      default:
        break;
    }
  };

  onSubscribeSuccess = (res: any) => {
    try {
      environment.isLoading = false;
      environment.isRequesting = false;
      if (res && res.success) {
        this.notifyCreator(res, 'success', 'İşlem başarıyla gerçekleşti.');
      } else if (res && res.error) {
        this.notifyCreator(res, 'error', 'Opss! Bir hata ile karşılaşıldı!');
      }
    } catch {}
  };

  notifyCreator = (obj: any, type: string, message: string): void => {
    if (type === 'error') {
      this.toastr.error(errorObject(obj, message), 'Hata');
    } else {
      this.toastr.success(errorObject(obj, message), 'Başarılı');
    }
  };

  onInserted(response = null) {
    environment.isLoading = false;
    environment.isRequesting = false;
    this.notifyCreator(response, 'success', 'Kayıt başarıyla oluşturuldu.');
  }

  onRemoved(response = null) {
    environment.isLoading = false;
    environment.isRequesting = false;
    this.notifyCreator(response, 'success', 'Kayıt başarıyla silindi.');
  }

  onUpdated(response = null) {
    environment.isLoading = false;
    environment.isRequesting = false;
    this.notifyCreator(
      response,
      'success',
      'Kayıt başarıyla güncelleştirildi.'
    );
  }
}

const successObject = (succObj: any, customMessage: string): string => {
  if (!succObj) {
    return customMessage;
  }
  if (succObj.success) {
    if (succObj.success.message) {
      return succObj.success.message;
    } else {
      return customMessage;
    }
  } else {
    return customMessage;
  }
};

const errorObject = (errObj: any, customMessage: string): string => {
  if (!errObj) {
    return customMessage;
  }
  if (errObj.errorDetails) {
    if (errObj.errorDetails.message) {
      return errObj.errorDetails.message;
    } else {
      return customMessage;
    }
  } else if (errObj.error) {
    if (errObj.error.message) {
      return errObj.error.message;
    } else {
      return customMessage;
    }
  } else {
    return customMessage;
  }
};

const displayTime = (obj: any, type: string): number => {
  if (type === 'error') {
    if (!obj || !obj.errorDetails || !obj.errorDetails.time) {
      return 4000;
    }
    return obj.errorDetails.time;
  } else {
    if (!obj || !obj.error || !obj.error.time) {
      return 300;
    }
    return obj.error.time;
  }
};

