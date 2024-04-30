import { ReactElement } from "react";

type TableProps = {
    headers: string[],
    rows: (string | ReactElement)[][]
}

export default TableProps;