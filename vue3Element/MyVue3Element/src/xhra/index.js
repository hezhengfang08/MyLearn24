import axios from "axios";
import { ElMessage } from "element-plus";
import userToken from '@/utils/sessionstorage'
var {getToken} = userToken();
axios.defaults.baseURL = "http://49.235.128.49:5059/api/";
const succesCode = 20000;  //成功码
//这里怎样适配多个api呢
const service = baseURL=> axios.create({
    timeout:5000,
    baseURL:baseURL||axios.defaults.baseURL
})
service.interceptors.request.use(
    config=>{
        config.headers["token"] = getToken();
        return config;
    })

service.interceptors.response.use(
    res =>{ 
        let {code,msg} = res.data;
        if(code !== succesCode){
            ElMessage({
                message:msg||'warning,this is a warning message',
                type:'warning',
                duration:2000
            });

        }
        return res;

    },err=>{
        //400 500 开头的
        return Promise.reject(err);
    }
)    
export default service;