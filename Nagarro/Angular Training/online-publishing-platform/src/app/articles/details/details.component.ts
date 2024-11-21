import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FirestoreService } from '../../services/firestore.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
})
export class DetailsComponent implements OnInit {
  article: any = null;
  relatedArticles: any[] = [];
  loading = true;
  categoryName = '';

  constructor(
    private route: ActivatedRoute,
    private firestoreService: FirestoreService
  ) {}

  ngOnInit(): void {
    // Get the article ID from the route
    const articleId = this.route.snapshot.paramMap.get('id');
    if (articleId) {
      this.fetchArticleDetails(articleId);
      this.fetchRelatedArticles(articleId);
    }
  }

  fetchArticleDetails(articleId: string): void {
    this.firestoreService.getArticleById(articleId).subscribe((article) => {
      if (article) {
        this.article = article;
        this.firestoreService
          .getCategoryById(article.categoryId)
          .subscribe((data) => {
            this.categoryName = data.name;
          });
        this.loading = false;
      } else {
        console.error('Article not found!');
        this.loading = false;
      }
    });
  }

  fetchRelatedArticles(articleId: string): void {
    // Example: Fetch related articles (this might need adjustment based on your schema)
    this.firestoreService.getArticles().subscribe((articles) => {
      this.relatedArticles = articles
        .filter((a) => a.id !== articleId)
        .slice(0, 5);
    });
  }
}
