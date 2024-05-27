import { useEffect, useState } from "react";

import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";
import BranchesProps from "./BranchesProps";
import User from "../../../modules/user/User";
import Branch from "../../../modules/branch/Branch";
import Employee from "../../../modules/employee/Employee";

export default function Branches(props: BranchesProps) {
    const { repository, userRepository, employeeRepository } = props;

    const headers = ["Name", "Manager"];
    const [branches, setBranches] = useState<Branch[]>();
    const [employees, setEmployees] = useState<Employee[]>();
    const [users, setUsers] = useState<User[]>();

    useEffect(() => {
        repository.getAll()
            .then(data => {
                setBranches(data.map(branch => new Branch(
                    branch.id,
                    branch.managerId,
                    branch.employeesIds,
                    branch.name
                )));
            })
            .catch((error: Error) => {
                console.log(error);
            });
    }, []);

    useEffect(() => {
        if (branches) {
            employeeRepository.getAll()
                .then(data => {
                    setEmployees(data.map(employee => new Employee(
                        employee.id,
                        employee.userId,
                        employee.branchId,
                    )));
                })
                .catch((error: Error) => {
                    console.log(error);
                });

        }
    }, [branches]);

    useEffect(() => {
        if (employees) {
            userRepository.getAll()
                .then(data => {
                    setUsers(data.map(user => new User(
                        user.id,
                        user.username,
                        user.email,
                        user.firstName,
                        user.lastName,
                        user.address,
                        user.phone
                    )))
                })
                .catch((error: Error) => {
                    console.log(error);
                });
        }
    }, [employees]);

    const rows = branches ? branches.map(branch => {
        const employee = employees?.find(employee => branch.managerId === employee.id);
        const user = employee ? users?.find(user => user.id === employee.branchId) : null;

        return [
            branch.name,
            user ? user.fullName : ""
        ];
    }) : [];

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}
