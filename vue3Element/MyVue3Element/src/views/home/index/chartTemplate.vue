<script setup>
import { ref, onMounted, defineProps, computed } from 'vue'
import * as echarts from 'echarts'


//供父模块调用
const { width, height, option } = defineProps(
    {
        width: {
            type: String,
            default: '100%'
        },
        height: {
            type: String,
            default: '100%'
        },
        option: {
            type: Object,
            default: {
                xAxis: {
                    type: 'category',
                    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
                },
                yAxis: {
                    type: 'value'
                },
                series: [
                    {
                        data: [150, 230, 224, 218, 135, 147, 260],
                        type: 'line'
                    }
                ]
            }
        }
    }
)
onMounted(() => {
    InitChart();
});
const chart = ref();
//计算属性设置样式，注意写法
const style = computed(() => ({ width, height }));
const InitChart = () => {
    const myChart = echarts.init(chart.value);
    myChart.setOption(option);
    window.addEventListener('resize', () => {
        myChart.resize();
    });
}
</script>
<template>
    <div ref="chart" :style="style"></div>
</template>
<style scoped></style>