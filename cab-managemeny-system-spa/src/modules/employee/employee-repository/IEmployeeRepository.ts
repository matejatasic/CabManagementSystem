export default interface IEmloyeeRepository {
    getAll(): Promise<EmployeeResponseData[]>;
}