import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FirestoreService } from '../../services/firestore.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-editor',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './editor.component.html',
  styleUrl: './editor.component.css',
})
export class EditorComponent {
  article = {
    authorName: '',
    content: '',
    description: '',
    tags: '',
    thumbnailUrl: '',
    title: '',
    publishDate: '',
    isDraft: false,
    featured: false,
    likes: 0,
    views: 0,
  };

  constructor(
    private firestoreService: FirestoreService,
    private router: Router
  ) {} // Inject the service

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
      this.article.title.trim() !== '' &&
      this.article.publishDate.trim() !== ''
    );
  }

  resetForm() {
    this.article = {
      authorName: '',
      content: '',
      description: '',
      tags: '',
      thumbnailUrl: '',
      title: '',
      publishDate: '',
      isDraft: false,
      featured: false,
      likes: 0,
      views: 0,
    };
  }
}
