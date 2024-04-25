import { Link } from "react-router-dom";

import "./Login.scss"
import ContentCard from "../../common/content-card/ContentCard";

export default function Login() {
    return (
        <div id="login-background">
            <main className="container d-flex justify-content-center align-items-center">
                <ContentCard heading="Login">
                    <>
                        <div className="mb-3">
                            <label className="form-label">Username</label>
                            <input type="text" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Password</label>
                            <input type="password" className="form-control" />
                        </div>

                        <div className="my-3">
                            <Link to="/register">Don't have an account? Click Here</Link>
                        </div>
                        <button className="btn btn-primary">Login</button>
                    </>
                </ContentCard>
            </main>
        </div>
    );
}