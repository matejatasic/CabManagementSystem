import { ArrowUp, PencilFill, TrashFill } from "react-bootstrap-icons";

import TableProps from "./TableProps";

export default function Table(props: TableProps) {
    const { headers, rows } = props;

    return (
        <table className="table mt-5 mb-4">
            <thead>
                <tr>
                    {headers.map((header, index) => {
                        return (
                            <th key={index}>{header} <span className="sort-arrow"><ArrowUp /></span></th>
                        );
                    })}
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {rows.map((row, index) => {
                    return (
                        <tr key={index}>
                            {row.map((column, columnIndex) => <td key={columnIndex}>{column}</td>)}
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
