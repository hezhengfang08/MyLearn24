import {defineStore} from 'pinia'
import {createTree} from '@/utils/myFn'

export const permissionStore = defineStore('permissions',{
    state:()=>({
          menuItems:[], //所有的菜单数据
          navs:[],//导航数据
          userPermissions:[] //用户菜单按钮权限
    }),
    actions:{
          setNav(data)
          {
            this.menuItems =data;
            //data数据没有层级，需要多级处理
            //取出顶层数据
            data.map(item=>{
                if(item.father_id ==0)
                {
                    this.navs.push({...item});
                }
            });
            createTree(this.navs,data);
          },
           //获取userPermission
        setUserPermission(path){//path ======  '/vehicle'
          let res = this.menuItems.filter(v=>v.menu_url===path);
          this.userPermissions = res.map(v=>v.userPermission);//['编辑','删除']
        }
    }

})