import { Component, OnInit } from '@angular/core';
import { FirestoreService } from '../../services/firestore.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';

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

  articles: any[] = [];
  filteredArticles: any[] = [];

  tags: any[] = [];
  authors: any[] = [];
  loading = true;
  featuredArticlesOpen: boolean = true;
  allArticlesOpen: boolean = true;
  searchText: string = '';
  filterType: string = 'articles';
  isAuthor: boolean | undefined = false;

  constructor(
    private firestoreService: FirestoreService,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    // Fetch regular articles
    this.allArticlesOpen = true;

    this.firestoreService.getArticles().subscribe((data) => {
      this.articles = data;
      this.filteredArticles = this.articles;
      this.loading = false;
    });

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
}
