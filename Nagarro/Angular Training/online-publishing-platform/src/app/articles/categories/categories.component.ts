import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FirestoreService } from '../../services/firestore.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  categories: any[] = [];
  @Output() categorySelected = new EventEmitter<any>();

  constructor(private firestoreService: FirestoreService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.firestoreService.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  filterByCategory(category: any): void {
    this.categorySelected.emit(category);
  }
}
