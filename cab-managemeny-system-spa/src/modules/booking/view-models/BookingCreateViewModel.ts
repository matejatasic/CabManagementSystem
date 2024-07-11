export default class BookingCreateViewModel {
    private fromAddress: string;
    private toAddress: string;
    private travelerId: number;
    private driverId: number;

    constructor(
        fromAddress: string,
        toAddress: string,
        travelerId: number,
        driverId: number
    ) {
        this.fromAddress = fromAddress;
        this.toAddress = toAddress;
        this.travelerId = travelerId;
        this.driverId = driverId;
    }

    // public getFromAddress(): string {
    //     return this.fromAddress;
    // }

    // public getToAddress(): string {
    //     return this.toAddress;
    // }

    // public getTravelerId(): number {
    //     return this.travelerId;
    // }

    // public getDriverId(): number {
    //     return this.driverId;
    // }
}