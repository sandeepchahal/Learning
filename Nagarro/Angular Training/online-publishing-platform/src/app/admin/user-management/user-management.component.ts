import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { MyUser } from '../../models/user.model';
import { FirestoreService } from '../../services/firestore.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css',
})
export class UserManagementComponent {
  user: MyUser = {
    uid: '',
    bio: '',
    displayName: '',
    email: '',
    followers: [],
    following: [],
    isAuthor: false,
    isAdmin: false,
    photoUrl: '',
  };

  constructor(private firestoreService: FirestoreService) {}

  // Method to handle form submission and add the user to Firestore
  async onSubmit(): Promise<void> {
    try {
      // Add user using the service method
      await this.firestoreService.addUser(this.user);
      console.log('User added successfully!');
    } catch (error) {
      console.error('Error adding user:', error);
    }
  }
}
