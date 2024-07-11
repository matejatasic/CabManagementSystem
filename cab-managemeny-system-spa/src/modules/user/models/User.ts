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

    public static USERNAME = "username";
    public static PASSWORD = "password";
    public static CONFIRM_PASSWORD = "confirmPassword";
    public static EMAIL = "email";
    public static FIRST_NAME = "firstName";
    public static LAST_NAME = "lastName";
    public static ADDRESS = "address";
    public static PHONE = "phone";

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

    public setUsername(value: string): User {
        if (value.length === 0) {
            throw new ValidationError("Username is required");
        }

        if (value.length < 3) {
            throw new ValidationError("The username needs to be at least 3 characters long");
        }

        this.username = value;

        return new User(...this.getParameters() as any);
    }

    public setPassword(value: string): User {
        if (value.length === 0) {
            throw new ValidationError("Password is required");
        }

        if (value.length < 8) {
            throw new ValidationError("The password needs to be at least 8 characters long");
        }

        const hasNumbers = /[0-9]/.test(value);
        const hasLowerLetters = /[a-z]/.test(value);
        const hasUpperLetters = /[A-Z]/.test(value);
        const hasSpecialCharacters = /[^0-9a-zA-Z]/.test(value);

        if(
            !hasNumbers
            || !hasLowerLetters
            || !hasUpperLetters
            || !hasSpecialCharacters
        ) {
            throw new ValidationError("The password needs have at least 1 lowercase letter, 1 uppercase letter and 1 special character");
        }

        this.password = value;

        return new User(...this.getParameters() as any);
    }

    public setEmail(value: string): User {
        if (value.length === 0) {
            throw new ValidationError("Email is required");
        }

        const isValidEmail =
            /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value);

        if (!isValidEmail) {
            throw new ValidationError("Email not valid");
        }

        this.email = value;

        return new User(...this.getParameters() as any);
    }

    public setConfirmPassword(value: string): User {
        if (value !== this.password) {
            throw new ValidationError("Password and confirm password do not match");
        }

        return new User(...this.getParameters() as any);
    }

    public setFirstName(value: string): User {
        const isValidFirstName = /^[a-z ,.'-]+$/.test(value);

        if (!isValidFirstName) {
            throw new ValidationError("Not a valid first name");
        }

        this.firstName = value;

        return new User(...this.getParameters() as any);
    }

    public setLastName(value: string): User {
        const isValidLastName = /^[a-z ,.'-]+$/.test(value);

        if (!isValidLastName) {
            throw new ValidationError("Not a valid last name");
        }

        this.lastName = value;

        return new User(...this.getParameters() as any);
    }

    public setAddress(value: string): User {
        if (value.length === 0) {
            throw new ValidationError("Address is required");
        }

        this.address = value;

        return new User(...this.getParameters() as any);
    }

    public setPhone(value: string): User {
        const isValidPhoneNumber =
            /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/.test(value);

        if (!isValidPhoneNumber) {
            throw new ValidationError("Not a valid phone number");
        }

        this.phone = value;

        return new User(...this.getParameters() as any);
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
            username: this.username,
            password: this.password,
            email: this.email,
            firstName: this.firstName,
            lastName: this.lastName,
            address: this.address,
            phone: this.phone
        };
    }

    get fullName(): string {
        return `${this.firstName} ${this.lastName}`;
    }

    public validate(): ValidationError | void {
        if (this.email.length === 0) {
            throw new ValidationError("Email is required", User.EMAIL);
        }
        else if (this.username.length === 0) {
            throw new ValidationError("Username is required", User.USERNAME);
        }
        else if (this.password.length === 0) {
            throw new ValidationError("Password is required", User.PASSWORD);
        }
        else if(this.address.length === 0) {
            throw new ValidationError("Address is required", User.ADDRESS);
        }
    }

    public loginValidate(): ValidationError | void {
        if (this.username.length === 0) {
            throw new ValidationError("Username is required", User.USERNAME);
        }
        else if (this.password.length === 0) {
            throw new ValidationError("Password is required", User.PASSWORD);
        }
    }
}
