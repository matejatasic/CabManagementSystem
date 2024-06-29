export default class RegisterViewModel {
    public username: string;
    public password: string;
    public email: string;
    public firstName: string;
    public lastName: string;
    public address: string;
    public phone: string;

    constructor(
        username: string,
        password: string,
        email: string,
        firstName: string,
        lastName: string,
        address: string,
        phone: string
    ) {
        this.username = username;
        this.password = password;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = address;
        this.phone = phone;
    }
}