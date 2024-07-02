import IStorageGateway from "./IStorageGateway";

export default class StorageGateway implements IStorageGateway {
    get(propertyName: string): string | number {
        return sessionStorage.get(propertyName);
    }

    set(propertyName: string, value: string): void {
        sessionStorage.setItem(propertyName, value);
    }

    remove(propertyName: string): void {
        sessionStorage.removeItem(propertyName);
    }
}