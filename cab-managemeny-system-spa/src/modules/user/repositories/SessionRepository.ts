import IStorageGateway from "../../common/IStorageGateway";
import ISessionRepository from "./ISessionRepository";

export default class SessionRepository implements ISessionRepository {
    private storageGateway: IStorageGateway;
    private readonly USER_ID_PROPERTY_NAME: string = "userId";
    private readonly USERNAME_PROPERTY_NAME: string = "username";
    private readonly TOKEN_PROPERTY_NAME: string = "token";
    private readonly ROLE_PROPERTY_NAME: string = "role";

    constructor(storageGateway: IStorageGateway) {
        this.storageGateway = storageGateway;
    }

    getToken(): string {
        return this.storageGateway.get(this.TOKEN_PROPERTY_NAME) as string;
    }

    setUserSession(
        userId: number,
        username: string,
        token: string,
        role: string
    ): void {
        this.storageGateway.set(this.USER_ID_PROPERTY_NAME, userId.toString());
        this.storageGateway.set(this.USERNAME_PROPERTY_NAME, username);
        this.storageGateway.set(this.TOKEN_PROPERTY_NAME, token);
        this.storageGateway.set(this.ROLE_PROPERTY_NAME, role);
    }

    removeUserSession(): void {
        this.storageGateway.remove(this.USER_ID_PROPERTY_NAME);
        this.storageGateway.remove(this.USERNAME_PROPERTY_NAME);
        this.storageGateway.remove(this.TOKEN_PROPERTY_NAME);
        this.storageGateway.remove(this.ROLE_PROPERTY_NAME);
    }
}