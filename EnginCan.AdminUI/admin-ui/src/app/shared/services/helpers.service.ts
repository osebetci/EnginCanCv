import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root',
})
export class HelpersService {
  checkTcControl(e: any): boolean {
    if (!e.value) {
      return true;
    }

    const value = e.value.toString();
    const isEleven = /^[0-9]{11}$/.test(value);
    if (!isEleven) {
      return false;
    }
    let totalX = 0;
    for (let j = 0; j < 10; j++) {
      totalX += Number(value.substr(j, 1));
    }
    const isRuleX = totalX % 10 == value.substr(10, 1);
    let totalY1 = 0;
    let totalY2 = 0;
    for (let j = 0; j < 10; j += 2) {
      totalY1 += Number(value.substr(j, 1));
    }
    for (let j = 1; j < 10; j += 2) {
      totalY2 += Number(value.substr(j, 1));
    }
    const isRuleY = (totalY1 * 7 - totalY2) % 10 == value.substr(9, 0);
    return isRuleX && isRuleY;
  }

  generateObjecId(): string {
    const timestamp = (new Date().getTime() / 1000).toString(16).split('.')[0];
    return (
      timestamp +
      'xxxxxxxxxxxxxxxx'
        .replace(/[x]/g, function () {
          return (Math.random() * 16).toString(16).split('.')[0];
        })
        .toLowerCase()
    );
  }

  base64ImageCompressor(
    img: string,
    quality?: number,
    type?: string
  ): Promise<string> {
    return new Promise((resolve) => {
      const image = new Image();
      image.src = img;
      image.onload = () => {
        const canvas = document.createElement('canvas'),
          ctx = canvas.getContext('2d');

        if (image.width > 1920) {
          const parameter = 1920 / image.width;
          canvas.width = Math.round(parameter * image.width);
          canvas.height = Math.round(parameter * image.height);
        } else {
          canvas.width = image.width;
          canvas.height = image.height;
        }

        if (!type || 'jpg,jpeg'.indexOf(type) > -1) {
          ctx.fillStyle = 'white';
          ctx.fillRect(0, 0, canvas.width, canvas.height);
        }

        ctx.drawImage(image, 0, 0, canvas.width, canvas.height);
        fetch(
          canvas.toDataURL(
            `image/${type ? type : 'jpeg'}`,
            quality ? quality : 0.5
          )
        ).then((res) => {
          resolve(res.url);
        });
      };
    });
  }

  // SVG Formatında olduğu için tüm html elementlerini yazdırmıyor. Örnk: hr
  htmlToPng(
    html: HTMLElement,
    style?: string,
    quality?: number
  ): Promise<string> {
    return new Promise((resolve) => {
      const image = new Image();
      image.src =
        'data:image/svg+xml,' +
        encodeURIComponent(
          `<svg xmlns="http://www.w3.org/2000/svg" width="${html.offsetWidth}" height="${html.offsetHeight}"><foreignObject width="100%" height="100%"><div xmlns="http://www.w3.org/1999/xhtml"><style>${style}</style>${html.innerHTML}</div></foreignObject></svg>`
        );
      image.onload = () => {
        const canvas = document.createElement('canvas'),
          ctx = canvas.getContext('2d');

        canvas.width = image.width;
        canvas.height = image.height;

        ctx.drawImage(image, 0, 0, html.offsetWidth, html.offsetHeight);
        fetch(canvas.toDataURL(`image/png`, quality ? quality : 0.75)).then(
          (res) => {
            resolve(res.url);
          }
        );
      };
    });
  }

  downloadBase64File(b64, fileName) {
    let image_data;
    if (b64.split(',')[1]) {
      image_data = atob(b64.split(',')[1]);
    } else {
      image_data = atob(b64);
    }
    const arraybuffer = new ArrayBuffer(image_data.length);
    const view = new Uint8Array(arraybuffer);
    let blob;
    for (let i = 0; i < image_data.length; i++) {
      // tslint:disable-next-line:no-bitwise
      view[i] = image_data.charCodeAt(i) & 0xff;
    }
    try {
      blob = new Blob([arraybuffer], { type: 'application/octet-stream' });
    } catch (e) {
      const bb = new (window['WebKitBlobBuilder'] ||
        window['MozBlobBuilder'])();
      bb.append(arraybuffer);
      blob = bb.getBlob('application/octet-stream');
    }
    const a = document.createElement('a');
    document.body.appendChild(a);
    a.style.display = 'none';
    const url = (window['webkitURL'] || window.URL).createObjectURL(blob);
    a.href = url;
    a.download = fileName;
    a.click();
    window.URL.revokeObjectURL(url);
  }

  convertDataURIToBinary(base64: string) {
    const raw = window.atob(base64.split(',')[1]);
    const rawLength = raw.length;
    const array = new Uint8Array(new ArrayBuffer(rawLength));

    for (let i = 0; i < rawLength; i++) {
      array[i] = raw.charCodeAt(i);
    }
    return array;
  }

  convertDataURIToBlobUri(base64: string, type: string) {
    const file = new Blob([this.convertDataURIToBinary(base64)], {
      type: type,
    });
    return URL.createObjectURL(file);
  }

  notify(
    title: string,
    body?: string,
    action?: string,
    icon?: string,
    image?: string
  ) {
    if (typeof Notification !== 'undefined') {
      if (Notification.permission !== 'granted') {
        Notification.requestPermission();
      } else {
        const notification = new Notification(title, {
          icon: icon ? icon : '/assets/img/logo.png',
          body: body,
          image: image,
        });

        if (action) {
          notification.onclick = function () {
            window.open(action);
          };
        }
      }
    }
  }

