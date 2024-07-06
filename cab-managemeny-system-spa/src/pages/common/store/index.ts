import { configureStore } from '@reduxjs/toolkit';
import userReducer from './slices/user.slice';
import authenticationMiddleware from './middleware/authentication.middleware';

const store = configureStore({
  reducer: {
    user: userReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authenticationMiddleware),
});

export default store;