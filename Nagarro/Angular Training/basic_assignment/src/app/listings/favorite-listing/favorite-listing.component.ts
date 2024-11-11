import { Component } from '@angular/core';
import { Listing, ListingService } from '../services/listing.service';

@Component({
  selector: 'app-favorite-listing',
  templateUrl: './favorite-listing.component.html',
  styleUrl: './favorite-listing.component.css',
})
export class FavoriteListingComponent {
  filteredListings: Listing[] = [];

  constructor(private listingService: ListingService) {}
  showAll() {
    this.filteredListings = this.listingService.getFav();
  }
}
