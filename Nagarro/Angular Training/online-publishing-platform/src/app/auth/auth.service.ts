import { Injectable } from '@angular/core';
import { initializeApp } from 'firebase/app';
import {
  getAuth,
  signInWithPopup,
  GoogleAuthProvider,
  FacebookAuthProvider,
  signOut,
  User,
  onAuthStateChanged,
} from 'firebase/auth';
import { getFirestore, doc, setDoc, getDoc } from 'firebase/firestore';
import { environment } from '../../environments/environment';
import { MyUser } from '../models/user.model';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  firebaseApp = initializeApp(environment.firebase);
  private auth = getAuth(this.firebaseApp); // Use the initialized Firebase App's Auth instance
  private db = getFirestore(this.firebaseApp); // Firestore instance
  private userSubject = new BehaviorSubject<MyUser | null>(null); // Track MyUser state

  constructor() {
    // Listen to authentication state changes and update the user state
    onAuthStateChanged(this.auth, async (user) => {
      if (user) {
        // Fetch the user data from Firestore or another source
        const userData = await this.getUserDataFromFirestore(user.uid);
        this.userSubject.next(userData); // Emit MyUser data
      } else {
        this.userSubject.next(null); // Set user to null when logged out
      }
    });
  }
  // Fetch user data from Firestore or your database
  private async getUserDataFromFirestore(uid: string): Promise<MyUser | null> {
    try {
      const userDocRef = doc(this.db, 'users', uid); // Firestore reference
      const userDoc = await getDoc(userDocRef);

      if (userDoc.exists()) {
        return userDoc.data() as MyUser; // Return the user data cast to MyUser
      } else {
        console.error('User document not found in Firestore');
        return null;
      }
    } catch (error) {
      console.error('Error fetching user from Firestore:', error);
      return null;
    }
  }

  async signInWithGoogle(): Promise<void> {
    try {
      const result = await signInWithPopup(this.auth, new GoogleAuthProvider());
      console.log('Google sign-in successful:', result);
      // Prepare the MyUser object based on the FirebaseUser
      await this.saveUserToDb(result.user); // Save user to Firestore
    } catch (error) {
      console.error('Google sign-in error:', error);
    }
  }
  get authStateChanged() {
    return this.userSubject.asObservable();
  }
  async signInWithFacebook(): Promise<void> {
    try {
      const result = await signInWithPopup(
        this.auth,
        new FacebookAuthProvider()
      );
      console.log('Facebook sign-in successful:', result);
      await this.saveUserToDb(result.user); // Save user to Firestore
    } catch (error) {
      console.error('Facebook sign-in error:', error);
    }
  }

  async signOut(): Promise<void> {
    try {
      await signOut(this.auth);
      console.log('Sign-out successful');
    } catch (error) {
      console.error('Sign-out error:', error);
    }
  }

  async isAuthenticated(): Promise<boolean> {
    const user = this.auth.currentUser;
    return !!user;
  }

  async getUser(): Promise<MyUser | null> {
    try {
      const currentUser = this.auth.currentUser;

      if (!currentUser || !currentUser.uid) {
        console.error('No authenticated user found');
        return null;
      }

      const userDocRef = doc(this.db, 'users', currentUser.uid);
      const userDoc = await getDoc(userDocRef);

      if (userDoc.exists()) {
        const userData = userDoc.data() as MyUser; // Cast Firestore data to MyUser
        console.log('User data retrieved from Firestore:', userData);
        return userData; // Return the user document data as MyUser
      } else {
        console.error('User document not found in Firestore');
        return null;
      }
    } catch (error) {
      console.error('Error fetching user from Firestore:', error);
      return null;
    }
  }

  async saveUserToDb(user: User): Promise<void> {
    if (!user || !user.uid) {
      console.error('Invalid user data');
      return;
    }
    console.log('User logged in successfully', user);
    const usr: MyUser = {
      uid: user.uid,
      bio: '', // You can fetch this from Firestore or set a default value
      displayName: user.displayName || 'Unknown',
      email: user.email || 'No email',
      followers: [], // Default empty array, can be updated later
      following: [], // Default empty array, can be updated later
      isAuthor: true, // Set default value, can be updated later
      isAdmin: true, // Set default value, can be updated later
      photoUrl: user.photoURL || '', // Default photo URL
    };

    const userDocRef = doc(this.db, 'users', usr.uid); // Reference to the user document
    const userDoc = await getDoc(userDocRef);
    console.log('User Doc', userDoc);
    if (!userDoc.exists()) {
      console.log('User not exists');
      // If user does not exist, save to Firestore
      await setDoc(userDocRef, {
        ...usr, // Spread the user object directly into Firestore
        createdAt: new Date().toISOString(), // Add createdAt field dynamically
      });
      console.log('User added to Firestore');
    } else {
      console.log('User already exists in Firestore');
    }
  }
}
