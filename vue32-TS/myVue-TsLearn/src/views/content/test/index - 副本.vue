<template>
    <div>
        <!-- 头部模块  引用子组件 异步组件 -->
        <Header />
        <!-- 分类类别  动态传参 -->
        <Category :categoryData="categoryData" />
    </div>
</template>

<script setup lang="ts">
import {ref,onMounted} from 'vue'
import Header from './header/header.vue'
import Category from './category/category.vue'
import {getCategory} from '@/api/http'
import {ICategory} from '@/utils/dataType'

const categoryData = ref<ICategory[]>();//初始化

//方式一：
// function getData(){
//     getCategory<ICatagory[]>().then(res=>{
//         categoryData.value = res.data;
//     })
// }
// onMounted(()=>{
//     getData()
// })

//方式二：
onMounted(async()=>{
    categoryData.value = await getCategory<ICategory[]>().then(res=>res.data);
})
</script>