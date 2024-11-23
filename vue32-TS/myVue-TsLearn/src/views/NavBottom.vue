<template>
    <div>
        <div class="footer"></div>
        <div class="footer-seat">
            <div class="footer-lists">
                <ul>
                    <li v-for="v in data" :key="v.path">
                        <router-link :to="v.path" active-class="selected">
                            <i class="iconfont" :class="v.icon"></i>
                            <span>{{v.title}}</span>
                        </router-link>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { computed } from 'vue';
import {routerNav} from '@/utils/publicData'
//底部导航数据flag过滤
const data = computed(()=>routerNav.filter(v=>v.flag));
// export default defineComponent({
//     setup(){
//         const state = reactive({
//             navData:[
//                 {title:'首页1',path:'/home',icon:'icon-shouye'},
//                 {title:'首页2',path:'/home',icon:'icon-shouye'},
//                 {title:'首页3',path:'/home',icon:'icon-shouye'},
//                 {title:'首页4',path:'/home',icon:'icon-shouye'}
//             ]
//         });
//         let change = function(){}
//         return {
//             ...state,
//             change
//         }
//     }
// })

// const navData = ref([
//                 {title:'首页',path:'/home',icon:'icon-shouye'},
//                 {title:'分类页',path:'/search',icon:'icon-shouye'},
//                 {title:'购物车',path:'/shopping',icon:'icon-shouye'},
//                 {title:'我的',path:'/mine',icon:'icon-shouye'}
//             ])

</script>