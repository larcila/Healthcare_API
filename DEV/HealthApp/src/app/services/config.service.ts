// import { Injectable, Inject } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class ConfigService {
//   constructor(@Inject('APP_CONFIG') private config: any) {}

//   get apiBaseUrl(): string {
//     return this.config?.apiBaseUrl || 'http://localhost:4200/api'; // dafault value
//   }
// }

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  get apiBaseUrl(): string {
    return (window as any).appConfig?.apiBaseUrl || '';
  }
}