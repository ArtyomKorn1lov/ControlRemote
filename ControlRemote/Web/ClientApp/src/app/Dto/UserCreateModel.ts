export class UserCreateModel {
    name: string;
    login: string;
    password: string;
    role: string;

    public constructor(_name: string, _login: string, _password: string, _role: string) {
        this.name = _name;
        this.login = _login;
        this.password = _password;
        this.role = _role;
    }
}