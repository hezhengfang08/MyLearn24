import { createRouter, createWebHashHistory, RouteRecordRaw } from "vue-router"
import { Files } from "@/views/files"
import { routerNav } from "@/utils/publicData"
const routes: Array<RouteRecordRaw> = [
    {
          path:'/',
        redirect:'/home'
    }
] 
routerNav.forEach(item=>{
    routes.push({
        path:item.path,
        name:item.name,
        component:Files[item.component]  //这个比较重要
    })
})
const router = createRouter({
    history:createWebHashHistory(),
    routes
})
export default router