import {createContext} from "react";

interface AuthContextData {
    user: User | null;
    setAuth: (user: AuthContextData | null) => void;
    expires: Date | null;
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
    setAuth: () => {},
    expires: null
});

export type { User, AuthContextData };