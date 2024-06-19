import { createApp } from 'vue'
import './style.css'
import 'element-plus/dist/index.css'
import {createPinia} from 'pinia'
import App from './App.vue'
import router from './router'
const pinia = createPinia();
createApp(App)
.use(pinia)
.use(router)
.mount('#app')
