<template>
    <div class="nav">
        <ul>
            <li v-for="v in props.categoryData" :key="v.category_id" @click="getList(v.category_id)">
                <img :src="v.img"/>
                <p>{{v.category_name}}</p>
            </li>
        </ul>
    </div>

    <!-- 数据列表 -->
    <Lists :listsData="lists"/>
</template>

<script setup lang="ts">
import {ref,watch} from 'vue'
import {ICategory,ICategorygoods} from '@/utils/dataType'
import {getCategoryGoods} from '@/api/http'
import Lists from './list.vue'

let props = defineProps({
    categoryData:{
        type:Array as ()=> ICategory[],  //类型断言   可以手动指定一个值的类型
        default:()=>[]
    }
})

const lists = ref<ICategorygoods[]>();

function getList(id:number|string){
    sessionStorage.setItem("id",String(id));  //(key:string,value:string)
    getCategoryGoods<ICategorygoods[]>({mId:id}).then((res:any)=>{
        lists.value = res.data;
    })
};

//监听
watch(()=>props.categoryData,(newValue)=>{
    getList(sessionStorage.getItem("id") || newValue[0].category_id);
})

</script>