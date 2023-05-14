import { GetItem } from "./item";

export interface AllOrder {
    id: number,
    item: GetItem,
    comment: string,
    address: string,
    creationTime: Date,
    deliveryTime: Date,
    price: number,
    status: string
}