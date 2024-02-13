import {createContext} from "react";
import {Moment} from "moment";

interface AuthContextData {
    user: User | null;
    login: (username: string, password: string) => void;
    logout: () => void;
    expires: Date | null;
    updated: Moment | null;
}

interface User {
    username: string;
    email: string;
    firstName: string;
    lastName: string;
    phoneNumber: string | null;
    profilePicture: string | null;
}

export const AuthContext = createContext<AuthContextData>({
    user: null,
    login: () => {},
    logout: () => {},
    expires: null,
    updated: null
});

export type { User, AuthContextData };