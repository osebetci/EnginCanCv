import { MessageService } from './message.service';
/**
 * Http Intercepter Service
 */
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { finalize, catchError, tap, timeout } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { HttpParameter, Api } from 'src/app/shared/core/dto/general.dto';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private messageService: MessageService
  ) {}
  timeout: number = 900000;

  get(env: Api, httpParameter: HttpParameter): Observable<any> {
    return this.http
      .get(
        `${env.endpoint}/${httpParameter.url}`,
        this.requestParams(env, httpParameter)
      )
      .pipe(
        timeout(this.timeout),
        catchError(this.onCatch.bind(env, this)),
        tap(
          (res) => {
            this.onSubscribeSuccess(res);
          },
          (error: any) => {
            this.onSubscribeError(error);
          }
        ),
        finalize(() => {
          this.onfinalize();
        })
      );
  }

  post(
    env: Api,
    httpParameter: HttpParameter,
    body: Object = {}
  ): Observable<any> {
    return this.http
      .post(
        `${env.endpoint}/${httpParameter.url}`,
        this.formatPayload(body),
        this.requestParams(env, httpParameter)
      )
      .pipe(
        timeout(this.timeout),
        tap(
          (res) => {
            this.onSubscribeSuccess(res);
          },
          (error: any) => {
            this.onSubscribeError(error);
          }
        ),
        finalize(() => {
          this.onfinalize();
        })
      );
  }

  postNoMessage(
    env: Api,
    httpParameter: HttpParameter,
    body: Object = {}
  ): Observable<any> {
    return this.http
      .post(
        `${env.endpoint}/${httpParameter.url}`,
        this.formatPayload(body),
        this.requestParams(env, httpParameter)
      )
      .pipe(
        timeout(this.timeout),
        tap((error: any) => {
          this.onSubscribeError(error);
        }),
        finalize(() => {
          this.onfinalize();
        })
      );
  }

  put(
    env: Api,
    httpParameter: HttpParameter,
    body: Object = {}
  ): Observable<any> {
    return this.http
      .put(
        `${env.endpoint}/${httpParameter.url}`,
        this.formatPayload(body),
        this.requestParams(env, httpParameter)
      )
      .pipe(
        timeout(this.timeout),
        tap(
          (res) => {
            this.onSubscribeSuccess(res);
          },
          (error: any) => {
            this.onSubscribeError(error);
          }
        ),
        finalize(() => {
          this.onfinalize();
        })
      );
  }

  patch(
    env: Api,
    httpParameter: HttpParameter,
    body: Object = {}
  ): Observable<any> {
    return this.http
      .patch(
        `${env.endpoint}/${httpParameter.url}`,
        this.formatPayload(body),
        this.requestParams(env, httpParameter)
      )
      .pipe(
        timeout(this.timeout),
        tap(
          (res) => {
            this.onSubscribeSuccess(res);
          },
          (error: any) => {
            this.onSubscribeError(error);
          }
        ),
        finalize(() => {
          this.onfinalize();
        })
      );
  }

  delete(env: Api, httpParameter: HttpParameter): Observable<any> {
    return this.http
      .delete(
        `${env.endpoint}/${httpParameter.url}`,
        this.requestParams(env, httpParameter)
      )
      .pipe(
        timeout(this.timeout),
        tap(
          (res) => {
            this.onSubscribeSuccess(res);
          },
          (error: any) => {
            this.onSubscribeError(error);
          }
        ),
        finalize(() => {
          this.onfinalize();
        })
      );
  }

  nativeGet(url: string, options?: any) {
    return this.http.get(url, this.nativeOptions(options)).pipe(
      timeout(this.timeout),
      tap(
        (res) => {
          this.onSubscribeSuccess(res);
        },
        (error: any) => {
          this.onSubscribeError(error);
        }
      ),
      finalize(() => {
        this.onfinalize();
      })
    );
  }

  nativeGetHub(url: string, options?: any) {
    return this.http.get(url, this.nativeOptions(options)).pipe(
      tap(
        (res) => {
          this.onSubscribeSuccess(res);
        },
        (error: any) => {
          this.onSubscribeError(error);
        }
      ),
      finalize(() => {
        this.onfinalize();
      }))
  }

  nativePost(url: string, body: any, options?: any) {
    return this.http.post(url, body, this.nativeOptions(options)).pipe(
      timeout(this.timeout),
      tap(
        (res) => {
          this.onSubscribeSuccess(res);
        },
        (error: any) => {
          this.onSubscribeError(error);
        }
      ),
      finalize(() => {
        this.onfinalize();
      })
    );
  }

  nativePut(url: string, body: any, options?: any) {
    return this.http.put(url, body, this.nativeOptions(options)).pipe(
      timeout(this.timeout),
      tap(
        (res) => {
          this.onSubscribeSuccess(res);
        },
        (error: any) => {
          this.onSubscribeError(error);
        }
      ),
      finalize(() => {
        this.onfinalize();
      })
    );
  }

  nativePatch(url: string, body: any, options?: any) {
    return this.http.patch(url, body, this.nativeOptions(options)).pipe(
      timeout(this.timeout),
      tap(
        (res) => {
          this.onSubscribeSuccess(res);
        },
        (error: any) => {
          this.onSubscribeError(error);
        }
      ),
      finalize(() => {
        this.onfinalize();
      })
    );
  }

  nativeDelete(url: string, options?: any) {
    return this.http.delete(url, this.nativeOptions(options)).pipe(
      timeout(this.timeout),
      tap(
        (res) => {
          this.onSubscribeSuccess(res);
        },
        (error: any) => {
          this.onSubscribeError(error);
        }
      ),
      finalize(() => {
        this.onfinalize();
      })
    );
  }

  nativeOptions(options?: any): Object {
    if (options == undefined || options == null || options != {}) {
      return {
        headers: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
        },
      };
    }

    return options;
  }

  getHttpHeaders(env: Api, httpParameter: HttpParameter) {
    environment.isRequesting = true;
    const token = localStorage.getItem(env.token);
    const tmpLocation = location.pathname;
    const headers = {
      'Content-Type': 'application/json',
      Accept: 'application/json',
      'api-version': httpParameter.version,
      Referrer: tmpLocation,
      Authorization: `Bearer ${token != undefined ? token : ''}`,
    };

    if (httpParameter.headers) {
      httpParameter.headers.keys().forEach((header) => {
        const headerValue = httpParameter.headers.get(header);
        if (headerValue) {
          headers[header] = headerValue;
        } else {
          delete headers[header];
        }
      });
    }
    return headers;
  }

  private requestParams(env: Api, httpParameter: HttpParameter): Object {
    return {
      headers: new HttpHeaders(this.getHttpHeaders(env, httpParameter)),
      params: httpParameter.params,
      responseType: httpParameter.reponseType
        ? httpParameter.reponseType
        : 'json',
    };
  }

  private formatPayload(payload: any) {
    if (payload instanceof FormData) {
      // FormData ise contenti formatlamıyoruz.
      return payload; // Contentin aktarılabilmesi için Content-Type gönderilmemelidir.
    }

    return JSON.stringify(payload, (key, value) => {
      if (!(this[key] instanceof Date)) {
        return value;
      }
      value = this.formatISO8601(this[key]);
      return value;
    });
  }

  formatISO8601(date: Date, skipZeroTime?: any, skipTimezone?: any) {
    let bag = [];
    let isZeroTime = function () {
      return (
        date.getHours() +
          date.getMinutes() +
          date.getSeconds() +
          date.getMilliseconds() <
        1
      );
    };
    let pad = function (text, length?, right?) {
      text = String(text);
      while (text.length < length) {
        text = right ? text + '0' : '0' + text;
      }
      return text;
    };
    let padLeft2 = function (text) {
      return pad(text, 2);
    };

    bag.push(date.getFullYear());
    bag.push('-');
    bag.push(padLeft2(date.getMonth() + 1));
    bag.push('-');
    bag.push(padLeft2(date.getDate()));
    if (!(skipZeroTime && isZeroTime())) {
      bag.push('T');
      bag.push(padLeft2(date.getHours()));
      bag.push(':');
      bag.push(padLeft2(date.getMinutes()));
      bag.push(':');
      bag.push(padLeft2(date.getSeconds()));
      if (date.getMilliseconds()) {
        bag.push('.');
        bag.push(pad(date.getMilliseconds(), 3));
      }
      if (!skipTimezone) {
        bag.push('Z');
      }
    }
    return bag.join('');
  }

  /**
   * Error handler.
   * @param error
   * @param caught
   * @returns {ErrorObservable}
   */
  private onCatch(
    env: Api,
    error: any,
    caught: Observable<any>
  ): Observable<any> {
    //this.messageService.onCatch(error);
    return throwError(error);
  }

  /**
   * onSubscribeSuccess
   * @param res
   */
  private onSubscribeSuccess(res: any): void {
    this.messageService.onSubscribeSuccess(res);
  }
  /**
   * onSubscribeError
   * @param error
   */
  private onSubscribeError(error: any): void {
    // this.loaderService.popError();
  }

  /**
   * onfinalize
   */
  private onfinalize(): void {}
}
