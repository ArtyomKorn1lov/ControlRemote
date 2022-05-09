export class RegisterModel {
    login: string;
    password: string;
    confirmPassword: string;

    public constructor(_login: string, _password: string, _confirmPassword: string) {
        this.login = _login;
        this.password = _password;
        this.confirmPassword = _confirmPassword;
    }
}