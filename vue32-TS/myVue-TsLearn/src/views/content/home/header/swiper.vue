<template>
    <img v-for="(v,i) in swiperArr" :key="v.id" 
            :src="v.img_url" 
            v-show="i==n"/>
</template>

<script setup lang="ts">
import {ref,onMounted, onUnmounted,computed} from 'vue'
//import {getSwiperImg} from '@/api/http'
import {IGlobalState} from '@/utils/dataType'
import {useStore,Store} from 'vuex' //导入store
import * as Types from '@/store/action-type'

//'@/utils/dataType'  TS中不能识别@/ 解决方式

 //动态获取轮播数据
//const swiperArr = await getSwiperImg<ISwiper[]>().then(res=>res.data);
// export defalut definedComponent({
//     async setup(){
//         const swiperArr = await getSwiperImg().then(res=>res.data);
//         return swiperArr
//     }
// })
//vuex数据缓存
const store = useStore<IGlobalState>();
let {swiperArr} = setData(store);

function setData(store:Store<IGlobalState>){
    let swiperArr = computed(()=>store.state.home.swiper); //获取swiper
    //加载初始化数
    onMounted(()=>{
        if(swiperArr.value.length==0){  //如果store中swiper有直接获取
            store.dispatch(`home/${Types.SET_SWIPER_LIST}`)
        }
    });
    return {
        swiperArr
    }
}

const n = ref<number>(0); //初始化
const timer=ref<any>(null);

function autoPlay(){
    n.value++;
    if(n.value==swiperArr.value.length){
        n.value = 0;
    }
}
const play=()=>{
    timer.value = setInterval(autoPlay,2000)
}
//挂载完成
onMounted(()=>{
    play();
})
//销毁
onUnmounted(()=>{
    clearInterval(timer.value);
})

</script>