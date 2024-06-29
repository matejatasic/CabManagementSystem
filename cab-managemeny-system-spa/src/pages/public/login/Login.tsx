import { useState } from "react";
import { Link } from "react-router-dom";

import "./Login.scss"
import ContentCard from "../common/content-card/ContentCard";
import User from "../../../modules/user/models/User";
import LoginProps from "./LoginProps";

export default function Login(props: LoginProps) {
    const { repository } = props;

    const [user, setUser] = useState<User>(new User());

    async function handleSubmit() {
        repository.login(user)
        .then(data => {
            console.log(data);
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
    console.log(user);

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