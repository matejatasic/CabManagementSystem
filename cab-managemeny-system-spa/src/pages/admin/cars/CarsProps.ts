import ICabRepository from "../../../modules/cab/repositories/ICabRepository"
import IEmloyeeRepository from "../../../modules/employee/employee-repository/IEmployeeRepository"
import IUserRepository from "../../../modules/user/repositories/IUserRepository"
import PageProps from "../../common/props/PageProps"

type CarsProps = {
    employeeRepository: IEmloyeeRepository,
    userRepository: IUserRepository
} & PageProps<ICabRepository>

export default CarsProps;