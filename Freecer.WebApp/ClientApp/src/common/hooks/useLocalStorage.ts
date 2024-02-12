import { useState } from "react";

export enum StorageKeys {
    authData = "freecer_auth"
}

export const useLocalStorage = () => {
    const [value, setValue] = useState<string | null>(null);

    const setItem = (key: StorageKeys, value: string) => {
        localStorage.setItem(key, value);
        setValue(value);
    };

    const getItem = (key: StorageKeys) => {
        const value = localStorage.getItem(key);
        setValue(value);
        return value;
    };

    const removeItem = (key: StorageKeys) => {
        localStorage.removeItem(key);
        setValue(null);
    };

    return { value, setItem, getItem, removeItem };
};