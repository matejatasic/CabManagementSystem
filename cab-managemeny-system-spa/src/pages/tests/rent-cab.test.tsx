import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import { configureStore } from "@reduxjs/toolkit";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";

import RentCab from "../public/rent-cab/RentCab";
import IBookingRepository from "../../modules/booking/repositories/IBookingRepository";
import Booking from "../../modules/booking/models/Booking";
import ICabRepository from "../../modules/cab/repositories/ICabRepository";
import userReducer from "../common/store/slices/user.slice";
import BookingRepository from "../../modules/booking/repositories/BookingRepository";
import BookingCreateViewModel from "../../modules/booking/view-models/BookingCreateViewModel";
import IBookingGateway from "../../modules/booking/gateways/IBookingGateway";

describe("Rent cab page", () => {
    const mockCabs = [
        { id: 1, name: "", numberOfSeats: 1, driverId: 1, fuelType: "", registeredUntil: "", registrationPlates: "" },
        { id: 2, name: "", numberOfSeats: 1, driverId: 1, fuelType: "", registeredUntil: "", registrationPlates: "" },
    ];

    const mockBookings = [
        { id: 1, fromAddress: "", toAddress: "", travelerId: 1, travelCost: 1, driverId: 1 }
    ];

    let mockRepository: ICabRepository;
    const mockBookingRepository: IBookingRepository = {
        getAll: () => Promise.resolve(mockBookings),
        create: (booking: Booking) => Promise.resolve(mockBookings[0])
    };

    const renderComponent = (store: any, repository: ICabRepository = mockRepository, bookingRepository: IBookingRepository = mockBookingRepository) => {
        return render(
            <Provider store={store}>
                <MemoryRouter>
                    <RentCab repository={repository} bookingRepository={bookingRepository} />
                </MemoryRouter>
            </Provider>
        );
    };

    let store: Record<string, any>;

    beforeEach(() => {
        store = configureStore({ reducer: { user: userReducer } });
        mockRepository = {
            getAll: () => Promise.resolve(mockCabs)
        };
    });

    test("the page renders", async () => {
        renderComponent(store);

        expect(screen.getByTestId("heading")).toBeInTheDocument();
        await waitFor(() => {
            expect(screen.getByTestId("cabs-div").children).toHaveLength(mockCabs.length);
        });
    });

    test("the errors appear when trying to rent with empty fields", async () => {
        const mockGateway: IBookingGateway = {
            getAll: () => Promise.resolve(mockBookings),
            create: (createViewModel: BookingCreateViewModel) => Promise.resolve(mockBookings[0])
        };

        renderComponent(store, mockRepository, new BookingRepository(mockGateway));

        await waitFor(() => {
            const card = screen.getByTestId("cabs-div").querySelector(".card");
            expect(card).toBeInTheDocument();
        });

        fireEvent.click(screen.getByTestId("cabs-div").querySelector(".card") as Element);

        await waitFor(() => {
            const bookACabModal = screen.getByRole("dialog");
            expect(bookACabModal).toBeInTheDocument();
            const rentButton = screen.getByRole("button", { name: /Rent/i });
            expect(rentButton).not.toBeNull();
            fireEvent.click(rentButton);

            const validationErrors = bookACabModal.querySelectorAll(".text-danger");
            expect(validationErrors).toHaveLength(1);
        });
    });

    test("the cab successfully rented when appropriate data is given", async () => {
        let isFunctionCalled = false;
        mockBookingRepository.create = (booking: Booking) => {
            isFunctionCalled = true;
            return Promise.resolve(mockBookings[0]);
        };

        renderComponent(store, mockRepository);

        await waitFor(() => {
            const card = screen.getByTestId("cabs-div").querySelector(".card");
            expect(card).toBeInTheDocument();
        });

        fireEvent.click(screen.getByTestId("cabs-div").querySelector(".card") as Element);

        await waitFor(() => {
            const bookACabModal = screen.getByRole("dialog");
            const rentButton = screen.getByRole("button", { name: /Rent/i });
            const pickupAddressInput = bookACabModal.querySelector("#pickup-address") as HTMLInputElement;
            const toAddressInput = bookACabModal.querySelector("#to-address") as HTMLInputElement;

            expect(rentButton).not.toBeNull();
            expect(pickupAddressInput).not.toBeNull();
            expect(toAddressInput).not.toBeNull();

            fireEvent.change(pickupAddressInput, { target: { value: "Some pickup address" } });
            fireEvent.change(toAddressInput, { target: { value: "Some to address" } });
            fireEvent.click(rentButton);

            expect(isFunctionCalled).toBe(true);
        });
    });
});