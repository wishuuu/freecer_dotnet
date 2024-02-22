import {useEffect} from "react";
import api from "../api/data-fetching-axios.ts";
import {AuthContextData} from "@/common/types/User.ts";
import {StorageKeys, useLocalStorage} from "@/common/hooks/useLocalStorage.ts";
import moment from "moment/moment";
import {useUser} from "@/common/hooks/useUser.ts";

export const useAuth = () => {
    const {setItem, removeItem} = useLocalStorage();
    const {user, setUser} = useUser();

    useEffect(() => {
        if(needRefresh()) {
            console.log('refreshing');
            me().then(async (user) => {
                if (user) {
                    await api.send(api.config("post", "user/refresh"))
                    user.updated = moment();
                    setItem(StorageKeys.authData, JSON.stringify(user));
                    setUser(user);
                }
            });
        } 
    }, [user]);
    
    const me = () => {
        return api.send<AuthContextData>(api.config("get", "user/me"));
    }

    const login = async (username: string, password: string) => {
        return api.send<AuthContextData>(api.config("post", "user/login", {username, password})).then(async () => {
            let user1 = await me();
            if (user1) {
                user1.updated = moment();
                setItem(StorageKeys.authData, JSON.stringify(user1));
                setUser(user1);
            }
        });
    };
    
    const logout = async () => {
        await api.send(api.config("post", "user/logout"));
        removeItem(StorageKeys.authData);
    };

    const needRefresh = () => {
        return user && user.updated && moment().subtract(10, 'm') > moment(user.updated);
    }
    

    return { login, logout } as const;
};