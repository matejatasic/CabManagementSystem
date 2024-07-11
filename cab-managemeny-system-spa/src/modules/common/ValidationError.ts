type ValidationErrorObject = {
    id: string,
    message: string
}

export default class ValidationError extends Error {
    public fieldName: string | null = null;

    constructor(message?: string, fieldName?: string) {
        super(message);

        if (fieldName) {
            this.fieldName = fieldName;
        }
    }
}