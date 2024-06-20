<script setup>
import { ref } from 'vue'
import { NavMenu, Tag, Breadcrumb } from './components/index'
const msg = ref("首页");
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
                    <el-button class="exit" type="primary">退出</el-button>
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