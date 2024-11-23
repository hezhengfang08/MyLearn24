<script setup>
import { ref, onMounted } from 'vue'
import * as echarts from 'echarts'
import china from '@/assets/json/china.json'

const chart = ref(); //这个必须写在initChart函数外面
onMounted(() => {
    initChart();
}
)
function initChart() {

    const mychart = echarts.init(chart.value); //这个必须要一个.value
    echarts.registerMap('china', china);
    const option = {
        // geo: { //地理坐标系组件
        //     show: true,
        //     type: 'map',
        //     map: 'china',
        //     label: {
        //         show: true,
        //         color: '#fff'
        //     },
        //     itemStyle: {
        //         areaColor: '#219edb',
        //         borderColor: '#fff'
        //     },
        //     zoom: 1.2
        // },
        // series: [
        //     {
        //         type: 'effectScatter',
        //         coordinateSystem: 'geo',
        //         label: {
        //             formatter: '{b}',
        //             position: 'right',
        //             show: true
        //         },
        //         itemStyle: {
        //             color: '#f4e925',
        //             shadowBlur: 10,
        //             shadowColor: '#333'
        //         },
        //         symbol: 'circle',
        //         data: [
        //             {
        //                 name: '北京',
        //                 value: [116.46, 39.92],
        //             },
        //             {
        //                 name: '乌鲁木齐',
        //                 value: [87.68, 43.77],
        //             },
        //             {
        //                 name: '济南',
        //                 value: [117, 36.65],
        //             },
        //         ]
        //     },
        // ]
        series:[
          {
            type:'map',
            map:'china',
            label:{
              show:true,
              color:'#fff'
            },
            itemStyle:{
              areaColor:'#219edb',
              borderColor:'#fff'
            },
            zoom:1.2
          }
        ]
    }
    mychart.setOption(option);
    window.addEventListener('resize', () => {
        mychart.resize();
    });
}
</script>
<template>
    <div ref="chart" style="width: 100%; height: 400px;"></div>
</template>
<style scoped></style>