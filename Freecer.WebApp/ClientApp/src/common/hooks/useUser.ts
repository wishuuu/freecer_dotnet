import {useContext} from "react";
import {StorageKeys, useLocalStorage} from "./useLocalStorage";
import {AuthContext, AuthContextData} from "../types/User.ts";

export const useUser = () => {
    const { user, setAuth, expires } = useContext(AuthContext);
    const { setItem, removeItem } = useLocalStorage();

    const addUser = (auth: AuthContextData) => {
        setAuth(auth);
        setItem(StorageKeys.authData, JSON.stringify(auth));
    };

    const removeUser = () => {
        setAuth(null);
        removeItem(StorageKeys.authData);
    };

    return { user, expires, addUser, removeUser };
};