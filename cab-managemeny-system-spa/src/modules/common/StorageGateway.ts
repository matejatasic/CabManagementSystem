import IStorageGateway from "./IStorageGateway";

export default class StorageGateway implements IStorageGateway {
    get(propertyName: string): string | number | null {
        return sessionStorage.getItem(propertyName);
    }

    set(propertyName: string, value: string): void {
        sessionStorage.setItem(propertyName, value);
    }

    remove(propertyName: string): void {
        sessionStorage.removeItem(propertyName);
    }
}