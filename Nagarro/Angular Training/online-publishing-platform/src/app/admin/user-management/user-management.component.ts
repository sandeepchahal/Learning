import { Component } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { MyUser } from '../../models/user.model';
import { FirestoreService } from '../../services/firestore.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

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

  constructor(
    private firestoreService: FirestoreService,
    private router: Router
  ) {}

  async onSubmit(): Promise<void> {
    try {
      await this.firestoreService.addUser(this.user);
      this.router.navigate(['/articles']);
    } catch (error) {
      console.error('Error adding user:', error);
    }
  }
}
