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
  startAfter,
  endBefore,
  QueryDocumentSnapshot,
  DocumentData,
  getDocs,
  limitToLast,
} from '@angular/fire/firestore';
import { from, map, Observable, tap } from 'rxjs';
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
  getArticles(): Observable<Article[]> {
    const articlesRef = collection(this.firestore, 'articles');
    const articlesQuery = query(
      articlesRef,
      where('isDraft', '==', false),
      limit(10)
    );

    return collectionData<Article>(articlesQuery, { idField: 'id' }).pipe(
      map((articles: Article[]) =>
        articles.map((article) => ({
          ...article,
          publishDate: article.publishDate?.toDate().toLocaleDateString(), // Convert Timestamp to readable format
        }))
      ),
      tap((articles) => console.log('Fetched articles:', articles)) // Debug log
    );
  }

  getPaginationArticles(
    articlesPerPage: number,
    direction: 'next' | 'prev',
    lastDocument: QueryDocumentSnapshot<DocumentData> | null,
    firstDocument: QueryDocumentSnapshot<DocumentData> | null
  ): Observable<{
    articles: Article[];
    lastDoc: QueryDocumentSnapshot<DocumentData> | null;
    firstDoc: QueryDocumentSnapshot<DocumentData> | null;
  }> {
    const articlesRef = collection(this.firestore, 'articles');
    const orderByField = 'publishDate';

    // Build the query based on direction (next/prev)
    let articlesQuery;
    if (direction === 'next' && lastDocument) {
      // For 'next' direction, start after the last document
      articlesQuery = query(
        articlesRef,
        orderBy(orderByField),
        startAfter(lastDocument),
        limit(articlesPerPage)
      );
    } else if (direction === 'prev' && firstDocument) {
      // For 'prev' direction, end before the first document
      articlesQuery = query(
        articlesRef,
        orderBy(orderByField),
        endBefore(firstDocument),
        limitToLast(articlesPerPage) // Use limitToLast for reversing the order
      );
    } else {
      // Initial load or fallback to default query
      articlesQuery = query(
        articlesRef,
        orderBy(orderByField),
        limit(articlesPerPage)
      );
    }

    // Fetch the data as a snapshot
    return from(getDocs(articlesQuery)).pipe(
      map((snapshot) => {
        const articles: Article[] = snapshot.docs.map((doc) => {
          const data = doc.data();
          return {
            id: doc.id,
            authorName: data['authorName'] || '',
            content: data['content'] || '',
            tags: data['tags'] || [],
            thumbnailUrl: data['thumbnailUrl'] || '',
            title: data['title'] || '',
            isDraft: data['isDraft'] || false,
            publishDate:
              data['publishDate']?.toDate().toLocaleDateString() || '',
          } as Article;
        });

        console.log(articles);

        // Extract Firestore QueryDocumentSnapshot objects for pagination
        const lastDoc =
          snapshot.size > 0 ? snapshot.docs[snapshot.docs.length - 1] : null;
        const firstDoc = snapshot.size > 0 ? snapshot.docs[0] : null;

        return {
          articles,
          lastDoc,
          firstDoc,
        };
      })
    );
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
