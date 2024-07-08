<script setup>
import { ref ,reactive} from 'vue';
import AMapLoader from '@amap/amap-jsapi-loader';
import apiConfig from '@/utils/config'
var marker, lineArr = [[116.478935, 39.997761], [116.478939, 39.997825], [116.478912, 39.998549], [116.478912, 39.998549], [116.478998, 39.998555], [116.478998, 39.998555], [116.479282, 39.99856], [116.479658, 39.998528], [116.480151, 39.998453], [116.480784, 39.998302], [116.480784, 39.998302], [116.481149, 39.998184], [116.481573, 39.997997], [116.481863, 39.997846], [116.482072, 39.997718], [116.482362, 39.997718], [116.483633, 39.998935], [116.48367, 39.998968], [116.484648, 39.999861]];
let apiKey = apiConfig.apiKey;
let map = ref(null);
function initMap() {
    AMapLoader.load({
        key: apiKey, // 申请好的Web端开发者Key，首次调用 load 时必填
    })
        .then((AMap) => {
            map = new AMap.Map("container", {
                // 设置地图容器id
                viewMode: "3D", // 是否为3D地图模式
                zoom: 11, // 初始化地图级别
                center: [116.397428, 39.90923], // 初始化地图中心点位置
            });
            marker = new AMap.Marker({
                map: map,
                position: [116.478935, 39.997761],
                icon: "https://webapi.amap.com/images/car.png",
                offset: new AMap.Pixel(-26, -13),
                autoRotation: true,
                angle: -90,
            });
            // 绘制轨迹 轨迹颜色
            var polyline = new AMap.Polyline({
                map: map,
                path: lineArr,
                showDir: true,
                strokeColor: "#28F",  //线颜色
                strokeWeight: 6,      //线宽
            });

            //轨迹回放颜色
            var passedPolyline = new AMap.Polyline({
                map: map,
                strokeColor: "#f60",  //线颜色
                strokeWeight: 6,      //线宽
            });
            ////设置移动过的路线  监听车辆移动事件
            marker.on('moving', function (e) {
                // 延长驾驶途径过的轨迹
                passedPolyline.setPath(e.passedPath);
            });
            //视野居中
            map.setFitView();
        })
        .catch((e) => {
            console.log(e);
        });
}
initMap();
//开始
function start(){
  //设置移动路线及速度,
  marker.moveAlong(lineArr, 100); 
}
//暂停
function pause(){
  marker.pauseMove(); 
}
//继续
function resume(){
  marker.resumeMove(); 
}



</script>
<template>
    <div id="container"></div>
  <div class="input-card">
      <h4>轨迹和回放控制</h4>
      <div class="input-item">
        <input type="button" class="btn" value="开始" @click="start" />
        <input type="button" class="btn" value="暂停" @click="pause" />
        <input type="button" class="btn" value="继续" @click="resume" />
      </div>
  </div>
</template>
<style scoped>
 html, body, #container {
      height: 100%;
      width: 100%;
  }
</style>