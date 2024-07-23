import { useState } from "react";

import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";

import "./Login.scss"
import ContentCard from "../common/components/content-card/ContentCard";
import User from "../../../modules/user/models/User";
import LoginProps from "./LoginProps";
import { login } from "../../common/store/slices/user.slice";
import CustomerRoutesEnum from "../common/enums/CustomerRoutesEnum";

export default function Login(props: LoginProps) {
    const { repository } = props;
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [user, setUser] = useState<User>(new User());
    const [validationErrors, setValidationErrors] = useState<Record<string, string>>({});

    async function handleSubmit() {
        repository.login(user)
        .then(data => {
            dispatch(login({
                userId: data.userId,
                username: data.username,
                token: data.token,
                role: data.role
            }));
            navigate("/");
        })
        .catch(error => {
            let fieldName = "general";

            if (error.fieldName) {
                fieldName = error.fieldName;
            }

            setValidationErrors({...validationErrors, [fieldName]: error.message});
        });

    }

    function changeUsername(value: string) {
        try{
            setUser(user.setUsername(value))
            setValidationErrors({...validationErrors, [User.USERNAME]: ""})
        }
        catch(error) {
        }
    }

    function changePassword(value: string) {
        try{
            setUser(user.setPassword(value));
            setValidationErrors({...validationErrors, [User.PASSWORD]: ""})
        }
        catch(error) {
        }
    }

    return (
        <div id="login-background">
            <main className="container d-flex justify-content-center align-items-center">
                <ContentCard heading="Login">
                    <>
                        <div className="mb-3" data-testid="username">
                            <label className="form-label">Username</label>
                            <input
                                type="text"
                                className="form-control"
                                onChange={(e) => changeUsername(e.target.value)}
                            />
                            {
                                validationErrors[User.USERNAME] ?
                                <p className="error text-danger mt-2">{validationErrors[User.USERNAME]}</p>
                                : null
                            }
                        </div>
                        <div className="mb-3" data-testid="password">
                            <label className="form-label">Password</label>
                            <input
                                type="password"
                                className="form-control"
                                onChange={(e) => changePassword(e.target.value)}
                            />
                            {
                                validationErrors[User.PASSWORD] ?
                                <p className="error text-danger mt-2">{validationErrors[User.PASSWORD]}</p>
                                : null
                            }
                        </div>
                        {
                            validationErrors["general"] ?
                            <p data-testid="general-error" className="error text-danger mt-4">{validationErrors["general"]}</p>
                            : null
                        }
                        <div className="my-3">
                            <Link to={CustomerRoutesEnum.Register}>Don't have an account? Click Here</Link>
                        </div>
                        <button data-testid="submit-btn" className="btn btn-primary" onClick={() => handleSubmit()}>Login</button>
                    </>
                </ContentCard>
            </main>
        </div>
    );
}