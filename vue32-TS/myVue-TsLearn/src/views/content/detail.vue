<template>
    
    <div>
        <div class="other-header">
            <img :src="imgData.length>0?imgData[0].image_url : img1">
            <button class="go-back" @click="goBack()">返回</button>
        </div>
        <div class="other-title">
            <div class="title-text" v-for="v in infoData" :key="v.product_id">
                <p><i>￥</i>{{v.product_uprice}}</p>
                <p>{{v.product_name}}</p>
                <p>{{v.product_detail}}</p>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import {ref,onMounted, onUnmounted} from 'vue'
import {useRouter,useRoute} from 'vue-router' //导入路由
import {useStore} from 'vuex'
import {getDetails} from '@/api/http'
import {IDetailImg,IDetailInfo} from '@/utils/dataType'
import * as Types from '@/store/action-type'
import img1 from '@/assets/image/bj2.png' 

const router = useRouter();
const route = useRoute();
const store = useStore();  //定义store
const imgData = ref<IDetailImg[]>([]);
const infoData = ref<IDetailInfo[]>([]);
function getData(id:number) {
    getDetails<[IDetailImg[],IDetailInfo[]]>({mId:id}).then(res=>{
        imgData.value = res.data[0];
        infoData.value = res.data[1];
    })
}
onMounted(()=>{  //挂载完成
    store.dispatch(`home/${Types.SET_BOTTOM_NAV}`,false);  //触发actions  作用：底部导航隐藏
    getData(Number(route.params.id));  //类型转换
})

onUnmounted(()=>{  //销毁
    store.dispatch(`home/${Types.SET_BOTTOM_NAV}`,true);   //触发actions  作用：底部导航显示
})

function goBack(){
    //router.push('/home');
    router.go(-1); //返回上一步
}

</script>