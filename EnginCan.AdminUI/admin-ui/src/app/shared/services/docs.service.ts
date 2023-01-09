import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from 'src/app/shared/services/http.service';
import { map } from 'rxjs/operators';

@Injectable()
export class DocsService {
  constructor(private http: HttpService) {}

  postUploadImage(base64Image: string) {
    return this.http.post(
      environment.api,
      { version: '1.0', url: `Docs/Images/Upload` },
      { image: base64Image }
    );
  }
}
