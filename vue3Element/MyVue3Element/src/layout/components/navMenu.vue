<script setup>
import { ref, onMounted } from 'vue'
import { userInfo } from '@/xhra/api'
import { useRoute } from 'vue-router';
import { commonConsts } from '@/utils/commonConsts'
import NavItem from './navitem.vue';
const router = useRoute();
const { apiSuccesCode } = commonConsts();//解构
const items = ref([]);
onMounted(async () => {
    let { data } = await userInfo();
    console.log(data);
    if (data.code === apiSuccesCode) { //返回成功后才可以调用
        debugger
        var navDatas = data.data.module;
        // 第一层级
        navDatas.map(item => {
            if (item.father_id === 0) {
                items.value.push({ ...item });
            }
        });
        //其他层级 递归
        function CreateTree(pdata, sdata) {
            //两层循环
            for (let i = 0; i < pdata.length; i++) {
                //添加本层数据
                for (let j = 0; j < sdata.length; j++) {
                    if (pdata[i].id === sdata[j].father_id) {
                        if (!pdata[i].children) {
                            pdata[i].children = [];
                        }
                        pdata[i].children.push({ ...sdata[j] })
                    }
                }
                //有子层级添加下一层数据
                if (pdata[i].children) {
                    CreateTree(pdata[i].children, sdata);
                }
            }
        }
        CreateTree(items.value, navDatas);
    }
});
</script>
<template>
    <el-aside width="200px" class="aside">
        <el-menu :default-active="router.path" class="el-menu-vertical-demo" router>
            <NavItem v-for="v in items" :key="v.menu_url" :item="v" />
        </el-menu>
    </el-aside>
</template>
<style scoped></style>