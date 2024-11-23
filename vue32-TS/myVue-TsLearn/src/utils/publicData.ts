import { INav } from "./dataType";
//导航数据
export const routerNav: INav[] = [
    { title: '首页', path: '/home', name: 'home', icon: 'icon-shouye', component: 'Home', flag: true,},
    { title: '列表详情', path: '/detail/:id', name: 'detail', component: 'Detail', flag: false },
    { title: '分类搜索', path: '/search', name: 'search', icon: 'icon-sousuo', component: 'Search', flag: true },
    { title: '购物车', path: '/shopping', name: 'shopping', icon: 'icon-gouwuche', component: 'Shopping', flag: true },
    { title: '我的', path: '/mine', name: 'mine', icon: 'icon-shouye', component: 'Mine', flag: true }
]

