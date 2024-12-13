import { Component } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
})

export class NavComponent {
  public isMenuCollapsed: boolean;

  constructor() {
    this.isMenuCollapsed = true;
  }
}
