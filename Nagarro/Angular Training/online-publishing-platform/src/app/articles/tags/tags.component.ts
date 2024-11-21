import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FirestoreService } from '../../services/firestore.service';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css'],
})
export class TagsComponent implements OnInit {
  tags: any[] = [];
  @Output() tagSelected = new EventEmitter<any>();

  constructor(private firestoreService: FirestoreService) {}

  ngOnInit(): void {
    this.loadTags();
  }

  loadTags(): void {
    this.firestoreService.getTags().subscribe((tags) => {
      this.tags = tags;
    });
  }

  filterByTag(tag: any): void {
    this.tagSelected.emit(tag);
  }
}
