import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { ListingComponent } from './listing/listing.component';
import { ListingDetailsComponent } from './listing-details/listing-details.component';
import { NewListingComponent } from './new-listing/new-listing.component';
import { PreviewPostComponent } from './preview-post/preview-post.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { SearchFilterComponent } from './search-filter/search-filter.component';
import { FavoriteListingComponent } from './favorite-listing/favorite-listing.component';

@NgModule({
  declarations: [
    HomeComponent,
    ListingComponent,
    ListingDetailsComponent,
    NewListingComponent,
    PreviewPostComponent,
    SearchFilterComponent,
    FavoriteListingComponent,
  ],
  imports: [CommonModule, SharedModule, FormsModule],
})
export class ListingsModule {}
