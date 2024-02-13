import {StorageKeys, useLocalStorage} from "@/common/hooks/useLocalStorage.ts";
import {AuthContextData} from "@/common/types/User.ts";
import {useEffect, useState} from "react";


export const useUser = () => {
    const {getItem} = useLocalStorage();
    const [user, setUser] = useState<AuthContextData | null>(null);
    
    useEffect(() => {
        const user = getItem(StorageKeys.authData);
        if (user) {
            setUser(JSON.parse(user));
        }
    }, []);

    return { user } as const;
}
