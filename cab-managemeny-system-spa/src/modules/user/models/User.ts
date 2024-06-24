import ValidationError from "../../common/ValidationError";

export default class User {
    public id: number;
    public username: string;
    public password: string;
    public email: string;
    public firstName: string;
    public lastName: string;
    public address: string;
    public phone: string;

    constructor(
        id: number = 0,
        username: string = "",
        password: string = "",
        email: string = "",
        firstName: string = "",
        lastName: string = "",
        address: string = "",
        phone: string = ""
    ) {
        this.id = id;
        this.username = username;
        this.password = password;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = address;
        this.phone = phone;
    }

    public setUsername(username: string): User {
        if (username.length < 3) {
            throw new ValidationError("The username needs to be at least 3 characters long");
        }

        this.username = username;

        return new User(this.id, this.username);
    }

    public setPassword(password: string): User {
        if (password.length < 8) {
            throw new ValidationError("The password needs to be at least 8 characters long");
        }

        const hasNumbers = /[0-9]/.test(password);
        const hasLowerLetters = /[a-z]/.test(password);
        const hasUpperLetters = /[A-Z]/.test(password);
        const hasSpecialCharacters = /[^0-9a-zA-Z]/.test(password);

        if(
            !hasNumbers
            || !hasLowerLetters
            || !hasUpperLetters
            || !hasSpecialCharacters
        ) {
            throw new ValidationError("The password needs have at least 1 lowercase letter, 1 uppercase letter and 1 special character");
        }

        this.password = password;

        return new User(this.id, this.username, this.password);
    }

    get fullName(): string {
        return `${this.firstName} ${this.lastName}`;
    }
}
