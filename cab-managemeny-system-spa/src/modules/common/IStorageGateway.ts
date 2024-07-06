export default interface IStorageGateway {
    get(propertyName: string): string | number | null;
    set(propertyName: string, value: string): void;
    remove(propertyName: string): void;
}