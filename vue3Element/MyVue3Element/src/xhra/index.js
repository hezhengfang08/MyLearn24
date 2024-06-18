import axios from "axios";
const service = axios.create({
    baseURL:"127.0.0.1:8021",
    timeout:5000,
})
service.interceptors.request.use(
    config=>{
        return config;
    })

service.interceptors.response.use(
    repsone =>{ return repsone},err=>{
        return Promise.reject(err);
    }
)    
export default service;