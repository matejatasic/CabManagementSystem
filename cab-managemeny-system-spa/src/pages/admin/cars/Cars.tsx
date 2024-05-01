import ContentCard from "../common/content-card/ContentCard";
import TableContent from "../common/table-content/TableContent";

export default function Cars() {
    const headers = [
        "Name",
        "Number of seats",
        "Fuel type",
        "Registered until",
        "Registration plates",
        "Driver",
        "Date added"
    ];
    const rows = [
        [
            "Skoda Fabia",
            5,
            "Bensin",
            "2024-06-03",
            "61A495J",
            "Brian Perez",
            "2023-12-15"
        ],
        [
            "Dacia Logan",
            5,
            "Diesel",
            "2024-09-10",
            "JED546",
            "Josiah Howell",
            "2023-06-02"
        ]
    ]

    return (
        <ContentCard>
            <TableContent headers={headers} rows={rows} />
        </ContentCard>
    );
}