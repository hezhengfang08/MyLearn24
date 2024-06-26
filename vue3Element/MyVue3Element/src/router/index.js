import { createRouter, createWebHashHistory } from 'vue-router'

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
        component: () => import('../layout/index.vue'),
        children:[
            {
                path: '/index',  //首页
                name: 'index',
                meta:{title:'首页'},
                component: () => import('../views/home/index/index.vue')
            },
            {
                path: '/vehicle',//车辆列表
                name: 'vehicle',
                meta:{title:'车辆列表'},
                component: () => import('../views/home/vehicle/index.vue')
            },
            {
                path: '/monitor',//车辆电量监控
                name: 'monitor',
                meta:{title:'车辆电量监控'},
                component: () => import('../views/home/monitor/index.vue'),
                children:[
                    {
                        path: '/monitor/charge',//车辆充电
                        name: 'charge',
                        meta:{title:'车辆充电'},
                        component: () => import('../views/home/monitor/charge/index.vue')
                    },
                    {
                        path: '/monitor/maintenance',//车辆维保
                        name: 'maintenance',
                        meta:{title:'车辆维保'},
                        component: () => import('../views/home/monitor/maintenance/index.vue')
                    },
                    {
                        path: 'order',//工单管理
                        name: 'order',
                        meta:{title:'工单管理'},
                        component: () => import('../views/home/monitor/order/index.vue')
                    },
                ]
            },
        ]
    }
]
const router = createRouter({
    history: createWebHashHistory(),
    routes
})
export default router