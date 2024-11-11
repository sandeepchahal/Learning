import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoriteListingComponent } from './favorite-listing.component';

describe('FavoriteListingComponent', () => {
  let component: FavoriteListingComponent;
  let fixture: ComponentFixture<FavoriteListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FavoriteListingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FavoriteListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
