import { Timestamp } from 'firebase/firestore';

export interface Article {
  id: string;
  authorName: string;
  content: string;
  tags: string;
  thumbnailUrl: string;
  title: string;
  publishDate: Timestamp;
  categoryId: string;
  isDraft: boolean;
  isFeatured: boolean;
  likes: number;
  views: number;
}
