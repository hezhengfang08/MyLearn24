<script setup>
import { ref } from 'vue'
const msg = ref("头部导航");
</script>
<template>
    <el-tag
      v-for="tag in dynamicTags"
      :key="tag"
      closable
      :disable-transitions="false"
      @close="handleClose(tag)"
    >
      {{ tag }}
    </el-tag>
</template>
<style scoped></style>