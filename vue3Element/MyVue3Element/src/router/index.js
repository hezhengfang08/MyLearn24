import { createRouter, createWebHashHistory } from 'vue-router'
import {initRoutes} from '@/utils/myFn'
import {permissionStore} from '@/store/permission'
import {findModulesByUid} from '@/xhra/api'
import {userStore} from '@/store/user'
const routes = [
    {
        path:'/',
        redirect:'/login'
    },
    {
        path: '/login',
        name: 'login',
        component: () => import('../views/login/index.vue')
    },
    {
        path: '/home',  //首页
        name: 'home',
        meta:{title:'首页'},
        redirect:'/index',
        component: () => import('../layout/index.vue'), 
    }
]
const router = createRouter({
    history: createWebHashHistory(),
    routes
});
//路由拦截
router.beforeEach(async(to,from,next)=>{
    if(to.path == '/login')
    {
        next();
    }
    else
    {
        if(permissionStore()&& permissionStore().navs.length ==0)
        {
             //没有缓存
            //发送请求
           const useUserStore = userStore();
           let res = await findModulesByUid({id:useUserStore.uid})
           
            //缓存数据Store
            permissionStore().setNav(res.data.data.list);
             //1、筛选home
             let homeRoutes = routes.filter(v=>v.path == '/home')[0];
             //2、添加children
             homeRoutes.children = [];
             //3、递归
             initRoutes(permissionStore().navs,homeRoutes.children);
             router.addRoute(homeRoutes);
             console.log(homeRoutes)
             next({...to});
        }else
        {
            next();
        }
    }
})
export default router