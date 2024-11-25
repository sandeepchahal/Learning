import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FirestoreService } from '../../services/firestore.service';
import { Router } from '@angular/router';
import { EditorModule } from '@tinymce/tinymce-angular';
import { Article } from '../models/article.model';
import { Timestamp } from 'firebase/firestore';

@Component({
  selector: 'app-editor',
  standalone: true,
  imports: [CommonModule, FormsModule, EditorModule],
  templateUrl: './editor.component.html',
  styleUrl: './editor.component.css',
})
export class EditorComponent {
  article: Article = {
    id: '',
    authorName: '',
    content: '',
    tags: '',
    categoryId: '',
    thumbnailUrl: '',
    title: '',
    publishDate: Timestamp.fromDate(new Date()),

    isDraft: false,
    isFeatured: false,
    likes: 0,
    views: 0,
  };

  categories: any[] = [];
  constructor(
    private firestoreService: FirestoreService,
    private router: Router
  ) {
    this.fetchCategories();
  } // Inject the service

  onSubmit() {
    if (this.validateForm()) {
      // Call the addArticle method to save the article
      this.firestoreService.addArticle(this.article).subscribe(
        () => {
          console.log('Article successfully added!');
          this.resetForm();
          this.router.navigate(['/articles']);
        },
        (error) => {
          console.error('Error adding article:', error);
        }
      );
    } else {
      console.log('Form is not valid!');
    }
  }

  validateForm(): boolean {
    return (
      this.article.authorName.trim() !== '' &&
      this.article.content.trim() !== '' &&
      this.article.title.trim() !== ''
    );
  }

  resetForm() {
    this.article = {
      id: '',
      authorName: '',
      content: '',
      tags: '',
      thumbnailUrl: '',
      title: '',
      publishDate: Timestamp.fromDate(new Date()),
      categoryId: '',
      isDraft: false,
      isFeatured: false,
      likes: 0,
      views: 0,
    };
  }
  fetchCategories() {
    this.firestoreService.getCategories().subscribe((data) => {
      this.categories = data;
      console.log('categories', data);
    });
  }
}
