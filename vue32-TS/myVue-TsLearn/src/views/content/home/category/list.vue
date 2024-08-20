<template>
    <div class="c-wrap">
        <ul>
            <li v-for="v in listsData" :key="v.product_id" >
                <div class="c-wrap-img">
                    <router-link :to="'/detail/'+v.product_id">
                        <img :src="v.product_img_url"/>
                    </router-link>
                </div>
                <h4>{{v.product_name}}</h4>
                <div class="c-wrap-info">
                    <i>￥</i>
                    <b>{{v.product_uprice}}</b>
                </div>
            </li>
        </ul>
    </div>

</template>

<script setup lang="ts">
import {ICategorygoods} from '@/utils/dataType'

let {listsData} = defineProps({
    listsData:{
        type:Array as ()=> ICategorygoods[],  //类型断言   可以手动指定一个值的类型
        default:()=>[]
    }
})

//类型断言   可以手动指定一个值的类型
//语法： 值 as 类型

// let str:any = 'iti';
// let len = (str as string).length;



</script>