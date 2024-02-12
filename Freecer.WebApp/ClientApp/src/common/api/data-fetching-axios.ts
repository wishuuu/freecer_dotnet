import axios, {AxiosError, AxiosRequestConfig, AxiosResponse} from "axios";

const baseUrl = import.meta.env.MODE === 'development' ? 'https://localhost:7277' : import.meta.env.BASE_URL;

function generateConfig<T>(method: "get" | "post" | "put" | "delete", endpoint: string, data?: T) {
    return {
        method: method,
        withCredentials: true,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        data: data,
        url: baseUrl + "/api/" + endpoint
    } as AxiosRequestConfig;
}

async function axios_request<T>(config: AxiosRequestConfig): Promise<T> {
    return axios.request(config).then((response: AxiosResponse<T>) => {
        return response.data;
    }).catch((error: AxiosError) => {
        throw error;
    })
}

export default {config: generateConfig, send: axios_request} as const;