export default interface IEmployeeGateway {
    getAll(): Promise<EmployeeResponseData[]>;
}
