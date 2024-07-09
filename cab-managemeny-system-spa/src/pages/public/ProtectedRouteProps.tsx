import { ComponentType } from "react";

type ProtectedRoutesProps = {
    path: string,
    component: ComponentType<any>,
    props?: Record<string, any>
};

export default ProtectedRoutesProps;