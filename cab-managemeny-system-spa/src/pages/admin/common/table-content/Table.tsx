import { ArrowUp, PencilFill, TrashFill } from "react-bootstrap-icons";
import TableProps from "./TableProps";

export default function Table(props: TableProps) {
    const { headers, rows } = props;

    return (
        <table className="table mt-5 mb-4">
            <thead>
                <tr>
                    {headers.map(header => {
                        return (
                            <th>{header} <span className="sort-arrow"><ArrowUp /></span></th>
                        );
                    })}
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {rows.map(row => {
                    return (
                        <tr>
                            {row.map((column) => <td>{column}</td>)}
                            <td>
                                <button className="btn btn-success me-3"><PencilFill /></button>
                                <button className="btn btn-danger"><TrashFill /></button>
                            </td>
                        </tr>
                    );
                })}
            </tbody>
        </table>
    );
}