import {v4 as uuidv4} from 'uuid';

export interface IBasket {
    id: number;
    items: IBasketItem[];
}

export interface IBasketItem {
    id: number;
    foodName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
}

export class Basket implements IBasket {
     id = 1;
    items: IBasketItem[] = [];
}

export interface IBasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
}
