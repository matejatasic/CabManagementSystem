import IBranchRepository from "../../../modules/branch/branch-repository/IBranchRepository";
import IEmloyeeRepository from "../../../modules/employee/employee-repository/IEmployeeRepository";
import IUserRepository from "../../../modules/user/repositories/IUserRepository";
import PageProps from "../../common/props/PageProps";

type BranchesProps = {
    userRepository: IUserRepository;
    employeeRepository: IEmloyeeRepository;
} & PageProps<IBranchRepository>;

export default BranchesProps;