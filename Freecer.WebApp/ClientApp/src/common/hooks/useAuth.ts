import {useEffect} from "react";
import api from "../api/data-fetching-axios.ts";
import {AuthContextData} from "@/common/types/User.ts";
import {StorageKeys, useLocalStorage} from "@/common/hooks/useLocalStorage.ts";
import moment from "moment/moment";
import {useUser} from "@/common/hooks/useUser.ts";

export const useAuth = () => {
    const {setItem, removeItem} = useLocalStorage();
    const {user} = useUser();

    useEffect(() => {
        if(needRefresh()) {
            console.log('refreshing');
            me().then(async (user) => {
                if (user) {
                    await api.send(api.config("post", "user/refresh"))
                    user.updated = moment();
                    setItem(StorageKeys.authData, JSON.stringify(user));
                }
            });
        } 
    }, [user]);
    
    const me = () => {
        return api.send<AuthContextData>(api.config("get", "user/me"));
    }

    const login = (username: string, password: string) => {
        api.send<AuthContextData>(api.config("post", "user/login", {username, password})).then(() => {
            me().then(async (user) => {
                if (user) {
                    user.updated = moment();
                    setItem(StorageKeys.authData, JSON.stringify(user));
                }
            });
        });
    };
    
    const logout = () => {
        api.send(api.config("post", "user/logout")).then(() => {
            removeItem(StorageKeys.authData);
        });
    };

    const needRefresh = () => {
        return user && user.updated && moment().subtract(10, 'm') > moment(user.updated);
    }
    

    return { login, logout } as const;
};