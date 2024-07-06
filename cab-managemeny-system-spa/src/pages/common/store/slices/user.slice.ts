import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import SessionRepository from "../../../../modules/user/repositories/SessionRepository";
import StorageGateway from "../../../../modules/common/StorageGateway";
import UserSession from "../../../../modules/user/types/UserSession";
import LoginPayloadAction from "./login-payload-action.type";

const sessionRepository = new SessionRepository(new StorageGateway());
const initialState: UserSession = sessionRepository.getUserSession() ?? {
    userId: 1,
    username: "",
    role: "",
    token: ""
};

const userSlice = createSlice({
    name: "users",
    initialState,
    reducers: {
        login(state, action: PayloadAction<LoginPayloadAction>) {
            state.userId = action.payload.userId;
            state.username = action.payload.username;
            state.token = action.payload.token;
            state.role = action.payload.role;
        }
    }
});

export const { login } = userSlice.actions;

export default userSlice.reducer;