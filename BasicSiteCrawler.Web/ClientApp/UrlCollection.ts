import { IUrl } from "./Components/Interfaces";

export class UrlCollection {
    constructor() {
        this._items = [] as Array<IUrl>;
    }
    private _items: Array<IUrl>;

    get items(): Array<IUrl> {
        return this._items;
    }
    
    addItemToStart(url: string) {
        const id = Date.now().toString();
        const urlObj = { url: url, id: id } as IUrl;
        this._items = [urlObj, ...this._items];
    }

}