// src/app/components/test-firestore/test-firestore.component.ts
import { Component, OnInit } from '@angular/core';
import { FirestoreService } from '../services/firestore.service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-test-firestore',
  imports: [CommonModule],
  standalone: true,
  template: `
    <div class="container mx-auto p-4">
      <h2 class="text-2xl font-bold mb-4">Recent Articles</h2>
      <div class="grid gap-4">
        <div
          *ngFor="let article of articles$ | async"
          class="p-4 border rounded"
        >
          <h3 class="text-xl font-semibold">{{ article.title }}</h3>
          <p class="text-gray-600">{{ article.description }}</p>
          <p class="text-sm text-gray-500">By {{ article.authorName }}</p>
        </div>
      </div>

      <h2 class="text-2xl font-bold my-4">Featured Articles</h2>
      <div class="grid gap-4">
        <div
          *ngFor="let article of featuredArticles$ | async"
          class="p-4 border rounded bg-blue-50"
        >
          <h3 class="text-xl font-semibold">{{ article.title }}</h3>
          <p class="text-gray-600">{{ article.description }}</p>
          <p class="text-sm text-gray-500">By {{ article.authorName }}</p>
        </div>
      </div>
    </div>
  `,
})
export class TestFirestoreComponent implements OnInit {
  articles$!: Observable<any[]>; // Use `!` to ensure TypeScript knows it will be initialized
  featuredArticles$!: Observable<any[]>;

  constructor(private firestoreService: FirestoreService) {}

  ngOnInit(): void {
    // Initialize the observables in `ngOnInit` to ensure `firestoreService` is ready
    this.articles$ = this.firestoreService.getArticles();
    console.log(this.articles$);
    this.featuredArticles$ = this.firestoreService.getFeaturedArticles();
  }
}
