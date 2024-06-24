export default interface IApiGateway {
    get(url: string): Promise<Response>;
    post(url: string, body: Record<string, any>): Promise<any>;
}
