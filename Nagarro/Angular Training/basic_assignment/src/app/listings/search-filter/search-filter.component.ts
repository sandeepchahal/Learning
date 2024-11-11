import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ListingService } from '../services/listing.service';

interface FilterCriteria {
  location?: string;
  minPrice?: number;
  maxPrice?: number;
  amenities?: string[];
}

@Component({
  selector: 'app-search-filter',
  templateUrl: './search-filter.component.html',
  styleUrls: ['./search-filter.component.css'],
})
export class SearchFilterComponent implements OnInit {
  @Output() filterCriteria = new EventEmitter<FilterCriteria>();

  filters: FilterCriteria = {
    location: '',
    minPrice: undefined,
    maxPrice: undefined,
    amenities: [],
  };

  amenities: string[] = [];

  constructor(private listingService: ListingService) {}

  ngOnInit(): void {
    this.amenities = this.listingService.getAmenities();
  }

  toggleAmenity(amenity: string): void {
    const index = this.filters.amenities?.indexOf(amenity);
    if (index !== undefined && index > -1) {
      this.filters.amenities?.splice(index, 1);
    } else {
      this.filters.amenities?.push(amenity);
    }
  }

  applyFilter(): void {
    this.filterCriteria.emit(this.filters);
  }
}
