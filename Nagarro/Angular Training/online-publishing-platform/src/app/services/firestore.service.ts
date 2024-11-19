// src/app/services/firestore.service.ts
import { Injectable } from '@angular/core';
import { initializeApp, provideFirebaseApp } from '@angular/fire/app';
import {
  Firestore,
  collection,
  addDoc,
  collectionData,
  doc,
  getDoc,
  query,
  where,
  orderBy,
  limit,
} from '@angular/fire/firestore';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FirestoreService {
  constructor(private firestore: Firestore) {}

  // Article Methods
  getArticles(): Observable<any[]> {
    const articlesRef = collection(this.firestore, 'articles');
    const articlesQuery = query(
      articlesRef,
      where('isDraft', '==', false),
      limit(10)
    );
    console.log(
      'Checking the data',
      collectionData(articlesQuery, { idField: 'id' })
    );
    return collectionData(articlesQuery, { idField: 'id' });
  }
  addArticle(article: any): Observable<void> {
    const articlesRef = collection(this.firestore, 'articles');
    return new Observable((observer) => {
      addDoc(articlesRef, {
        authorName: article.authorName,
        content: article.content,
        description: article.description,
        tags: article.tags.split(',').map((tag: string) => tag.trim()), // Convert tags to an array
        thumbnailUrl: article.thumbnailUrl,
        title: article.title,
        publishDate: article.publishDate,
        isDraft: article.isDraft,
        featured: article.featured,
        likes: article.likes,
        views: article.views,
      })
        .then(() => {
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error adding article: ', error);
          observer.error(error);
        });
    });
  }

  getFeaturedArticles(): Observable<any[]> {
    const articlesRef = collection(this.firestore, 'articles');
    const featuredQuery = query(
      articlesRef,
      where('featured', '==', true),
      where('isDraft', '==', false),
      limit(5)
    );
    return collectionData(featuredQuery, { idField: 'id' });
  }

  // Comments Methods
  getArticleComments(articleId: string): Observable<any[]> {
    console.log('article Id - ', articleId);
    const commentsRef = collection(this.firestore, 'comments');
    const commentsQuery = query(
      commentsRef,
      where('articleId', '==', articleId)
    );
    return collectionData(commentsQuery, { idField: 'id' });
  }

  addComment(comment: any) {
    const commentsRef = collection(this.firestore, 'comments');
    const addCommentPromise = addDoc(commentsRef, {
      ...comment,
      createdAt: new Date(),
    });

    // Convert the Promise to an Observable using 'from'
    return from(addCommentPromise);
  }

  // User Methods
  getUserProfile(userId: string): Observable<any> {
    const userDoc = doc(this.firestore, 'users', userId);
    return new Observable((subscriber) => {
      getDoc(userDoc).then((doc) => {
        if (doc.exists()) {
          subscriber.next({ id: doc.id, ...doc.data() });
        } else {
          subscriber.next(null);
        }
        subscriber.complete();
      });
    });
  }
  getArticleById(articleId: string): Observable<any> {
    const articleDoc = doc(this.firestore, 'articles', articleId);
    return new Observable((subscriber) => {
      getDoc(articleDoc).then((docSnap) => {
        if (docSnap.exists()) {
          subscriber.next({ id: docSnap.id, ...docSnap.data() });
        } else {
          subscriber.next(null);
        }
        subscriber.complete();
      });
    });
  }
}
