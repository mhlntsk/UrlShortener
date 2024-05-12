export interface Url {
    id: number | undefined;
    fullUrl: string;
    shortUrl: string;
    createdDate: Date;
    lastAppeal: Date | undefined;
    numberOfAppeals: number;
    userId: number;
  }