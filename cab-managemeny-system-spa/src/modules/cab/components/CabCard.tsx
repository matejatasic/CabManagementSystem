import { CarFrontFill } from "react-bootstrap-icons";

import CabCardProps from "./CabCardProps";
import { useState } from "react";

export default function CabCard(props: CabCardProps) {
    const { name, numberOfSeats, driverId, onClick } = props;
    const [isHoveredOver, setIsHoveredOver] = useState<boolean>();

    const handleOnMouseEnter = () => {
        setIsHoveredOver(true);
    }

    const handleOnMouseLeave = () => {
        setIsHoveredOver(false);
    }

    const divStyle = {
        boxShadow: isHoveredOver ? "5px 5px 20px 0px rgba(0,0,0,0.75)" : "",
        cursor: isHoveredOver ? "pointer" : ""
    }

    return (
        <div
            onMouseEnter={() => handleOnMouseEnter()}
            onMouseLeave={() => handleOnMouseLeave()}
            onClick={() => onClick(driverId)}
            className="card mb-3"
            style={divStyle}
        >
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
