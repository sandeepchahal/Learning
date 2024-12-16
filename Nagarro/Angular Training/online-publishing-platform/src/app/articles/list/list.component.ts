import { Component, OnInit } from '@angular/core';
import { FirestoreService } from '../../services/firestore.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { DocumentData, QueryDocumentSnapshot } from 'firebase/firestore';
import { Article } from '../models/article.model';

@Component({
  selector: 'app-article-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  featuredArticles: any[] = [];
  filteredFeaturedArticles: any[] = [];

  articles: Article[] = [];
  filteredArticles: Article[] = [];

  tags: any[] = [];
  authors: any[] = [];
  loading = true;
  featuredArticlesOpen: boolean = true;
  allArticlesOpen: boolean = true;
  searchText: string = '';
  filterType: string = 'articles';
  isAuthor: boolean | undefined = false;

  // Pagination properties
  currentPage = 0;
  articlesPerPage = 2; // Matches the `limit` in the Firestore query
  totalArticles = 0;
  lastDocument: QueryDocumentSnapshot<DocumentData> | null = null; // Last document for Firestore pagination
  firstDocument: QueryDocumentSnapshot<DocumentData> | null = null; // First document for "previous" navigation

  constructor(
    private firestoreService: FirestoreService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // Fetch regular articles
    this.allArticlesOpen = true;

    this.getArticles();

    // Fetch featured articles
    this.firestoreService.getFeaturedArticles().subscribe((data) => {
      this.featuredArticles = data;
      this.filteredFeaturedArticles = this.featuredArticles;
    });
    // Fetch Tags
    this.firestoreService.getTags().subscribe((data) => {
      this.tags = data;
    });

    // authors
    this.firestoreService.getAuthors().subscribe((data) => {
      this.authors = data;
      console.log('user', data);
    });

    // is Author
    this.authService.getUser().then((usr) => {
      if (usr?.isAuthor != undefined) {
        this.isAuthor = usr?.isAuthor;
      }
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
  onSearch(): void {
    const query = this.searchText.toLowerCase();
    console.log(this.filterType);
    if (this.filterType == 'articles') {
      this.filteredArticles = this.articles.filter((art) =>
        art.title.toLowerCase().includes(query)
      );
    } else {
      this.filteredArticles = this.articles.filter((art) =>
        art.authorName.toLowerCase().includes(query)
      );
    }

    // this.filteredFeaturedArticles = this.featuredArticles.filter((art) =>
    //   art.title.toLowerCase().includes(query)
    // );
  }

  goToArticleDetail(articleId: string): void {
    this.router.navigate(['/articles', articleId]);
  }

  navigateToComments(articleId: string): void {
    this.router.navigate(['/articles', articleId, 'comments']);
  }
  navigateToAuthor(authorId: string): void {
    console.log('author id', authorId);
    this.router.navigate(['/author', authorId]);
  }
  // Fetch articles for the current page
  getArticles(direction: 'next' | 'prev' = 'next'): void {
    this.loading = true;

    this.firestoreService
      .getPaginationArticles(
        this.articlesPerPage,
        direction,
        this.lastDocument,
        this.firstDocument
      )
      .subscribe((data) => {
        console.log(data);
        this.articles = data.articles;
        this.filteredArticles = this.articles;

        // Update pagination references
        this.lastDocument = data.lastDoc; // Update the last document
        this.firstDocument = data.firstDoc; // Update the first document
        console.log('data', data);
        this.currentPage =
          direction === 'next' ? this.currentPage + 1 : this.currentPage - 1;
      });
  }

  // Reset pagination when needed
  resetPagination(): void {
    this.currentPage = 1;
    this.lastDocument = null;
    this.firstDocument = null;
    this.getArticles();
  }

  filterByTag(tagName: string) {
    this.firestoreService.getArticles().subscribe((articles) => {
      if (tagName == 'All') {
        this.currentPage = 0;
        this.getArticles();
      } else {
        this.currentPage = 1;
        this.articles = articles;
        this.filteredArticles = this.articles.filter((article: any) =>
          article.tags.includes(tagName)
        );
      }
    });
  }
}
