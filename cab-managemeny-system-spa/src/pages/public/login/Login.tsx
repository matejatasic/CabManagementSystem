import { useState } from "react";

import { useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";

import "./Login.scss"
import ContentCard from "../common/content-card/ContentCard";
import User from "../../../modules/user/models/User";
import LoginProps from "./LoginProps";
import { login } from "../../common/store/slices/user.slice";

export default function Login(props: LoginProps) {
    const { repository } = props;
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const [user, setUser] = useState<User>(new User());

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
            console.error(error.message);
        });

    }

    function changeUsername(value: string) {
        try{
            setUser(user.setUsername(value))
        }
        catch(error) {
        }
    }

    function changePassword(value: string) {
        try{
            setUser(user.setPassword(value));
        }
        catch(error) {
        }
    }

    return (
        <div id="login-background">
            <main className="container d-flex justify-content-center align-items-center">
                <ContentCard heading="Login">
                    <>
                        <div className="mb-3">
                            <label className="form-label">Username</label>
                            <input
                                type="text"
                                className="form-control"
                                onChange={(e) => changeUsername(e.target.value)}
                            />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Password</label>
                            <input
                                type="password"
                                className="form-control"
                                onChange={(e) => changePassword(e.target.value)}
                            />
                        </div>

                        <div className="my-3">
                            <Link to="/register">Don't have an account? Click Here</Link>
                        </div>
                        <button className="btn btn-primary" onClick={() => handleSubmit()}>Login</button>
                    </>
                </ContentCard>
            </main>
        </div>
    );
}