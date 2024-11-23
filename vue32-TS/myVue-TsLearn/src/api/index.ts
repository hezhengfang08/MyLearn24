import axios, {AxiosInstance, AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig} from 'axios'

axios.defaults.baseURL = `http://124.223.161.17:3015`;
// 添加请求拦截器
// 在发送请求之前做些什么
const instance: AxiosInstance = axios.create(
    {
        baseURL: 'http://124.223.161.17:3015/',
        timeout: 1000,
    }
);
const configFunction = (config: AxiosRequestConfig): InternalAxiosRequestConfig => {
    // 确保 headers 不是 undefined
    const headers: any = config.headers || {};
    headers['Content-Type'] = 'application/json';
    return { ...config, headers };
  };
  instance.interceptors.request.use(configFunction)
// 添加响应拦截器
instance.interceptors.response.use((response:AxiosResponse)=>{
   // 对响应数据做点什么
   return response
}, (err:Error | string | number)=>{
   // 对响应错误做点什么
   return Promise.reject(err);
})
export default instance;