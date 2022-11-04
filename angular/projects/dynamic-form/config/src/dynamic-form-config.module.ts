import { ModuleWithProviders, NgModule } from '@angular/core';
import { DYNAMIC_FORM_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class DynamicFormConfigModule {
  static forRoot(): ModuleWithProviders<DynamicFormConfigModule> {
    return {
      ngModule: DynamicFormConfigModule,
      providers: [DYNAMIC_FORM_ROUTE_PROVIDERS],
    };
  }
}
