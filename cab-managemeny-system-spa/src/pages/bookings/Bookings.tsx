import ContentCard from "../admin/common/content-card/ContentCard";
import TableContent from "../admin/common/table-content/TableContent";

export default function Bookings() {
    const headers = ["Date Booked", "From", "To", "Status"];
    const rows = [
        [
            "2023-04-03",
            "107th St, Chicago",
            "99th St, Chicago",
            "Pending"
        ],
        [
            "2023-04-03",
            "107th St, Chicago",
            "99th St, Chicago",
            "Pending"
        ],
        [
            "2023-04-03",
            "107th St, Chicago",
            "99th St, Chicago",
            "Pending"
        ],
        [
            "2023-04-03",
            "107th St, Chicago",
            "99th St, Chicago",
            "Pending"
        ],
    ];


    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}