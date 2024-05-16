export default interface IApiGateway {
    get(url: string): Promise<Response>;
}
