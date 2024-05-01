import { ReactElement } from "react";

type TableProps = {
    headers: string[],
    rows: (string | number)[][]
}

export default TableProps;