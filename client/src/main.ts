import { bootstrapApplication } from '@angular/platform-browser';
import { appConfigDefault } from './app/app.config';
import { App } from './app/app';
import { Capacitor } from '@capacitor/core';
import { registerLocaleData } from '@angular/common';
import localeCs from '@angular/common/locales/cs';

let config = appConfigDefault;

const platform = Capacitor.getPlatform();


registerLocaleData(localeCs);

bootstrapApplication(App, config)
  .catch((err) => console.error(err));
