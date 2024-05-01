import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";

export default function Employees() {
    const headers = ["Username", "Email", "Full Name", "Address", "Phone number", "Branch", "Date joined"];
    const rows = [
        [
            "brianp",
            "bibendum.fermentum.metus@hotmail.net",
            "Brian Perez",
            "7340 Mollis Street, Veracruz, Mexico",
            "(821) 838-4026",
            "Travel",
            "2023-04-12"
        ],
        [
            "joshow",
            "nibh@outlook.com",
            "Josiah Howell",
            "Ap #516-2320 Non, Road, New Brunswick, USA",
            "(672) 350-7355",
            "Travel",
            "2023-01-22"
        ],
    ];

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}