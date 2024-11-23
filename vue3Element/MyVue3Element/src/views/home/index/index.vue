<script setup>
import { ref } from 'vue'
import {
    Connection,
    MessageBox,
} from '@element-plus/icons-vue'
import Chart from './chartTemplate.vue'
import ChinaMap from './myMap.vue'

const lists = ref([
  {title:'电量监控',icon:Connection},
  {title:'电量监控',icon:Connection},
  {title:'电量监控',icon:Connection},
  {title:'电量监控',icon:Connection},
  {title:'电量监控',icon:Connection},
])

const option1 = {
  xAxis: {
    type: 'category',
    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
  },
  yAxis: {
    type: 'value'
  },
  series: [
    {
      data: [750, 230, 224, 218, 135, 147, 960],
      type: 'line'
    }
  ]
};
</script>

<template>
  <!-- 第一行 -->
  <div class="module-box">
    <div class="box" style="flex:0 1 20%;margin:5px;" v-for="(v,i) in lists" :key="i">
      <div class="list-box">
        <i><el-icon><component :is="v.icon"></component></el-icon></i>
        <p>{{ v.title }}</p>
      </div>
    </div>
  </div>

  <!-- 第二行 -->
  <div class="module-box">
    <div style="flex:0 1 60%;margin:10px;">
      <el-card class="box-card">
        <ChinaMap />
      </el-card>
    </div>

    <div style="flex:0 1 40%;margin:5px;">
      <div style="flex:0 1 100%;margin:5px;">
        <el-card class="box-card">
          <Chart :option="option1" :width='"100%"' :height='"200px"' />
        </el-card>
      </div>

      <div style="flex:0 1 100%;margin:5px;">
        <el-card class="box-card">
          <Chart :option="option1" :width='"100%"' :height='"200px"' />
        </el-card>
      </div>
    </div>
  </div>

  <!-- 第三行 -->
  <div class="module-box">
    <div style="flex:0 1 30%;margin:5px;">
      <el-card class="box-card">
        <Chart :option="option1" :width='"100%"' :height='"220px"' />
      </el-card>
    </div>

    <div style="flex:0 1 70%;margin:5px;">
      <el-card class="box-card">
        <Chart :option="option1" :width='"100%"' :height='"220px"' />
      </el-card>
    </div>
  </div>
</template>

<style scoped>

</style>
