<template>
    <div class="search-content">
        <div class="express-main">
            <!-- 左侧分类选项 -->
            <div class="express-main-left">
                <ul class="fn-textcenter">
                    <li  :class="v.category_id == (route.query.id ||categoryAllData[0].category_id) ?'active':''" 
                         v-for="v in categoryAllData" :key="v.category_id" 
                        @click="getList(v.category_id)">
                        <router-link :to="{path:'/search',query:{id:v.category_id}}">
                            {{v.category_name}}
                        </router-link>
                        
                    </li>
                </ul>
            </div>
            <!-- 右侧列表展示 -->
            <div class="express-main-right">
                <h4>热卖</h4>
                <ul>
                    <li v-for="v in categoryListData" :key="v.product_id">
                        <div class="express-img">
                            <img :src="v.product_img_url"/>
                        </div>
                        
                        <div class="express-list">
                            <p>{{v.product_name}}</p>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import {ref,onMounted} from 'vue'
import {useRoute} from 'vue-router' //导入路由
import {ICategory,ICategorygoods} from '@/utils/dataType'
import {getCategoryAll,getCategoryGoods} from '@/api/http'

const route = useRoute();

const categoryAllData = ref<ICategory[]>([]); //全部类别
const categoryListData = ref<ICategorygoods[]>([]);  //右侧列表数据

async function  getList(id:number){
   await getCategoryGoods<ICategorygoods[]>({mId:id}).then(res=>{
        categoryListData.value = res.data;
    })
}
onMounted(async()=>{
    categoryAllData.value = await getCategoryAll<ICategory[]>().then(res=>res.data);
    if(categoryAllData && categoryAllData.value){
        getList(Number(route.query.id) || categoryAllData.value[0].category_id);
    }
})




</script>

<style>
.active {
    background:#fff;
    border-left:3px solid red;
}
</style>