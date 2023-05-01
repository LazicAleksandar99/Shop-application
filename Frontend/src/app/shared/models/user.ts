import { UserRole } from "../enums/user-role.enum";

export interface LoginUser {
    email: string;
    password: string;
}

export interface RegistrationUser{
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    birthday: Date;
    address: string;
    picture: string;
    password: string;
    role: UserRole
}
