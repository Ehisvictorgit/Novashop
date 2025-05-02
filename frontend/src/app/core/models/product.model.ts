export class Product {
  id: string;
  name: string;
  quality: number;
  expirationDate: string;
  price: number;
  description: string;
  currency: string;
  categoryId: string;
  available: boolean;
  image: string;

  constructor() {
    this.id = '';
    this.name = '';
    this.quality = 0;
    this.expirationDate = '';
    this.price = 0;
    this.description = '';
    this.currency = '';
    this.categoryId = '';
    this.available = false;
    this.image = '';
  }
}
