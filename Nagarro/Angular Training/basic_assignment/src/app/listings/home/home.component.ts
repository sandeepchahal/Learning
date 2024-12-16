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
  highlights: Listing[] = [];
  paginatedListings: Listing[] = [];
  currentPage: number = 1;
  pageSize: number = 3; // Number of items per page
  totalPages: number = 1;
  showFavorites = false;
  // Carousel variables
  activeSlide = 0;
  carouselInterval: any;
  constructor(private listingService: ListingService, private router: Router) {}

  ngOnInit(): void {
    this.listings = this.listingService.getListings();
    this.filteredListings = this.listings;
    this.calculatePagination();
    this.highlights = this.listingService.getHighlightList();
  }

  viewDetails(id: number): void {
    this.router.navigate(['/listing', id]);
  }

  markAsFavorite(id: number): void {
    this.showFavorites = true;
    this.listingService.markFav(id);
    this.listings = this.listingService.getListings();
    this.calculatePagination();
  }

  toggleFavorites(): void {
    this.showFavorites = !this.showFavorites;
  }

  applyFilter(criteria: FilterCriteria): void {
    this.filteredListings = this.listingService.filterListings(
      this.listings,
      criteria
    );
    this.calculatePagination();
  }

  calculatePagination(): void {
    this.totalPages = Math.ceil(this.filteredListings.length / this.pageSize);
    this.changePage(1); // Reset to the first page
  }

  changePage(page: number): void {
    this.currentPage = page;
    const startIndex = (page - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedListings = this.filteredListings.slice(startIndex, endIndex);
  }

  stopCarousel(): void {
    clearInterval(this.carouselInterval);
  }

  // Pagination methods
  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }
}
