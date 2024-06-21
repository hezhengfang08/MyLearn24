<script setup>
import { ref, defineProps } from 'vue'
import {
  Document,
  Menu as IconMenu,
  Location,
  Setting,
} from '@element-plus/icons-vue'
import {get} from '@/store/tagStore'
import { storeToRefs } from 'pinia';

const store = tagSotre();
const {getTagList} = storeToRefs(store);

 const handTag = tag => {
   if(getTagList.any(v=>v.menu_url=== tag.menu_url ))
   {
    return;
   }
    store.add(tag)
}
const { item } = defineProps(
    {
        item: { type:Object }
    }
)
</script>
<template>
    <!-- nosubitem -->
    <el-menu-item :index="item.menu_url" v-if="!item.children">
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