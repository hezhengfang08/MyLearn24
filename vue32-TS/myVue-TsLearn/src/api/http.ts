import instance from ".";
import {AxiosResponse} from 'axios'

// export function getSwiperImg()
// {
//     return instance({
//         url:'',
//         method:'get'
//     });
// }

// export interface AxiosResponse<T = any, D = any> {
//     data: T;
//     status: number;
//     statusText: string;
//     headers: RawAxiosResponseHeaders | AxiosResponseHeaders;
//     config: InternalAxiosRequestConfig<D>;
//     request?: any;
// }

// export function  getSwiperImg<T>():Promise<AxiosResponse<T>>
// {
//     return instance({
//                 url:``,
//                 method:'get'
//             })
// }
//参数ID
interface IMid{
    mId:number |string,
}
//按类别查询商品列表
export const getCategoryGoods = <T>(obj:IMid):Promise<AxiosResponse<T>>=> instance({
    method:'get',
    url:`/categorygoods`,
    params:obj
})
export const getDetails = <T>(obj:IMid):Promise<AxiosResponse<T>> => instance({
    method:'get',
    url:'/detail',
    params:obj
})
//获取轮播图片
export const getSwiperImg = <T>():Promise<AxiosResponse<T>> => instance.get(`swiper`);
//轮播图
export const  getCategory = <T>():Promise<AxiosResponse<T>> => instance.get(`categoryBanner`);
//所有商品分类
export const getCategoryAll = <T>():Promise<AxiosResponse<T>> => instance.get(`category`);
//所有商品分类


