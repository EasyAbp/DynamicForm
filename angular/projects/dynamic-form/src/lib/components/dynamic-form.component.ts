import { Component, OnInit } from '@angular/core';
import { DynamicFormService } from '../services/dynamic-form.service';

@Component({
  selector: 'lib-dynamic-form',
  template: ` <p>dynamic-form works!</p> `,
  styles: [],
})
export class DynamicFormComponent implements OnInit {
  constructor(private service: DynamicFormService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
