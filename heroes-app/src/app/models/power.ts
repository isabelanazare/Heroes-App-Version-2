export class Power {
    public id: number;
    public name: string;
    public strength: number = 1;
    public details: string;
    public element: string;
    public elementId: number;
    public mainTrait: string;

    constructor(id?: number, strength?: number) {
        this.id = id;
        this.strength = strength;
    }
}
