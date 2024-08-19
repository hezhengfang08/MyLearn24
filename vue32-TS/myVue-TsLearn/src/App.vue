<script setup lang="ts">
import NavBottom from './views/NavBottom.vue'
import {useStore} from 'vuex'
const store = useStore();  //定义store
</script>

<template>
 <RouterView></RouterView>
 <NavBottom v-if="store.state.home.navState"/>
</template>

<style scoped>
@import './assets/css/common.css'
</style>
