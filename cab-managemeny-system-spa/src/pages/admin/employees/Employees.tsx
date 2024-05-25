import { useEffect, useState } from "react";

import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";
import Employee from "../../../modules/employee/Employee";
import EmployeesProps from "./EmployeesProps";
import User from "../../../modules/user/User";
import Branch from "../../../modules/branch/Branch";

export default function Employees(props: EmployeesProps) {
    const { repository, userRepository, branchRepository } = props;

    const headers = ["Username", "Email", "Full Name", "Address", "Phone number", "Branch", "Date joined"];
    const [employees, setEmployees] = useState<Employee[]>();
    const [users, setUsers] = useState<User[]>();
    const [branches, setBranches] = useState<Branch[]>();

    useEffect(() => {
        repository.getAll()
            .then(data => {
                setEmployees(data.map(employee => new Employee(
                    employee.id,
                    employee.userId,
                    employee.branchId,
                )));
            })
            .catch((error: Error) => {
                console.log(error);
            })
    }, []);

    useEffect(() => {
        if (employees) {
            userRepository.getAll()
            .then((data) => {
                setUsers(data.map(user => new User(
                    user.id,
                    user.username,
                    user.email,
                    user.firstName,
                    user.lastName,
                    user.address,
                    user.phone
                )));
            })
            .catch((error: Error) => {
                console.log(error);
            });
        }
    }, [employees]);

    useEffect(() => {
        if (employees) {
            branchRepository.getAll()
            .then((data) => {
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
        }
    }, [employees]);

    const rows = employees ? employees.map(employee => {
        const user = users?.find(user => user.id === employee.branchId);
        const branch = branches?.find(branch => branch.id === employee.branchId);
        const userData = user ? [
            user.username,
            user.email,
            user.fullName,
            user.address,
            user.phone,
        ] : [];
        const branchData = branch ? [
            branch.name
        ] : [];

        return [
            ...userData,
            ...branchData,
            "2023-04-12"
        ];
    }) : [];

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}