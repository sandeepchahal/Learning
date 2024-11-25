import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FirestoreService } from '../../services/firestore.service';
import { MyUser } from '../../models/user.model';

@Component({
  selector: 'author-details',
  standalone: true,
  imports: [],
  templateUrl: './author-details.component.html',
  styleUrl: './author-details.component.css',
})
export class AuthorDetailsComponent {
  user = {
    uid: '',
    bio: '',
    displayName: '',
    email: '',
    followers: [],
    following: [],
    photoUrl: '',
  };

  constructor(
    private route: ActivatedRoute,
    private firestoreService: FirestoreService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id')!;

    this.firestoreService.getAuthorById(id).subscribe((userData: MyUser) => {
      this.user = userData;
    });
  }
}
