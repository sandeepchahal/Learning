import { Component, OnInit } from '@angular/core';
import { ListingService } from '../services/listing.service';
import { Listing } from '../services/listing.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-listing',
  templateUrl: './new-listing.component.html',
  styleUrls: ['./new-listing.component.css'],
})
export class NewListingComponent implements OnInit {
  newListing: Listing = {
    id: 0,
    title: '',
    location: '',
    price: 0,
    description: '',
    amenities: [],
    isFurnished: false,
    isVegetarianPreferred: false,
    imageUrl: '',
    isFavorite: false,
  };

  amenities: string[] = [];

  constructor(private listingService: ListingService, private router: Router) {}

  ngOnInit(): void {
    this.amenities = this.listingService.getAmenities();
  }

  toggleAmenity(amenity: string): void {
    const index = this.newListing.amenities.indexOf(amenity);
    if (index > -1) {
      this.newListing.amenities.splice(index, 1);
    } else {
      this.newListing.amenities.push(amenity);
    }
  }

  addListing(): void {
    this.listingService.addListing(this.newListing);
    this.resetForm();
    this.navigateToHome();
  }

  resetForm(): void {
    this.newListing = {
      id: 0,
      title: '',
      location: '',
      price: 0,
      description: '',
      amenities: [],
      isFurnished: false,
      isVegetarianPreferred: false,
      imageUrl: '',
      isFavorite: false,
    };
  }
  private navigateToHome() {
    this.router.navigate(['/']);
  }
}
