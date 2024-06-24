import { useEffect, useState } from "react";

import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";
import Cab from "../../../modules/cab/Cab";
import CarsProps from "./CarsProps";
import Employee from "../../../modules/employee/Employee";
import User from "../../../modules/user/models/User";

export default function Cars(props: CarsProps) {
    const { repository, employeeRepository, userRepository } = props;

    const [cabs, setCabs] = useState<Cab[]>();
    const [employees, setEmployees] = useState<Employee[]>();
    const [users, setUsers] = useState<User[]>();

    useEffect(() => {
        repository.getAll()
            .then(data => {
                setCabs(data.map(cab => new Cab(
                    cab.id,
                    cab.name,
                    cab.numberOfSeats,
                    cab.fuelType,
                    cab.registeredUntil,
                    cab.registrationPlates,
                    cab.driverId
                )))
            })
            .catch(error => {
                console.log(error);
            });
    }, []);

    useEffect(() => {
        if (cabs) {
            employeeRepository.getAll()
                .then(data => {
                    setEmployees(data.map(employee => new Employee(
                        employee.id,
                        employee.userId,
                        employee.branchId
                    )))
                })
                .catch(error => {
                    console.log(error);
                });
        }
    }, [cabs])

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
                .catch(error => {
                    console.log(error);
                });
        }
    }, [employees]);

    const headers = [
        "Name",
        "Number of seats",
        "Fuel type",
        "Registered until",
        "Registration plates",
        "Driver",
        "Date added"
    ];

    const rows = cabs ? cabs.map(cab => {
        const employee = employees?.find(employee => cab.driverId === employee.id);
        const user = employee ? users?.find(user => user.id === employee.branchId) : null;

        return [
            cab.name,
            cab.numberOfSeats,
            cab.fuelType,
            cab.registeredUntil,
            cab.registrationPlates,
            user ? user.fullName : "",
            "2023-12-15"
        ];
    }) : [];

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}
