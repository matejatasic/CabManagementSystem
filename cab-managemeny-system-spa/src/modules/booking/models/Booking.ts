import ValidationError from "../../common/ValidationError";

export default class Booking {
    public id: number;
    public fromAddress: string;
    public toAddress: string;
    public travelCost: number;
    public travelerId: number;
    public driverId: number;

    public static FROM_ADDRESS = "fromAddress";
    public static TO_ADDRESS = "toAddress";
    public static TRAVEL_COST = "travelCost";
    public static TRAVELER_ID = "travelerId";
    public static DRIVER_ID = "driverId";

    constructor(
        id: number = 0,
        fromAddress: string = "",
        toAddress: string = "",
        travelCost: number = 0,
        travelerId: number = 0,
        driverId: number = 0
    ) {
        this.id = id;
        this.fromAddress = fromAddress;
        this.toAddress = toAddress;
        this.travelCost = travelCost;
        this.travelerId = travelerId;
        this.driverId = driverId
    }

    public setFromAddress(value: string): Booking {
        if (value.length === 0) {
            throw new ValidationError("From Address cannot be empty");
        }

        this.fromAddress = value;

        return new Booking(...this.getParameters() as any);
    }

    public setToAddress(value: string): Booking {
        if (value.length === 0) {
            throw new ValidationError("To Address cannot be empty");
        }

        this.fromAddress = value;

        return new Booking(...this.getParameters() as any);
    }

    public setTravelerId(value: number) {
        this.travelerId = value;

        return new Booking(...this.getParameters() as any);
    }

    public setDriverId(value: number) {
        this.driverId = value;

        return new Booking(...this.getParameters() as any);
    }

    public getParameters(): Array<string | number> {
        const instanceParameters = this.getInstance();
        const parameters = [];

        for (let propertyName of Object.getOwnPropertyNames(instanceParameters)) {
            parameters.push(instanceParameters[propertyName]);
        }

        return parameters;
    }

    public getInstance(): Record<string, string | number> {
        return {
            id: this.id,
            fromAddress: this.fromAddress,
            toAddress: this.toAddress,
            travelCost: this.travelCost,
            travelerId: this.travelerId,
            driverId: this.driverId
        };
    }

    public validate(): ValidationError | void {
        throw new ValidationError("Some general error");
        if (this.fromAddress.length === 0) {
            throw new ValidationError("From Address cannot be empty", Booking.FROM_ADDRESS);
        }
        else if (this.toAddress.length === 0) {
            throw new ValidationError("To Address cannot be empty", Booking.TO_ADDRESS);
        }
        else if (this.travelerId < 1) {
            throw new ValidationError("Traveler id must be set", Booking.TRAVELER_ID);
        }
        else {
            throw new ValidationError("Driver id must be set", Booking.DRIVER_ID);
        }
    }
}
