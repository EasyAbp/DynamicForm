import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'DynamicForm',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44313/',
    redirectUri: baseUrl,
    clientId: 'DynamicForm_App',
    responseType: 'code',
    scope: 'offline_access DynamicForm',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44313',
      rootNamespace: 'EasyAbp.DynamicForm',
    },
    DynamicForm: {
      url: 'https://localhost:44341',
      rootNamespace: 'EasyAbp.DynamicForm',
    },
  },
} as Environment;
