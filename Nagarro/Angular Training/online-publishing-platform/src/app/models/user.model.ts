export interface MyUser {
  uid: string;
  bio: string;
  displayName: string;
  email: string;
  followers: [];
  following: [];
  isAuthor: boolean;
  isAdmin: boolean;
  photoUrl: string;
}
