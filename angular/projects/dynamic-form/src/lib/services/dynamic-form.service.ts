import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DynamicFormService {
  apiName = 'DynamicForm';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/DynamicForm/sample' },
      { apiName: this.apiName }
    );
  }
}
