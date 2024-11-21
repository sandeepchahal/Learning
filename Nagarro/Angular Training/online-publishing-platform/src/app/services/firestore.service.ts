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
  updateDoc,
  increment,
  docData,
  Timestamp,
  setDoc,
} from '@angular/fire/firestore';
import { from, Observable } from 'rxjs';
import { Category } from '../articles/models/category.model';
import { Article } from '../articles/models/article.model';
import { MyUser } from '../models/user.model';
import { subscribe } from 'diagnostics_channel';

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
  addArticle(article: Article): Observable<void> {
    const articlesRef = collection(this.firestore, 'articles');
    return new Observable((observer) => {
      addDoc(articlesRef, {
        authorName: article.authorName,
        content: article.content,
        tags: article.tags.split(',').map((tag: string) => tag.trim()), // Convert tags to an array
        thumbnailUrl: article.thumbnailUrl,
        title: article.title,
        publishDate: Timestamp.fromDate(new Date()),
        isDraft: article.isDraft,
        isFeatured: article.isFeatured,
        likes: article.likes,
        views: article.views,
        categoryId: article.categoryId,
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
  likeComment(commentId: string): Promise<void> {
    const commentDocRef = doc(this.firestore, `comments/${commentId}`);
    return updateDoc(commentDocRef, { likes: increment(1) });
  }

  dislikeComment(commentId: string): Promise<void> {
    const commentDocRef = doc(this.firestore, `comments/${commentId}`);
    return updateDoc(commentDocRef, { dislikes: increment(1) });
  }
  getCategories(): Observable<Category[]> {
    const categoriesRef = collection(this.firestore, 'categories');
    return collectionData(categoriesRef, { idField: 'id' });
  }
  getCategoryById(categoryId: string): Observable<any> {
    const articleDoc = doc(this.firestore, 'categories', categoryId);
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
  getTags(): Observable<any[]> {
    const tagsRef = collection(this.firestore, 'tags');
    return collectionData(tagsRef, { idField: 'id' });
  }
  getAuthors(): Observable<MyUser[]> {
    const userRef = collection(this.firestore, 'users');
    const authorsQuery = query(userRef, where('isAuthor', '==', true));
    return collectionData(authorsQuery, { idField: 'id' });
  }
  getAuthorById(userId: string): Observable<MyUser> {
    const userRef = doc(this.firestore, 'users', userId);
    return docData(userRef, { idField: 'id' }) as Observable<MyUser>;
  }
  async addUser(user: MyUser): Promise<void> {
    try {
      console.log('adding a user', user);
      const userRef = collection(this.firestore, 'users');
      await addDoc(userRef, user); // Firestore will generate a unique document ID
      console.log('User added to Firestore:', user);
    } catch (error) {
      console.error('Error adding user to Firestore:', error);
    }
  }
}
