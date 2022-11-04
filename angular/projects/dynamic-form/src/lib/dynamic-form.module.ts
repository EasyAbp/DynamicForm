import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DynamicFormComponent } from './components/dynamic-form.component';
import { DynamicFormRoutingModule } from './dynamic-form-routing.module';

@NgModule({
  declarations: [DynamicFormComponent],
  imports: [CoreModule, ThemeSharedModule, DynamicFormRoutingModule],
  exports: [DynamicFormComponent],
})
export class DynamicFormModule {
  static forChild(): ModuleWithProviders<DynamicFormModule> {
    return {
      ngModule: DynamicFormModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DynamicFormModule> {
    return new LazyModuleFactory(DynamicFormModule.forChild());
  }
}
