export default interface IStorageGateway {
    get(propertyName: string): string | number;
    set(propertyName: string, value: string): void;
    remove(propertyName: string): void;
}