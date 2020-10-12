export class EntityTypeDto {
    public id: number;
    public name: string;
    public isBadGuy: boolean;
    public latitude: number;
    public longitude: number;

    constructor(id, isBadGuy, name) {
        this.id = id;
        this.isBadGuy = isBadGuy;
        this.name = name;
        this.latitude = 0;
        this.longitude = 0;
    }
}
