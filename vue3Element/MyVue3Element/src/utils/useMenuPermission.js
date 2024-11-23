import {ref, reactive, computed, onMounted} from 'vue'
import {useRoute} from 'vue-router'
import {permissionStore} from '@/store/permission'
import { storeToRefs} from 'pinia'

export default function useMenuPermission(){
    //进入页面模块
    const route = useRoute();
    const useMenuPermissionStore = permissionStore();
    onMounted(()=>{
        useMenuPermissionStore.setUserPermission(route.path);//'/vehicle'
         // 2、通过地址获取userPermission
    });
    const {userPermissions} = storeToRefs(useMenuPermissionStore)
     // 3、按钮匹配   
     const menuStatus = computed(()=>item=>{
        // var str = '编辑';
        // var arr = ['编辑','删除'];
        // console.log(arr.some(v=>v.includes(str)))
        return userPermissions.value.some(v=>!v.includes(item))
    })
    return {
        menuStatus
    }
}