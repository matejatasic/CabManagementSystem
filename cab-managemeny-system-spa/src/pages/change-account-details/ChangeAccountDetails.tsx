import "./ChangeAccountDetails.scss"
import ContentCard from "../../common/content-card/ContentCard";

export default function ChangeAccountDetails() {
    return (
        <div id="change-account-details-background">
            <main className="container d-flex justify-content-center align-items-center">
                <ContentCard heading="Change Account Details">
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

                        <button className="btn btn-primary">Update</button>
                    </>
                </ContentCard>
            </main>
        </div>
    );
}