import { Routes } from '@angular/router';
import { HomeComponent } from './listings/home/home.component';
import { NewListingComponent } from './listings/new-listing/new-listing.component';
import { ListingDetailsComponent } from './listings/listing-details/listing-details.component';
import { UserSignUpComponent } from './auth/user-sign-up/user-sign-up.component';
import { UserLoginComponent } from './auth/user-login/user-login.component';
import { UserAuthComponent } from './auth/user-auth/user-auth.component';
import { FavoriteListingComponent } from './listings/favorite-listing/favorite-listing.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'new-listing', component: NewListingComponent },
  { path: 'listing/:id', component: ListingDetailsComponent },
  { path: 'signup', component: UserAuthComponent },
  { path: 'login', component: UserLoginComponent },
  { path: 'fav', component: FavoriteListingComponent },
];
