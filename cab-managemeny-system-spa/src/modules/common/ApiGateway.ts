export default class ApiGateway {
    private readonly apiDomain: string = process.env.API_DOMAIN ?? '';
    private readonly basePath: string = `${this.apiDomain}`;

    public get(url: string): Promise<any> {
        return this.fetch(url, "GET");
    }

    private fetch(url: string, method: string, body: Record<string, string> | null = null): Promise<any> {
        const parameters: RequestInit = {
            method,
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
        }

        if (body) {
            parameters.body = JSON.stringify(body);
        }

        return fetch(`${this.basePath}${url}`, parameters);
    }
}
