import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserLoginComponent } from './user-login/user-login.component';
import { UserSignUpComponent } from './user-sign-up/user-sign-up.component';
import { UserAuthComponent } from './user-auth/user-auth.component';

@NgModule({
  declarations: [UserLoginComponent, UserSignUpComponent, UserAuthComponent],
  imports: [CommonModule, FormsModule],
})
export class AuthModule {}
