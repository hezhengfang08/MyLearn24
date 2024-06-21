<script setup>
import { ref, defineProps } from 'vue'
import { Menu as IconMenu, Document, Location, Setting } from '@element-plus/icons-vue';
const { item } = defineProps(
    {
        item: { type:Object }
    }
)
</script>
<template>
    <!-- nosubitem -->
    <el-menu-item :index="item.menu_url" v-if="!item.children">
        <el-icon>
            <icon-menu/>
        </el-icon>
        <span>{{ item.menu_name }}</span>
    </el-menu-item>
   <el-sub-item :index="item.menu_url" v-else>
    <template #title>
        <el-icon><icon-menu/></el-icon>
        <span>{{ item.menu_name }}</span>
      </template>
      <navitem v-for="sub in item.children" :key="sub.menu_url" :item="sub"/>
   </el-sub-item>
        
    
</template>
<style scoped>
</style>