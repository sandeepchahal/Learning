import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';
import { CommentComponent } from './comment/comment.component';
import { InterestComponent } from './interest/interest.component';
import { SearchFilterComponent } from './search-filter/search-filter.component';
import { RouterModule, RouterOutlet } from '@angular/router';

@NgModule({
  declarations: [
    HeaderComponent,
    CommentComponent,
    InterestComponent,
    SearchFilterComponent,
  ],
  exports: [HeaderComponent, CommentComponent],
  imports: [CommonModule, FormsModule, RouterModule, RouterOutlet],
})
export class SharedModule {}
