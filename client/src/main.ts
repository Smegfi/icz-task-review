import { bootstrapApplication } from '@angular/platform-browser';
import { appConfigDefault } from './app/app.config';
import { App } from './app/app';
import { registerLocaleData } from '@angular/common';
import localeCs from '@angular/common/locales/cs';

const config = appConfigDefault;



registerLocaleData(localeCs);

bootstrapApplication(App, config)
  .catch((err) => console.error(err));
