import IApiGateway from "./IApiGateway";

export default class ApiGateway implements IApiGateway {
    private readonly apiDomain: string = process.env.API_DOMAIN ?? '';
    private readonly basePath: string = `${this.apiDomain}`;

    public get(url: string): Promise<any> {
        return this.fetch(url, "GET");
    }

    public post(url: string, body: Record<string, any>): Promise<any> {
        return this.fetch(url, "POST", body);
    }

    private fetch(url: string, method: string, body: Record<string, string> | null = null): Promise<any> {
        const parameters: RequestInit = {
            method,
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            body: body && JSON.stringify(body)
        }

        if (body) {
            parameters.body = JSON.stringify(body);
        }

        return fetch(`${this.basePath}${url}`, parameters);
    }
}
