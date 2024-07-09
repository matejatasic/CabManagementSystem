import { Navigate, Route, Routes } from "react-router-dom";
import { useSelector } from "react-redux";

import RentCab from "./rent-cab/RentCab";
import RootState from "../common/store/state.type";
import CabRepository from "../../modules/cab/cab-repository/CabRepository";
import CabGateway from "../../modules/cab/cab-gateway/CabGateway";
import ApiGateway from "../../modules/common/ApiGateway";
import ProtectedRoutesProps from "./ProtectedRouteProps";
import Bookings from "./bookings/Bookings";
import BookingRepository from "../../modules/booking/booking-repository/BookingRepository";
import BookingGateway from "../../modules/booking/booking-gateway/BookingGateway";
import ChangeAccountDetails from "./change-account-details/ChangeAccountDetails";
import CustomerRoutesEnum from "./common/enums/CustomerRoutesEnum";

export default function ProtectedRoutes() {
    const protectedRoutes: Array<ProtectedRoutesProps> = [
        {
            path: CustomerRoutesEnum.RentACab,
            component: RentCab,
            props: {
                repository: new CabRepository(new CabGateway(new ApiGateway()))
            }

        },
        {
            path: CustomerRoutesEnum.Bookings,
            component: Bookings,
            props: {repository: new BookingRepository(new BookingGateway(new ApiGateway()))}
        },
        {
            path: CustomerRoutesEnum.ChangeProfileSettings,
            component: ChangeAccountDetails
        }
    ];
    const user = useSelector((state: RootState) => state.user)

    return (
        <Routes>
            {protectedRoutes.map(({ path, component: Component, props }, index) => (
                <Route
                    key={index}
                    path={path}
                    element={
                        user.token !== '' ? (
                            <Component {...props} />
                        ) : (
                            <Navigate to={CustomerRoutesEnum.Login} replace />
                        )
                    }
                />
            ))}
        </Routes>
    );
}