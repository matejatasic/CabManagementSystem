export default class Booking {
    public id: number;
    public fromAddress: string;
    public toAddress: string;
    public travelCost: number;

    constructor(
        id: number,
        fromAddress: string,
        toAddress: string,
        travelCost: number
    ) {
        this.id = id;
        this.fromAddress = fromAddress;
        this.toAddress = toAddress;
        this.travelCost = travelCost;
    }
}
