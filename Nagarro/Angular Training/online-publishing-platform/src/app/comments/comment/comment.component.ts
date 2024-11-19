import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { FirestoreService } from '../../services/firestore.service';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-comment',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
})
export class CommentComponent implements OnInit {
  articleId: any = null;
  comments: any[] = [];
  newComment = '';
  user: string | null = null;

  constructor(
    private firestoreService: FirestoreService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.articleId = this.route.snapshot.paramMap.get('id');
    // Check if user is authenticated
    this.authService.isAuthenticated().then((isAuthenticated) => {
      if (!isAuthenticated) {
        // Redirect unauthenticated users to the login page
        window.location.href = '/auth/login';
      } else {
        this.authService.getUser().then((user) => {
          this.user = user?.displayName || 'Anonymous'; // Get user name
        });
      }
    });

    // Fetch comments for the article
    console.log('loading comments');
    this.loadComments();
  }

  loadComments(): void {
    this.firestoreService
      .getArticleComments(this.articleId)
      .subscribe((comment) => {
        if (comment) {
          this.comments = comment;
          console.log('comment', comment);
          this.cdr.detectChanges();
        } else {
          console.error('Comments not found!');
        }
      });
  }

  addComment(): void {
    // Check if user is authenticated and has the necessary data
    this.authService
      .getUser()
      .then((user) => {
        if (user) {
          // Assign user data to the local user variable
          this.user = user.displayName; // or whatever user info you need, e.g., userId

          // Construct the comment object
          const comment = {
            articleId: this.articleId,
            content: this.newComment.trim(),
            createdAt: new Date(),
            likes: 0, // Assuming initially no likes
            parentCommentId: null, // You can set this if it's a reply to another comment
            userId: user.uid, // Assuming user has userId
            userName: user.displayName, // Use the assigned userName
          };

          this.firestoreService.addComment(comment).subscribe(() => {
            this.loadComments(); // Refresh the comments list
            this.newComment = ''; // Clear the input field
          });
        } else {
          console.error('User not found!');
        }
      })
      .catch((error) => {
        console.error('Error getting user:', error);
      });
  }
}
