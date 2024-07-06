import UserSession from "../types/UserSession";

export default interface ISessionRepository {
    getToken(): string;
    getUserSession(): UserSession | null;
    setUserSession(
        userId: number,
        username: string,
        token: string,
        role: string
    ): void;
    removeUserSession(): void;
}