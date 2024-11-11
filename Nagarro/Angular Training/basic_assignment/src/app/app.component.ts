import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { AuthModule } from './auth/auth.module';
import { ListingsModule } from './listings/listings.module';
import { SharedModule } from './shared/shared.module';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AuthModule, ListingsModule, SharedModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'RentHub';
}
