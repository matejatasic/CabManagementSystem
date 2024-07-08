import { Action, Dispatch, Middleware, MiddlewareAPI } from "@reduxjs/toolkit";
import StorageGateway from "../../../../modules/common/StorageGateway";
import SessionRepository from "../../../../modules/user/repositories/SessionRepository";
import { login, logout } from "../slices/user.slice"
import RootState from "../state.type";
import LoginPayloadAction from "../slices/login-payload-action.type";

const sessionRepository = new SessionRepository(new StorageGateway());

const authenticationMiddleware: Middleware<{}, RootState> = (api: MiddlewareAPI<Dispatch<Action>, RootState>) => (next) => (action) => {
    if (login.match(action)) {
        const loginPayload = action.payload as LoginPayloadAction;

        sessionRepository.setUserSession(
            loginPayload.userId,
            loginPayload.username,
            loginPayload.token,
            loginPayload.role
        );
    }
    else if(logout.match(action)) {
        sessionRepository.removeUserSession();
    }

    return next(action);
}


export default authenticationMiddleware;