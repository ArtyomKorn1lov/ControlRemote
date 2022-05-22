export class UserModel {
    id: number;
    name: string;
    login: string;
    role: string

    public constructor(_id: number, _name: string, _login: string, _role: string) {
        this.id = _id;
        this.name = _name;
        this.login = _login;
        this.role = _role;
    }
}