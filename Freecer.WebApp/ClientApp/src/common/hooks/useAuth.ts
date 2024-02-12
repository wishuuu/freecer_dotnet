import {useEffect} from "react";
import {useUser} from "./useUser";
import api from "../api/data-fetching-axios.ts";
import {AuthContextData} from "@/common/types/User.ts";

export const useAuth = () => {
    const { user, addUser, removeUser } = useUser();

    useEffect(() => {
        me().then(async (user) => {
            if (user) {
                await api.send(api.config("get", "user/refresh"))
                addUser(user);
            }
        });
    }, [addUser]);
    
    const me = () => {
        return api.send<AuthContextData>(api.config("get", "user/me"));
    }

    const login = (username: string, password: string) => {
        api.send<AuthContextData>(api.config("post", "user/login", {username, password})).then((auth) => {
            addUser(auth);
        });
    };
    
    const logout = () => {
        api.send(api.config("post", "user/logout")).then(() => {
            removeUser();
        });
    };

    return { user, login, logout };
};