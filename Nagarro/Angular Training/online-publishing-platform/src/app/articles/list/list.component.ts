import { Component, OnInit } from '@angular/core';
import { FirestoreService } from '../../services/firestore.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-article-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  articles: any[] = [];
  featuredArticles: any[] = [];
  loading = true;
  featuredArticlesOpen: boolean = false;
  allArticlesOpen: boolean = false;

  constructor(private firestoreService: FirestoreService) {}

  ngOnInit(): void {
    // Fetch regular articles
    this.allArticlesOpen = true;
    this.firestoreService.getArticles().subscribe((data) => {
      this.articles = data;
      this.loading = false;
    });

    // Fetch featured articles
    this.firestoreService.getFeaturedArticles().subscribe((data) => {
      this.featuredArticles = data;
    });
  }
  // Toggle featured articles visibility
  toggleFeaturedArticles() {
    this.featuredArticlesOpen = !this.featuredArticlesOpen;
  }

  // Toggle all articles visibility
  toggleAllArticles() {
    this.allArticlesOpen = !this.allArticlesOpen;
  }
}
