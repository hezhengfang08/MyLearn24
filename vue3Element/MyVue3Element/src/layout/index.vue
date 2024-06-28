<script setup>
import { ref } from 'vue'
import { NavMenu, Tag, Breadcrumb } from './components/index'
import {useRouter} from 'vue-router'
const router = useRouter();
const msg = ref("首页");

async function logout()
{
alert(1);
router.push('/login')
}
</script>
<template>
    <div id="module">
        <el-container>
            <!-- 左侧导航 -->
            <NavMenu />
            <el-container>
                <el-header>
                    <!-- 面包屑 -->
                    <Breadcrumb />
                    <el-button class="exit" type="primary" @click="logout">退出</el-button>
                    <!-- 多页签导航 -->
                    <Tag />
                </el-header>
                <el-main>
                    <!-- 右侧主体 -->
                    <router-view />
                </el-main>
            </el-container>
        </el-container>
    </div>
</template>

<style scoped></style>