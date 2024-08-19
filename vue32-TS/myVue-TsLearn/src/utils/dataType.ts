//全局模块类型的定义
export interface IGlobalState{
    home:IHomeState //home 模块
     //user: //各个模块
}
export interface IHomeState{
    swiper:ISwiper[],//轮播数据
    navState:boolean //底部导航数据是否显示
}
 //轮播数据类型
export interface ISwiper{
    id:number,
    img_url:string
}
//路由数据
export interface IFiles{
   [propName:string]:any
}
//导航数据
export interface INav {
    title:string,
    name:string,
    path:string,
    icon?:string,
    component:any,
    [propName:string]:any
}
//首页商品分类
export interface ICategory{
    category_id:number,
    category_name:string,
    catagory_pid:number,
    img:string
}
//首页商品列表
export interface ICategorygoods{
    category_id:number,
    category_name:string,
    category_pid?:number,
    img:string,
    product_img_url:string,
    product_uprice:number,
    product_name:string,
    [propname:string]:any
}
//详情页数据类型-- //详情页图片
export interface IDetailImg{
 image_url:string
}
// //详情页图片
export interface IDetailInfo{
    category_id:number,
    product_detail:string,
    product_img_url:string,
    product_uprice:number,
    product_name:string,
    [propname:string]:any
}