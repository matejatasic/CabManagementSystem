import { FormEvent, useState } from "react";

import { Link } from "react-router-dom";

import ContentCard from "../common/content-card/ContentCard";
import "./Register.scss"
import User from "../../../modules/user/models/User";
import ValidationError from "../../../modules/common/ValidationError";
import RegisterProps from "./RegisterProps";

export default function Register(props: RegisterProps) {
    const { repository, sessionRepository } = props;

    const [user, setUser] = useState<User>(new User());
    const [validationErrors, setValidationErrors] = useState<Record<string, string>>({});

    async function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();

        repository.register(user)
        .then(data => {
            sessionRepository.setUserSession(
                data.userId,
                data.username,
                data.token,
                data.role
            )
        })
        .catch(error => {
            console.error(error.message);
        });

    }

    function changeValue(propertyName: string, value: string) {
        try {
            let updatedUser: User;

            switch (propertyName) {
                case "email":
                    updatedUser = user.setEmail(value);
                    break;
                case "username":
                    updatedUser = user.setUsername(value);
                    break;
                case "password":
                    updatedUser = user.setPassword(value);
                    break;
                case "confirmPassword":
                    updatedUser = user.setConfirmPassword(value);
                    break;
                case "firstName":
                    updatedUser = user.setFirstName(value);
                    break;
                case "lastName":
                    updatedUser = user.setLastName(value);
                    break;
                case "address":
                    updatedUser = user.setAddress(value);
                    break;
                case "phone":
                    updatedUser = user.setPhone(value);
                    break;
                default:
                    throw new Error(`Property ${propertyName} does not exist on User`);
            }

            setUser(updatedUser);
            setValidationErrors({...validationErrors, [propertyName]: ""});
        }
        catch(error) {
            if (error instanceof ValidationError) {
                setValidationErrors({...validationErrors, [propertyName]: error.message});
            }
        }
    }
    function isRegisterButtonDisabled(): boolean {
        return Object.values(validationErrors)
            .some((error: string) => error.length > 0)
    }

    return (
        <div id="register-background">
            <main className="container d-flex justify-content-center align-items-center">
                <ContentCard heading="Register">
                    <form onSubmit={(e) => handleSubmit(e)}>
                        <div className="mb-3">
                            <label className="form-label">Email</label>
                            <input
                                type="email"
                                className="form-control"
                                onChange={(e) => changeValue("email", e.target.value)}
                            />
                            {
                                validationErrors["email"] ?
                                <p className="text-danger mt-2">{validationErrors["email"]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Username</label>
                            <input
                                type="text"
                                className="form-control"
                                onChange={(e) => changeValue("username", e.target.value)}
                            />
                            {
                                validationErrors["username"] ?
                                <p className="text-danger mt-2">{validationErrors["username"]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Password</label>
                            <input
                                type="password"
                                className="form-control"
                                onChange={(e) => changeValue("password", e.target.value)}
                            />
                            {
                                validationErrors["password"] ?
                                <p className="text-danger mt-2">{validationErrors["password"]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Confirm Password</label>
                            <input
                                type="password"
                                className="form-control"
                                onChange={(e) => changeValue("confirmPassword", e.target.value)}
                            />
                            {
                                validationErrors["confirmPassword"] ?
                                <p className="text-danger mt-2">{validationErrors["confirmPassword"]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3">
                            <label className="form-label">First Name</label>
                            <input
                                type="text"
                                className="form-control"
                                onChange={(e) => changeValue("firstName", e.target.value)}
                            />
                            {
                                validationErrors["firstName"] ?
                                <p className="text-danger mt-2">{validationErrors["firstName"]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Last Name</label>
                            <input
                                type="text"
                                className="form-control"
                                onChange={(e) => changeValue("username", e.target.value)}
                            />
                            {
                                validationErrors["lastName"] ?
                                <p className="text-danger mt-2">{validationErrors["lastName"]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Address</label>
                            <input
                                type="text"
                                className="form-control"
                                onChange={(e) => changeValue("address", e.target.value)}
                            />
                            {
                                validationErrors["address"] ?
                                <p className="text-danger mt-2">{validationErrors["address"]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Phone</label>
                            <input
                                type="text"
                                className="form-control"
                                onChange={(e) => changeValue("phone", e.target.value)}
                            />
                            {
                                validationErrors["phone"] ?
                                <p className="text-danger mt-2">{validationErrors["phone"]}</p>
                                : null
                            }
                        </div>

                        <div className="my-3">
                            <Link to="/login">Already have an account? Click Here</Link>
                        </div>
                        <button disabled={isRegisterButtonDisabled()} className="btn btn-primary">Register</button>
                    </form>
                </ContentCard>
            </main>
        </div>
    );
}