export interface Article {
  authorName: string;
  content: string;
  tags: string;
  thumbnailUrl: string;
  title: string;
  publishDate: Date;
  categoryId: string;
  isDraft: boolean;
  isFeatured: boolean;
  likes: number;
  views: number;
}
