import { Injectable } from '@angular/core';
import { initializeApp } from 'firebase/app';
import {
  getAuth,
  signInWithPopup,
  GoogleAuthProvider,
  FacebookAuthProvider,
  signOut,
  User,
} from 'firebase/auth';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  firebaseApp = initializeApp(environment.firebase);
  private auth = getAuth(this.firebaseApp); // Use the initialized Firebase App's Auth instance

  async signInWithGoogle(): Promise<void> {
    try {
      const result = await signInWithPopup(this.auth, new GoogleAuthProvider());
      console.log('Google sign-in successful:', result);
    } catch (error) {
      console.error('Google sign-in error:', error);
    }
  }

  async signInWithFacebook(): Promise<void> {
    try {
      const result = await signInWithPopup(
        this.auth,
        new FacebookAuthProvider()
      );
      console.log('Facebook sign-in successful:', result);
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

  async getUser(): Promise<User | null> {
    return this.auth.currentUser;
  }
}
