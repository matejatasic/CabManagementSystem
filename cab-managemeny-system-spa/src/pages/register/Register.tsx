import { Link } from "react-router-dom";
import ContentCard from "../../common/content-card/ContentCard";
import "./Register.scss"

export default function Register() {
    return (
        <div id="register-background">
            <main className="container d-flex justify-content-center align-items-center">
                <ContentCard heading="Register">
                    <>
                        <div className="mb-3">
                            <label className="form-label">Email</label>
                            <input type="email" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Username</label>
                            <input type="text" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Password</label>
                            <input type="password" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Confirm Password</label>
                            <input type="password" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">First Name</label>
                            <input type="text" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Last Name</label>
                            <input type="text" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Address</label>
                            <input type="text" className="form-control" />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Phone</label>
                            <input type="text" className="form-control" />
                        </div>

                        <div className="my-3">
                            <Link to="/login">Already have an account? Click Here</Link>
                        </div>
                        <button className="btn btn-primary">Register</button>
                    </>
                </ContentCard>
            </main>
        </div>
    );
}