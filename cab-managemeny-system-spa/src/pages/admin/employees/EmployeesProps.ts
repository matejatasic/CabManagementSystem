import IBranchRepository from "../../../modules/branch/branch-repository/IBranchRepository";
import IEmloyeeRepository from "../../../modules/employee/employee-repository/IEmployeeRepository";
import IUserRepository from "../../../modules/user/user-repository/IUserRepository";
import PageProps from "../../common/props/PageProps";

type EmployeesProps = {
    userRepository: IUserRepository;
    branchRepository: IBranchRepository
} & PageProps<IEmloyeeRepository>;

export default EmployeesProps;
