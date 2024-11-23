<script setup>
import { ref } from 'vue';
import AMapLoader from '@amap/amap-jsapi-loader';
import apiKey from '@/utils/config'
let map = ref(null);
let lnglat = ref('')
function initMap() {
    AMapLoader.load({
        key: apiKey
    }).then(
        AMAP => {
            map.value = new AMAP.Map('container', {
                resizeEnable: true, //是否监控地图容器尺寸变化
                zoom: 11, //初始化地图层级
                center: [116.397428, 39.90923] //初始化地图中心点
            });
            //地图加载完成
            map.value.on('complete',function(){
                alert('地图加载完成');
            });
            //获取经纬度
            map.value.on('click',function(e){
                lnglat.value = e.lnglat.getLng()+','+e.lnglat.getLat();
               
            });

        } )
        .catch(err => {
      console.log(err)
    });
}
initMap();

</script>
<template>
    <div id='container'></div>
  <div class="input-card">
      <h4>左击地图获取经纬度：</h4>
      <div class="input-item">
        <input type="text" readonly="true" v-model="lnglat" placeholder="左击地图获取
经纬度">
        </div>
  </div>
</template>
<style scoped>
body,
#container {
  width: 100%;
  height: 100%;
}
</style>