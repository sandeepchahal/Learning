import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { FirestoreService } from '../../services/firestore.service';
import { ActivatedRoute } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';
import { User } from 'firebase/auth';
import { MyUser } from '../../models/user.model';

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
  replyContent = ''; // For reply content
  replyingTo: string | null = null; // Track which comment is being replied to
  user: MyUser | null = null;
  userName: string | null = null;

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
      if (isAuthenticated) {
        // // Redirect unauthenticated users to the login page
        // window.location.href = '/auth/login';
        this.authService.getUser().then((user) => {
          this.userName = user?.displayName || 'Anonymous'; // Get user name
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
          this.userName = user.displayName; // or whatever user info you need, e.g., userId
          this.user = user;
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
  toggleReply(commentId: string): void {
    this.replyingTo = this.replyingTo === commentId ? null : commentId;
    this.replyContent = '';
  }

  addReply(parentCommentId: string): void {
    if (!this.replyContent.trim()) return;

    this.authService
      .getUser()
      .then((user) => {
        if (user) {
          this.userName = user.displayName;
          this.user = user;
          const replyComment = {
            articleId: this.articleId,
            content: this.replyContent.trim(),
            createdAt: new Date(),
            likes: 0,
            parentCommentId: parentCommentId,
            userId: user.uid,
            userName: user.displayName,
          };

          this.firestoreService.addComment(replyComment).subscribe(() => {
            this.loadComments();
            this.replyContent = '';
            this.replyingTo = null;
          });
        } else {
          console.error('User not found!');
        }
      })
      .catch((error) => {
        console.error('Error getting user:', error);
      });
  }

  likeComment(commentId: string): void {
    this.firestoreService
      .likeComment(commentId)
      .then(() => {
        this.loadComments(); // Refresh the comments after updating
      })
      .catch((error) => {
        console.error('Error liking comment:', error);
      });
  }

  dislikeComment(commentId: string): void {
    this.firestoreService
      .dislikeComment(commentId)
      .then(() => {
        this.loadComments(); // Refresh the comments after updating
      })
      .catch((error) => {
        console.error('Error disliking comment:', error);
      });
  }
  getCommentReplies(parentId: string) {
    return this.comments.filter(
      (comment) => comment.parentCommentId === parentId
    );
  }
}