  calculateAge(birthDate: Date | string): number {
    try {
      return Math.floor(
        (new Date().getTime() - new Date(birthDate).getTime()) /
          1000 /
          60 /
          60 /
          24 /
          365
      );
    } catch {
      return 0;
    }
  }

  maskText(text: string): string {
    const shouldMaskCharacterCount = Math.round(text.length * 0.6);
    let index = 1;
    if (text.length > 1) {
      this.replaceAt(text, 1, '*');
      index = 2;
    }
    for (index; index < shouldMaskCharacterCount + 1; index++) {
      let randomIndex = this.randomIntl(1, text.length - 1);
      while (text.charAt(randomIndex) == '*') {
        randomIndex = this.randomIntl(1, text.length - 1);
      }
      text = this.replaceAt(text, randomIndex, '*');
    }
    return text;
  }

  randomIntl(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }

  // replace the 'index'th character of 'string' with 'newChar'
  replaceAt(string, index, newChar): string {
    return string.substring(0, index) + newChar + string.substring(index + 1);
  }

  filterExpr(value: any, destination?: Array<string>) {
    const expr: Array<string> = value
      .toLocaleLowerCase()
      .split(' ')
      .filter((x) => x != '');

    let query = [];

    for (let i = 0; i < expr.length; i++) {
      for (let j = 0; j < destination.length; j++) {
        query.push(
          [`contains(tolower(${destination[j]}), '${expr[i]}')`],
          'or'
        );
      }
    }

    if (query[query.length - 1] == 'or') {
      query.pop();
    }

    return query;
  }

  adSoyadFilterExpr(value: any, path: string) {
    const expr: Array<string> = value
      .toLocaleLowerCase()
      .split(' ')
      .filter((x) => x != '');

    let query = [];
    let soyad = expr[expr.length - 1];
    let birlesikIsim = '';
    for (let i = 0; i < expr.length; i++) {
      const e = expr[i];
      birlesikIsim += e + ' ';
    }
    birlesikIsim = birlesikIsim.slice(0, -1).toLocaleUpperCase();

    return (query = [
      [
        `contains(concat(concat(toupper(${path}.ad), ' '), toupper(${path}.soyad)), '${birlesikIsim}')`,
      ],
    ]);
  }

  nameSurnameFilterExpr(value: any, path: string) {
    const expr: Array<string> = value
      .toLocaleLowerCase()
      .split(' ')
      .filter((x) => x != '');

    let query = [];
    let soyad = expr[expr.length - 1];
    let birlesikIsim = '';
    for (let i = 0; i < expr.length; i++) {
      const e = expr[i];
      birlesikIsim += e + ' ';
    }
    birlesikIsim = birlesikIsim.slice(0, -1).toLocaleUpperCase();

    return (query = [
      [
        `contains(concat(concat(toupper(${path}.name), ' '), toupper(${path}.surname)), '${birlesikIsim}')`,
      ],
    ]);
  }

  calcAdSoyadVal(item: any, field: string) {
    if (!item) {
      return '';
    }

    if (item[field]) {
      if (item[field].ad) {
        return `${item[field].ad} ${item[field].soyad}`;
      }
      if (item[field].unvan) {
        return `${item[field].unvan}`;
      }

      return '';
    }

    return '';
  }

  round(digits: number, value: number) {
    if (digits < 0) {
      return 0;
    }

    if (!value) {
      return 0;
    }

    let factor = 1;
    for (let i = 0; i < digits; i++) {
      factor = factor * 10;
    }

    return Math.round(value * factor) / factor;
  }

  sumAndRound(digit: number, ...values: number[]) {
    if (digit < 0) {
      return 0;
    }

    let factor = 1;
    for (let i = 0; i < digit; i++) {
      factor = factor * 10;
    }

    // let filteredValues = values.filter((x) => Boolean(x) == true);

    if (values.length <= 0) {
      return 0;
    }

    let sum = 0;
    for (let j = 0; j < values.length; j++) {
      if (!values[j]) {
        continue;
      }
      sum = Math.round((sum + values[j]) * factor) / factor;
    }

    return sum;
  }

  multiplyAndRound(digit: number, ...values: number[]) {
    if (digit < 0) {
      return 0;
    }

    let factor = 1;
    for (let i = 0; i < digit; i++) {
      factor = factor * 10;
    }

    // let filteredValues = values.filter((x) => Boolean(x) == true);

    if (values.length <= 0) {
      return 0;
    }

    let sum = 1;
    for (let j = 0; j < values.length; j++) {
      if (!values[j]) {
        return 0;
      }
      sum = Math.round(sum * values[j] * factor) / factor;
    }

    return sum;
  }

  deepCopy = (thing: any, opts: any) => {
    let newObject = {};
    if (thing instanceof Array) {
      return thing.map(function (i) {
        return this.deepCopy(i, opts);
      });
    } else if (thing instanceof Date) {
      return new Date(thing);
    } else if (thing instanceof RegExp) {
      return new RegExp(thing);
    } else if (thing instanceof Function) {
      return opts && opts.newFns
        ? new Function('return ' + thing.toString())()
        : thing;
    } else if (thing instanceof Object) {
      Object.keys(thing).forEach(
        function (key) {
          newObject[key] = this.deepCopy(thing[key], opts);
        }.bind(this)
      );
      return newObject;
    } else if ([undefined, null].indexOf(thing) > -1) {
      return thing;
    } else {
      if (thing.constructor.name === 'Symbol') {
        return Symbol(
          thing
            .toString()
            .replace(/^Symbol\(/, '')
            .slice(0, -1)
        );
      }
      return thing.__proto__.constructor(thing);
    }
  };
}
