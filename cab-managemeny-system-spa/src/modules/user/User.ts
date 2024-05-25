export default class User {
    public id: number;
    public username: string;
    public email: string;
    public firstName: string;
    public lastName: string;
    public address: string;
    public phone: string;

    constructor(
        id: number,
        username: string,
        email: string,
        firstName: string,
        lastName: string,
        address: string,
        phone: string
    ) {
        this.id = id;
        this.username = username;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = address;
        this.phone = phone;
    }

    get fullName(): string {
        return `${this.firstName} ${this.lastName}`;
    }
}
