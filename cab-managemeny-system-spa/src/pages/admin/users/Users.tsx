import { useEffect, useState } from "react";

import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";
import User from "../../../modules/user/models/User";
import PageProps from "../../common/props/PageProps";
import IUserRepository from "../../../modules/user/repositories/IUserRepository";

export default function Users(props: PageProps<IUserRepository>) {
    const { repository } = props;

    const headers = ["Username", "Email", "Full Name", "Address", "Phone number", "Date joined"];
    const [users, setUsers] = useState<User[]>();

    useEffect(() => {
        repository.getAll()
            .then(data => {
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
            })
    }, []);

    const rows = users ? users.map(user => {
        return [
            user.username,
            user.email,
            user.firstName,
            user.lastName,
            "7340 Mollis Street, Veracruz, Mexico",
            "(821) 838-4026"
        ];
    }) : [];

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}
