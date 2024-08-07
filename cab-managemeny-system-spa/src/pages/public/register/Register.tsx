import { FormEvent, useState } from "react";

import { Link } from "react-router-dom";
import { useDispatch } from "react-redux";

import ContentCard from "../common/components/content-card/ContentCard";
import "./Register.scss"
import User from "../../../modules/user/models/User";
import ValidationError from "../../../modules/common/ValidationError";
import RegisterProps from "./RegisterProps";
import { login } from "../../common/store/slices/user.slice";
import CustomerRoutesEnum from "../common/enums/CustomerRoutesEnum";

export default function Register(props: RegisterProps) {
    const { repository } = props;
    const dispatch = useDispatch();

    const [user, setUser] = useState<User>(new User());
    const [validationErrors, setValidationErrors] = useState<Record<string, string>>({});

    async function handleSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault();

        repository.register(user)
        .then(data => {
            dispatch(login({
                userId: data.userId,
                username: data.username,
                token: data.token,
                role: data.role
            }));
        })
        .catch(error => {
            if (error instanceof ValidationError) {
                let fieldName = "general";

                if (error.fieldName) {
                    fieldName = error.fieldName;
                }

                setValidationErrors({...validationErrors, [fieldName]: error.message});
            }
        });

    }

    function changeValue(propertyName: string, value: string) {
        try {
            let updatedUser: User;

            switch (propertyName) {
                case User.EMAIL:
                    updatedUser = user.setEmail(value);
                    break;
                case User.USERNAME:
                    updatedUser = user.setUsername(value);
                    break;
                case User.PASSWORD:
                    updatedUser = user.setPassword(value);
                    break;
                case User.CONFIRM_PASSWORD:
                    updatedUser = user.setConfirmPassword(value);
                    break;
                case User.FIRST_NAME:
                    updatedUser = user.setFirstName(value);
                    break;
                case User.LAST_NAME:
                    updatedUser = user.setLastName(value);
                    break;
                case User.ADDRESS:
                    updatedUser = user.setAddress(value);
                    break;
                case User.PHONE:
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
                        <div className="mb-3" data-testid="email">
                            <label className="form-label" htmlFor="email">Email</label>
                            <input
                                type="email"
                                className="form-control"
                                id="email"
                                onChange={(e) => changeValue(User.EMAIL, e.target.value)}
                            />
                            {
                                validationErrors[User.EMAIL] ?
                                <p className="error text-danger mt-2">{validationErrors[User.EMAIL]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="username">
                            <label className="form-label" htmlFor="username">Username</label>
                            <input
                                type="text"
                                className="form-control"
                                id="username"
                                onChange={(e) => changeValue(User.USERNAME, e.target.value)}
                            />
                            {
                                validationErrors[User.USERNAME] ?
                                <p className="error text-danger mt-2">{validationErrors[User.USERNAME]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="password">
                            <label className="form-label" htmlFor="password">Password</label>
                            <input
                                type="password"
                                className="form-control"
                                id="password"
                                onChange={(e) => changeValue(User.PASSWORD, e.target.value)}
                            />
                            {
                                validationErrors[User.PASSWORD] ?
                                <p className="error text-danger mt-2">{validationErrors[User.PASSWORD]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="confirm-password">
                            <label className="form-label" htmlFor="confirm-password">Confirm Password</label>
                            <input
                                type="password"
                                className="form-control"
                                id="confirm-password"
                                onChange={(e) => changeValue(User.CONFIRM_PASSWORD, e.target.value)}
                            />
                            {
                                validationErrors[User.CONFIRM_PASSWORD] ?
                                <p className="error text-danger mt-2">{validationErrors[User.CONFIRM_PASSWORD]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="first-name">
                            <label className="form-label" htmlFor="first-name">First Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="first-name"
                                onChange={(e) => changeValue(User.FIRST_NAME, e.target.value)}
                            />
                            {
                                validationErrors[User.FIRST_NAME] ?
                                <p className="error text-danger mt-2">{validationErrors[User.FIRST_NAME]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="last-name">
                            <label className="form-label" htmlFor="last-name">Last Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="last-name"
                                onChange={(e) => changeValue(User.LAST_NAME, e.target.value)}
                            />
                            {
                                validationErrors[User.LAST_NAME] ?
                                <p className="error text-danger mt-2">{validationErrors[User.LAST_NAME]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="address">
                            <label className="form-label" htmlFor="address">Address</label>
                            <input
                                type="text"
                                className="form-control"
                                id="address"
                                onChange={(e) => changeValue(User.ADDRESS, e.target.value)}
                            />
                            {
                                validationErrors[User.ADDRESS] ?
                                <p className="error text-danger mt-2">{validationErrors[User.ADDRESS]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="phone">
                            <label className="form-label" htmlFor="phone">Phone</label>
                            <input
                                type="text"
                                className="form-control"
                                id="phone"
                                onChange={(e) => changeValue(User.PHONE, e.target.value)}
                            />
                            {
                                validationErrors[User.PHONE] ?
                                <p className="error text-danger mt-2">{validationErrors[User.PHONE]}</p>
                                : null
                            }
                        </div>
                        {
                            validationErrors["general"] ?
                            <p data-testid="general-error" className="error text-danger mt-4">{validationErrors["general"]}</p>
                            : null
                        }
                        <div className="my-3">
                            <Link to={CustomerRoutesEnum.Login}>Already have an account? Click Here</Link>
                        </div>
                        <button
                            disabled={isRegisterButtonDisabled()}
                            className="btn btn-primary"
                            data-testid="submit-btn"
                        >Register</button>
                    </form>
                </ContentCard>
            </main>
        </div>
    );
}