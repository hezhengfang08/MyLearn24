import axios from "axios";
import { ElMessage } from "element-plus";
import {userToken} from '@/utils/sessionstorage'
import {commonConsts} from '@/utils/commonConsts'
var {getToken} = userToken();
axios.defaults.baseURL = "http://124.223.161.17:5059/api/";
const {apiSuccesCode} = commonConsts();  //成功码
//这里怎样适配多个api呢
const service =  axios.create({
    timeout:5000
    
})
service.interceptors.request.use(
    config=>{
        config.headers["token"] = getToken();
        return config;
    })

service.interceptors.response.use(
    res =>{ 
        let {code,msg} = res.data;
        if(code !== apiSuccesCode){
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