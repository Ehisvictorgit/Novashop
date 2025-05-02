export class ProductDto {
  id: string;
  name: string;
  price: number;
  image: string;
  description: string;
  category: string;

  constructor() {
    this.id = '';
    this.name = '';
    this.price = 0;
    this.image = '';
    this.description = '';
    this.category = '';
  }
}
