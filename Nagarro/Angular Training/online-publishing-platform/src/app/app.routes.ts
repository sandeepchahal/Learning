import { Routes } from '@angular/router';
import { DetailsComponent } from './articles/details/details.component';
import { ListComponent } from './articles/list/list.component';
import { LoginComponent } from './auth/login/login.component';
import { CommentComponent } from './comments/comment/comment.component';
import { EditorComponent } from './articles/editor/editor.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';

export const appRoutes: Routes = [
  { path: 'admin/manage/user', component: UserManagementComponent },
  { path: 'auth/login', component: LoginComponent },
  { path: 'articles/add', component: EditorComponent },
  { path: 'articles', component: ListComponent },
  { path: 'articles/:id', component: DetailsComponent },
  { path: 'articles/:id/comments', component: CommentComponent },
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
  { path: '**', redirectTo: 'auth/login' },
];
