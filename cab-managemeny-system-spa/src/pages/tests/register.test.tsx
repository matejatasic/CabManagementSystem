import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";

import Register from "../public/register/Register";
import userReducer from "../common/store/slices/user.slice"
import IAuthenticationRepository from '../../modules/user/repositories/IAuthenticationRepository';
import AuthenticationRepository from '../../modules/user/repositories/AuthenticationRepository';
import ValidationError from '../../modules/common/ValidationError';

describe("Register page", () => {
    const mockRepository = {
        register: jest.fn(),
        login: jest.fn()
    };

    const renderComponent = (store: any, repository: IAuthenticationRepository = mockRepository) => {
        return render(
            <Provider store={store}>
                <MemoryRouter>
                    <Register repository={repository} />
                </MemoryRouter>
            </Provider>
        )
    };

    let store: Record<string, any>;

    beforeEach(() => {
        store = configureStore({
            reducer: {
                user: userReducer,
            }
        });
    });

    test("the page renders", () => {
        renderComponent(store);

        expect(screen.getByTestId("heading")).toBeInTheDocument();

        expect(screen.getByTestId("email")).toBeInTheDocument();
        expect(screen.getByTestId("username")).toBeInTheDocument();
        expect(screen.getByTestId("password")).toBeInTheDocument();
        expect(screen.getByTestId("confirm-password")).toBeInTheDocument();
        expect(screen.getByTestId("first-name")).toBeInTheDocument();
        expect(screen.getByTestId("last-name")).toBeInTheDocument();
        expect(screen.getByTestId("address")).toBeInTheDocument();
        expect(screen.getByTestId("phone")).toBeInTheDocument();
    });

    test("test validation errors appear on invalid data", () => {
        renderComponent(store);

        const email = screen.getByTestId("email");
        const username = screen.getByTestId("username");
        const password = screen.getByTestId("password");
        const confirmPassword = screen.getByTestId("confirm-password");
        const firstName = screen.getByTestId("first-name");
        const lastName = screen.getByTestId("last-name");
        const phone = screen.getByTestId("phone");

        fireEvent.change(email.querySelector("input") as Element, {target: {value: "aa"}});
        fireEvent.change(username.querySelector("input") as Element, {target: {value: "aa"}});
        fireEvent.change(password.querySelector("input") as Element, {target: {value: "password"}});
        fireEvent.change(confirmPassword.querySelector("input") as Element, {target: {value: "confirmPassword"}});
        fireEvent.change(firstName.querySelector("input") as Element, {target: {value: "1"}});
        fireEvent.change(lastName.querySelector("input") as Element, {target: {value: "1"}});
        fireEvent.change(phone.querySelector("input") as Element, {target: {value: "1"}});

        expect(email.querySelector(".error")).toBeInTheDocument();
        expect(username.querySelector(".error")).toBeInTheDocument();
        expect(password.querySelector(".error")).toBeInTheDocument();
        expect(confirmPassword.querySelector(".error")).toBeInTheDocument();
        expect(firstName.querySelector(".error")).toBeInTheDocument();
        expect(lastName.querySelector(".error")).toBeInTheDocument();
        expect(phone.querySelector(".error")).toBeInTheDocument();

    });

    test("test validation errors appear on submit if required fields are blank", async () => {
        const mockGateway = {
            register: jest.fn(),
            login: jest.fn()
        };
        renderComponent(store, new AuthenticationRepository(mockGateway));

        const email = screen.getByTestId("email");

        fireEvent.click(screen.getByTestId("submit-btn"));

        await waitFor(() => {
            expect(email.querySelector(".error")).toBeInTheDocument();
        });
    });

    test("test show general error if thrown", async () => {
        const error = new ValidationError("General error");
        mockRepository.register.mockRejectedValue(error);

        renderComponent(store);

        const email = screen.getByTestId("email");
        const username = screen.getByTestId("username");
        const password = screen.getByTestId("password");
        const confirmPassword = screen.getByTestId("confirm-password");
        const address = screen.getByTestId("address");

        fireEvent.change(email.querySelector("input") as Element, {target: {value: "test@example.com"}});
        fireEvent.change(username.querySelector("input") as Element, {target: {value: "testuser"}});
        fireEvent.change(password.querySelector("input") as Element, {target: {value: "Password12345&"}});
        fireEvent.change(confirmPassword.querySelector("input") as Element, {target: {value: "Password12345&"}});
        fireEvent.change(address.querySelector("input") as Element, {target: {value: "Some address"}})

        fireEvent.click(screen.getByTestId("submit-btn"));

        await waitFor(() => {
            const generalError = screen.getByTestId("general-error");
            expect(generalError).toBeInTheDocument();
        });
    });
})