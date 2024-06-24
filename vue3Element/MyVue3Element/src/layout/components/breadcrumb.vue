<script setup>
import { ref,watch } from 'vue'
import {useRoute, useRouter} from 'vue-router'
const lists = ref([]);
const routes = useRoute();
watch(routes,(newVal,oldVal)=>{
    getBreadCrumb(newVal.matched)
},{deep:true,immediate:true})
function getBreadCrumb(matched){
    lists.value = matched;
}
</script>
<template>
    <el-breadcrumb separator="/">
    <el-breadcrumb-item class="breadcrumb-item" v-for="v in lists" :key="v.path">
      <router-link class="breadcrumb-router-Link" :to="v.path">{{ v.meta.title }}</router-link>
    </el-breadcrumb-item>
  </el-breadcrumb>
</template>
<style scoped>
 .breadcrumb-router-Link,breadcrumb-item{
    color:white !important;
 }
</style>