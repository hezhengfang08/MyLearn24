import { createRouter, createWebHashHistory } from 'vue-router'

const routes = [
    {
        path: "/",
        redirect: '/login'
    },
    {
        path: '/login',
        name: 'login',
        component: () => import('../views/login/index.vue'),
    },
    {
        path: '/home',
        name: 'home',
        component: () => import('../layout/index.vue'),
    }

]
const router = createRouter({
    history: createWebHashHistory(),
    routes
})
export default router