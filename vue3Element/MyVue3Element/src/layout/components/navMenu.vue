<script setup>
import { ref,onMounted } from 'vue'
import {userInfo} from '@/xhra/api'
import { useRoute } from 'vue-router';

// import navitem from './navitem.vue';
const route = useRoute();

onMounted(async ()=>{
    let {data} = await userInfo();
    console.log(data);
});
</script>
<template>
    <h1>{{ msg }}</h1>
</template>
<style scoped>
</style>