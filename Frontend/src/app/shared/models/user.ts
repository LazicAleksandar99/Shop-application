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

export interface UserAuthorization{
    unique_name: string, //email
    nameid: string,      //id
    role: string,        //role
}

export interface UserDetails{
    id: number,
    username: string;
    email: string;
    firstName: string;
    lastName: string;
    birthday: Date;
    address: string;
    picture: string;
    verificationStatus: string;
  }
