<script setup>
import { ref, defineProps } from 'vue'
import {
  Document,
  Menu as IconMenu,
  Location,
  Setting,
} from '@element-plus/icons-vue'
import {tagStore} from '@/store/tag'
import { storeToRefs } from 'pinia';

const store = tagStore();
const {getTagList} = storeToRefs(store);

const handTag = item =>{
    //添加判断，去重
    let repeat = getTagList.value.some(v=>v.menu_url==item.menu_url);
    if(repeat) return;
    store.addTag(item);
}
const { item } = defineProps(
    {
        item: { type:Object }
    }
)
</script>
<template>
    <!-- nosubitem -->
    <el-menu-item :index="item.menu_url" v-if="!item.children" @click="handTag(item)">
        <el-icon><icon-menu /></el-icon>
        <span>{{ item.menu_name }}</span>
    </el-menu-item>
   <el-sub-menu :index="item.menu_url" v-else>
    <template #title>
        <el-icon><Document /></el-icon>
        <span>{{ item.menu_name }}</span>
      </template>
      <navitem v-for="sub in item.children" :key="sub.menu_url" :item="sub"/>
   </el-sub-menu>
        
    
</template>
<style scoped>
</style>