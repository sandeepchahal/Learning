import { Injectable } from '@angular/core';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private users: User[] = [
    new User('john_doe', 'john@example.com', 'password123'),
    new User('jane_doe', 'jane@example.com', 'password456'),
    new User('alice_smith', 'alice@example.com', 'password789'),
  ]; // Hardcoded users for login testing

  constructor() {}

  // Method to sign up a new user
  signUp(newUser: User): boolean {
    // Check if the email already exists
    const existingUser = this.users.find(
      (user) => user.email === newUser.email
    );
    if (existingUser) {
      return false; // Email is already in use
    }

    // Add the new user to the list
    this.users.push(newUser);
    return true;
  }

  // Method to log in an existing user
  login(email: string, password: string): boolean {
    const user = this.users.find(
      (user) => user.email === email && user.password === password
    );
    return user !== undefined; // Return true if user found, false otherwise
  }

  // Method to get all users (for testing purposes or showing users)
  getUsers(): User[] {
    return this.users;
  }
}
