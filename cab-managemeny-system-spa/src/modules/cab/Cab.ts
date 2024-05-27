export default class Cab {
    public id: number;
    public name: string;
    public numberOfSeats: number;
    public fuelType: string;
    public registeredUntil: string;
    public registrationPlates: string;
    public driverId: number;

    constructor(
        id: number,
        name: string,
        numberOfSeats: number,
        fuelType: string,
        registeredUntil: string,
        registrationPlates: string,
        driverId: number
    ) {
        this.id = id;
        this.name = name;
        this.numberOfSeats = numberOfSeats;
        this.fuelType = fuelType;
        this.registeredUntil = registeredUntil;
        this.registrationPlates = registrationPlates;
        this.driverId = driverId;
    }
}
