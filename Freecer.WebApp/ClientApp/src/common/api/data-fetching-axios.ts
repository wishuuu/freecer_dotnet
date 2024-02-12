import axios, {AxiosError, AxiosRequestConfig, AxiosResponse} from "axios";

const baseUrl = import.meta.env.MODE === 'development' ? 'https://localhost:7277' : import.meta.env.BASE_URL;

async function axios_get<T>(endpoint: string, params?: any): Promise<T> {
    const config = {
        method: 'get',
        withCredentials: true,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        params: params,
        url: baseUrl + endpoint
    } as AxiosRequestConfig;
    
    return axios.request(config).then((response: AxiosResponse<T>) => {
        return response.data;
    }).catch((error: AxiosError) => {
        throw error;
    });
}

async function axios_post<T>(endpoint: string, data: any): Promise<T> {
    const config = {
        method: 'post',
        withCredentials: true,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        data: data,
        url: baseUrl + endpoint
    } as AxiosRequestConfig;

    return axios.request(config).then((response: AxiosResponse<T>) => {
        return response.data;
    }).catch((error: AxiosError) => {
        throw error;
    });
}

async function axios_put<T>(endpoint: string, data: any): Promise<T> {
    const config = {
        method: 'put',
        withCredentials: true,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        data: data,
        url: baseUrl + endpoint
    } as AxiosRequestConfig;

    return axios.request(config).then((response: AxiosResponse<T>) => {
        return response.data;
    }).catch((error: AxiosError) => {
        throw error;
    });
}

async function axios_delete<T>(endpoint: string, data: any): Promise<T> {
    const config = {
        method: 'delete',
        withCredentials: true,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        data: data,
        url: baseUrl + endpoint
    } as AxiosRequestConfig;

    return axios.request(config).then((response: AxiosResponse<T>) => {
        return response.data;
    }).catch((error: AxiosError) => {
        throw error;
    });
}

export default {get: axios_get, post: axios_post, put: axios_put, delete: axios_delete} as const;