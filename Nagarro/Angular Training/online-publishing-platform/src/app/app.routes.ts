import { Routes } from '@angular/router';
import { DetailsComponent } from './articles/details/details.component';
import { ListComponent } from './articles/list/list.component';
import { LoginComponent } from './auth/login/login.component';
import { CommentComponent } from './comments/comment/comment.component';
import { EditorComponent } from './articles/editor/editor.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { AuthorDetailsComponent } from './authors/details/author-details.component';

export const appRoutes: Routes = [
  { path: 'admin/manage/user', component: UserManagementComponent },
  { path: 'articles/add', component: EditorComponent },
  { path: 'articles', component: ListComponent },
  { path: 'articles/:id', component: DetailsComponent },
  { path: 'articles/:id/comments', component: CommentComponent },
  { path: 'author/:id', component: AuthorDetailsComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: '', redirectTo: 'articles' },
  { path: '**', redirectTo: 'articles' },
];
