import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import { configureStore } from "@reduxjs/toolkit";

import Login from "../public/login/Login";
import userReducer from "../common/store/slices/user.slice"
import AuthenticationRepository from "../../modules/user/repositories/AuthenticationRepository";
import IAuthenticationRepository from "../../modules/user/repositories/IAuthenticationRepository";
import ValidationError from "../../modules/common/ValidationError";

describe("Login page", () => {
    const mockRepository = {
        register: jest.fn(),
        login: jest.fn(),
    };

    const renderComponent = (store: any, repository: IAuthenticationRepository = mockRepository) => {
        return render(
            <Provider store={store}>
                <MemoryRouter>
                    <Login repository={repository} />
                </MemoryRouter>
            </Provider>
        );
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

        expect(screen.getByTestId("username")).toBeInTheDocument();
        expect(screen.getByTestId("password")).toBeInTheDocument();
    });

    test("validation errors appear on submit if required fields are blank", async () => {
        const mockGateway = {
            register: jest.fn(),
            login: jest.fn()
        };
        renderComponent(store, new AuthenticationRepository(mockGateway));

        const username = screen.getByTestId("username");
        fireEvent.click(screen.getByTestId("submit-btn"));

        await waitFor(() => {
            expect(username.querySelector(".error")).toBeInTheDocument();
        });
    });

    test("show general error if thrown", async () => {
        const error = new ValidationError("General error");
        mockRepository.login.mockRejectedValue(error);

        renderComponent(store);

        const username = screen.getByTestId("username");
        const password = screen.getByTestId("password");

        fireEvent.change(username.querySelector("input") as Element, {target: {value: "testuser"}});
        fireEvent.change(password.querySelector("input") as Element, {target: {value: "Password12345&"}});

        fireEvent.click(screen.getByTestId("submit-btn"));

        await waitFor(() => {
            const generalError = screen.getByTestId("general-error");
            expect(generalError).toBeInTheDocument();
        });
    });
});