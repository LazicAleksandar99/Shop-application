import { ActiveItem, GetItem, HistoryItem } from "./item";

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

export interface OrderHistory {
    id: number,
    item: HistoryItem,
    comment: string,
    address: string,
    creationTime: Date,
    deliveryTime: Date,
    price: number,
    status: string
}

export interface ActiveOrder {
    id: number,
    item: ActiveItem,
    comment: string,
    address: string,
    creationTime: Date,
    deliveryTime: Date,
    price: number,
    status: string
}