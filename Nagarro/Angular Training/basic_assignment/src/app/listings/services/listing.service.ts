import { Injectable } from '@angular/core';

export interface Listing {
  id: number;
  title: string;
  location: string;
  price: number;
  description: string;
  amenities: string[];
  isFurnished: boolean;
  isVegetarianPreferred: boolean;
  imageUrl: string;
  isFavorite: boolean;
}
export interface FilterCriteria {
  location?: string;
  minPrice?: number;
  maxPrice?: number;
  amenities?: string[];
}

@Injectable({
  providedIn: 'root',
})
export class ListingService {
  private listings: Listing[] = [
    {
      id: 1,
      title: 'Modern Apartment in City Center',
      location: 'New York, NY',
      price: 2000,
      description:
        'A beautiful modern apartment located in the heart of the city.',
      amenities: ['Wi-Fi', 'Air Conditioning', 'Gym Access'],
      isFurnished: true,
      isVegetarianPreferred: false,
      isFavorite: false,
      imageUrl:
        'https://st2.depositphotos.com/3092723/5368/i/450/depositphotos_53689175-stock-photo-home-for-rent.jpg', // Placeholder image URL
    },
    {
      id: 2,
      title: 'Cozy Studio near the Park',
      location: 'San Francisco, CA',
      price: 1500,
      description: 'A cozy studio apartment located near Golden Gate Park.',
      amenities: ['Wi-Fi', 'Washer/Dryer'],
      isFurnished: true,
      isVegetarianPreferred: true,
      isFavorite: false,
      imageUrl:
        'https://media.istockphoto.com/id/155700839/photo/a-beautiful-home-available-for-rent.jpg?s=612x612&w=0&k=20&c=aPwqJ67O3CGGItsDoI8fuGwAuTR3L3B80tImG2mlQQ8=', // Placeholder image URL
    },
    {
      id: 3,
      title: 'Luxury Villa with Ocean View',
      location: 'Malibu, CA',
      price: 5000,
      description:
        'A stunning villa with a breathtaking view of the Pacific Ocean.',
      amenities: ['Pool', 'Wi-Fi', 'Ocean View', 'Jacuzzi'],
      isFurnished: true,
      isVegetarianPreferred: false,
      isFavorite: false,
      imageUrl:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt0HD1EEYJUWzC8XIdMMh8N_0urnDXiIhZ0w&s',
    },
    {
      id: 4,
      title: 'Charming Cottage in Countryside',
      location: 'Austin, TX',
      price: 1200,
      description:
        'A peaceful and cozy cottage surrounded by nature in the countryside.',
      amenities: ['Wi-Fi', 'Fireplace'],
      isFurnished: true,
      isVegetarianPreferred: true,
      isFavorite: false,
      imageUrl:
        'https://a0.muscache.com/im/pictures/7262ce4c-f55f-4f91-a523-1508cc76a602.jpg?im_w=720',
    },
    {
      id: 5,
      title: 'Spacious Loft in Downtown',
      location: 'Chicago, IL',
      price: 3000,
      description:
        'A large and airy loft located in the heart of downtown Chicago.',
      amenities: ['Wi-Fi', 'Gym Access', 'Parking'],
      isFurnished: true,
      isVegetarianPreferred: false,
      isFavorite: false,
      imageUrl:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSJxULtSmIPpdR3Ol_xCqk_DEc11pIpowoNz4NJPCX836DtcDQlamV1CAs7J0STaekbZg4&usqp=CAU',
    },
    {
      id: 6,
      title: 'Bright and Airy Studio',
      location: 'Los Angeles, CA',
      price: 1800,
      description:
        'A modern studio with plenty of natural light in a great location.',
      amenities: ['Wi-Fi', 'Balcony'],
      isFurnished: true,
      isVegetarianPreferred: true,
      isFavorite: false,
      imageUrl:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTl25vHRyqM1Ex8JMmxYFvQ0vh5mDpjfYMZvQ&s',
    },
    {
      id: 7,
      title: 'Sleek Urban Apartment',
      location: 'Miami, FL',
      price: 2500,
      description:
        'A sleek apartment with modern finishes and a vibrant city view.',
      amenities: ['Wi-Fi', 'Washer/Dryer', 'Parking'],
      isFurnished: false,
      isVegetarianPreferred: false,
      isFavorite: false,
      imageUrl:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQm4cjT6Gy-vv-0tp2pLfi4dj1skJjMABZGtQ&s',
    },
    {
      id: 8,
      title: 'Rustic Mountain Cabin',
      location: 'Aspen, CO',
      price: 3500,
      description:
        'A cozy cabin in the mountains, perfect for winter getaways.',
      amenities: ['Fireplace', 'Ski Access', 'Jacuzzi'],
      isFurnished: true,
      isVegetarianPreferred: false,
      isFavorite: false,
      imageUrl:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTlvvcodYFUXKLyO8ud5LTPYdarsVZtupDHDQ&s',
    },
  ];

