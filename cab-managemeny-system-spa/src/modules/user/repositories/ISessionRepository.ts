export default interface ISessionRepository {
    getToken(): string;
    setUserSession(
        userId: number,
        username: string,
        token: string,
        role: string
    ): void;
    removeUserSession(): void;
}