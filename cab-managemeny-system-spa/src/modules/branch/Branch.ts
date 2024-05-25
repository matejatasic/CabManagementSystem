export default class Branch {
    public id: number;
    public managerId: number | null;
    public employeesIds: string[];
    public name: string;

    constructor(
        id: number,
        managerId: number | null,
        employeesIds: string[],
        name: string,
    ) {
        this.id = id;
        this.managerId = managerId;
        this.employeesIds = employeesIds;
        this.name = name;
    }
}