  private highlightListing: Listing[] = [
    {
      id: 7,
      title: 'Sleek Urban Apartment',
      location: 'Miami, FL',
      price: 2500,
      description:
        'A sleek apartment with modern finishes and a vibrant city view.',
      amenities: ['Wi-Fi', 'Washer/Dryer', 'Parking'],
      isFurnished: false,
      isVegetarianPreferred: false,
      isFavorite: false,
      imageUrl:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQm4cjT6Gy-vv-0tp2pLfi4dj1skJjMABZGtQ&s',
    },
    {
      id: 8,
      title: 'Rustic Mountain Cabin',
      location: 'Aspen, CO',
      price: 3500,
      description:
        'A cozy cabin in the mountains, perfect for winter getaways.',
      amenities: ['Fireplace', 'Ski Access', 'Jacuzzi'],
      isFurnished: true,
      isVegetarianPreferred: false,
      isFavorite: false,
      imageUrl:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTlvvcodYFUXKLyO8ud5LTPYdarsVZtupDHDQ&s',
    },
  ];

  private amenitiesList: string[] = [
    'Wi-Fi',
    'Air Conditioning',
    'Gym Access',
    'Washer/Dryer',
    'Swimming Pool',
  ];

  private favoriteListing: number[] = [];

  getAmenities(): string[] {
    return this.amenitiesList;
  }

  getListings(): Listing[] {
    return this.listings;
  }
  getHighlightList(): Listing[] {
    return this.highlightListing;
  }
  getListingById(id: number): Listing | undefined {
    return this.listings.find((listing) => listing.id === id);
  }

  addListing(listing: Listing): void {
    listing.id = this.listings.length + 1;
    this.listings.push(listing);
  }

  markFav(id: number): void {
    if (!this.favoriteListing.includes(id)) {
      this.favoriteListing.push(id);

      const listing = this.listings.find((listing) => listing.id === id);
      if (listing) {
        listing.isFavorite = true;
      }
    } else {
      const index = this.favoriteListing.indexOf(id);
      if (index > -1) {
        this.favoriteListing.splice(index, 1);
      }

      const listing = this.listings.find((listing) => listing.id === id);
      if (listing) {
        listing.isFavorite = false;
      }
    }
  }

  getFav(): Listing[] {
    return this.listings.filter((listing) =>
      this.favoriteListing.includes(listing.id)
    );
  }
  filterListings(listings: Listing[], criteria: FilterCriteria): Listing[] {
    return listings.filter((listing) => {
      const matchesLocation = criteria.location
        ? listing.location
            .toLowerCase()
            .includes(criteria.location.toLowerCase())
        : true;
      const matchesMinPrice =
        criteria.minPrice !== undefined
          ? listing.price >= criteria.minPrice
          : true;
      const matchesMaxPrice =
        criteria.maxPrice !== undefined
          ? listing.price <= criteria.maxPrice
          : true;
      const matchesAmenities =
        criteria.amenities && criteria.amenities.length > 0
          ? criteria.amenities.every((amenity) =>
              listing.amenities.includes(amenity)
            )
          : true;

      return (
        matchesLocation &&
        matchesMinPrice &&
        matchesMaxPrice &&
        matchesAmenities
      );
    });
  }
}
