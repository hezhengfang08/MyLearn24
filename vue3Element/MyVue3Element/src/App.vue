<script setup>
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>


    <el-button type="primary" size="large">Large</el-button>
    <el-button type="primary">Default</el-button>
    <el-button type="primary" size="small">Small</el-button>
    <el-button type="primary" size="large" :icon="Search">Search</el-button>
    <el-button type="primary" :icon="Search">Search</el-button>
    <el-button type="primary" size="small" :icon="Search">Search</el-button>


</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
