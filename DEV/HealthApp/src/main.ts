// import { bootstrapApplication } from '@angular/platform-browser';
// import { appConfig } from './app/app.config';
// import { AppComponent } from './app/app.component';

// bootstrapApplication(AppComponent, appConfig
// )
//   .catch((err) => console.error(err));

import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

function loadConfig(): Promise<any> {
  //return fetch('/assets/config/config.json')
  return fetch('assets/config/config.json')
    .then((response) => {
      if (!response.ok) {
        console.log('Failed to load configuration:', response.statusText);
        throw new Error(`Failed to load configuration: ${response.statusText}`);
      }
      return response.json();
    })
    .then((config) => {
      // Guardar la configuración en una variable global o servicio
      (window as any).appConfig = config;
    })
    .catch((error) => {
      console.log('Error loading configuration:', error);
      // Proporciona valores predeterminados si el archivo no se carga
      (window as any).appConfig = { apiBaseUrl: 'http://default-url/api' };
    });
}

loadConfig()
  .then(() => {
    bootstrapApplication(AppComponent, appConfig)
      .catch((err) => console.error(err));
  })
  .catch((err) => {
    console.error('Error loading configuration:', err);
  });