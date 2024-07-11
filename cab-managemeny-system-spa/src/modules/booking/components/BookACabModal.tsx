import { useEffect, useState } from "react";

import { useNavigate } from "react-router-dom";
import { Button, Form, Modal } from "react-bootstrap";

import BookingACabModalProps from "./BookACabModalProps"
import ValidationError from "../../common/ValidationError";
import CustomerRoutesEnum from "../../../pages/public/common/enums/CustomerRoutesEnum";
import Booking from "../models/Booking";

export default function BookACabModal(props: BookingACabModalProps) {
    const {
        shouldShowModal,
        handleModalClose,
        booking,
        setBooking,
        bookingRepository
    } = props;

    const navigate = useNavigate();
    const [validationErrors, setValidationErrors] = useState<Record<string, string>>({});

    useEffect(() => {
        if (!shouldShowModal) {
            setValidationErrors({});
        }
    }, [shouldShowModal])

    const handleRentButtonClick = () => {
        bookingRepository.create(booking)
            .then(response => {
                navigate(CustomerRoutesEnum.Bookings)
            })
            .catch(error => {
                if (error instanceof ValidationError) {
                    let fieldName = "general";

                    if (error.fieldName) {
                        fieldName = error.fieldName
                    }

                    setValidationErrors({...validationErrors, [fieldName]: error.message})
                }
            });
    }

    const changeFromAddress = (value: string) => {
        try {
            setBooking(booking.setFromAddress(value));
            setValidationErrors({...validationErrors, [Booking.FROM_ADDRESS]: ""});
        }
        catch(error) {
            if (error instanceof ValidationError) {
                setValidationErrors({...validationErrors, [Booking.FROM_ADDRESS]: error.message});
            }
        }
    };

    const changeToAddress = (value: string) => {
        try {
            setBooking(booking.setToAddress(value));
            setValidationErrors({...validationErrors, [Booking.TO_ADDRESS]: ""});
        }
        catch(error) {
            if (error instanceof ValidationError) {
                setValidationErrors({...validationErrors, [Booking.TO_ADDRESS]: error.message});
            }
        }
    };

    function isRentButtonDisabled(): boolean {
        return Object.values(validationErrors)
            .some((error: string) => error.length > 0)
    }

    return (
        <Modal show={shouldShowModal} onHide={() => handleModalClose()}>
            <Modal.Header closeButton>
                <Modal.Title>Rent a Cab</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3">
                        <Form.Label htmlFor="pickup-address">Pickup Address</Form.Label>
                        <Form.Control
                            type="text"
                            id="pickup-address"
                            onChange={(e) => changeFromAddress(e.target.value)}
                        >
                        </Form.Control>
                        {
                            validationErrors[Booking.FROM_ADDRESS] ?
                            <p className="text-danger mt-2">{validationErrors[Booking.FROM_ADDRESS]}</p>
                            : null
                        }
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label htmlFor="to-address">To Address</Form.Label>
                        <Form.Control
                            type="text"
                            id="to-address"
                            onChange={(e) => changeToAddress(e.target.value)}
                        >
                        </Form.Control>
                        {
                            validationErrors[Booking.TO_ADDRESS] ?
                            <p className="text-danger mt-2">{validationErrors[Booking.TO_ADDRESS]}</p>
                            : null
                        }
                    </Form.Group>
                    {
                        validationErrors["general"] ?
                        <p className="text-danger mt-4">{validationErrors["general"]}</p>
                        : null
                    }
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button
                    variant="primary" onClick={() => handleRentButtonClick()}
                    disabled={isRentButtonDisabled()}
                >
                    Rent
                </Button>
                <Button variant="secondary" onClick={() => handleModalClose()}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    );
}