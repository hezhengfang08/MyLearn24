<template>
    <div class="header">
        <!-- 图片加载的几种方式 -->
        <!-- <img src="@/assets/image/bj1.png" />
        <img :src="img1" />
        <img :src="`/src/assets/image/${img2}`" /> -->
        <!-- <img v-for="(v,i) in swiperArr" :key="i" 
            :src="`/src/assets/image/${v}`" 
            v-show="i==n"/> -->
        <!-- 轮播 异步组件-->
        <Suspense>
            <template #default>
                <Swiper />
            </template>
            <template #fallback>
                <div>loading...</div>
            </template>
        </Suspense>
        
    </div>
    <div class="title">
        <ul>
            <li v-for="(v,i) in _title" :key="i">{{v}}</li>
        </ul>
    </div>
</template>

<script setup lang="ts">
import {ref,} from 'vue'
// import img1 from '@/assets/image/bj2.png' 
import Swiper from './swiper.vue'

// const img2 = ref<string>('bj1.png');
const _title = ref<string[]>(['七日退换','官方正品','29元包邮']); //标题数据



</script>