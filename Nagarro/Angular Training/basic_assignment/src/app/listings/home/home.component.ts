import { Component, OnInit } from '@angular/core';
import { FilterCriteria, ListingService } from '../services/listing.service';
import { Listing } from '../services/listing.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  listings: Listing[] = [];
  filteredListings: Listing[] = [];
  showFavorites = false;

  constructor(private listingService: ListingService, private router: Router) {}

  ngOnInit(): void {
    this.listings = this.listingService.getListings();
    this.filteredListings = this.listings;
  }

  viewDetails(id: number): void {
    this.router.navigate(['/listing', id]);
  }

  markAsFavorite(id: number): void {
    this.showFavorites = true;
    this.listingService.markFav(id);
    this.listings = this.listingService.getListings();
  }

  toggleFavorites(): void {
    this.showFavorites = !this.showFavorites;
  }

  applyFilter(criteria: FilterCriteria): void {
    this.filteredListings = this.listingService.filterListings(
      this.listings,
      criteria
    );
  }
}
