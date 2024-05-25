export default class Employee {
    public id: number;
    public userId: number;
    public branchId: number;

    constructor(
        id: number,
        userId: number,
        branchId: number
    ) {
        this.id = id;
        this.userId = userId;
        this.branchId = branchId;
    }
}
