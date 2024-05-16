import { CarFrontFill } from "react-bootstrap-icons";

import CabCardProps from "./CabCardProps";

export default function CabCard(props: CabCardProps) {
    const { name, numberOfSeats } = props;

    return (
        <div className="card mb-3">
            <div className="row g-0">
                <div className="col-12">
                    <div className="card-body">
                        <CarFrontFill className="d-inline-block me-2 " />
                        <h5 className="card-title d-inline-block">{ name }</h5>
                        <p className="card-text">{ numberOfSeats }</p>
                    </div>
                </div>
            </div>
        </div>
    );
}
