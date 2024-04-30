import { ArrowUp, PencilFill, TrashFill } from "react-bootstrap-icons";
import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";

export default function Users() {
    const headers = ["Username", "Email", "Full Name", "Address", "Phone number", "Date joined"];
    const rows = [
        [
            "brianp",
            "bibendum.fermentum.metus@hotmail.net",
            "Brian Perez",
            "7340 Mollis Street, Veracruz, Mexico",
		    "(821) 838-4026",
            "2023-04-12"
		],
        [
            "joshow",
            "nibh@outlook.com",
            "Josiah Howell",
            "Ap #516-2320 Non, Road, New Brunswick, USA",
            "(672) 350-7355",
            "2023-01-22"
        ],
        [
            "bretb",
            "magna.lorem@protonmail.com",
            "Brett Ball",
            "285-9796 Nunc Ave, North Island, New Zealand",
            "1-533-865-3806",
            "2022-12-01"
        ],
        [
            "britw",
            "ultricies.ornare@google.edu",
            "Britanni Wade",
            "251-2096 Non, Rd., Surrey, Canada",
		    "(811) 943-9675",
            "2023-05-04"
        ]
    ]

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}