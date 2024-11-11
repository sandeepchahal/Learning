import { Component, Input, OnInit } from '@angular/core';
import { Comment } from './models/comment.model.js';
@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
})
export class CommentComponent implements OnInit {
  @Input() listingId!: number;
  comments: Comment[] = [];
  newCommentContent = '';

  ngOnInit(): void {
    this.loadComments();
  }

  loadComments(): void {
    // For now, load hardcoded comments or pull from CommentService
    this.comments = [
      {
        id: 1,
        listingId: this.listingId,
        content: 'Nice place!',
        author: 'John',
        timestamp: new Date(),
      },
    ];
  }

  addComment(): void {
    const newComment: Comment = {
      id: this.comments.length + 1,
      listingId: this.listingId,
      content: this.newCommentContent,
      author: 'Anonymous', // Placeholder author
      timestamp: new Date(),
    };
    this.comments.push(newComment);
    this.newCommentContent = '';
  }
}
