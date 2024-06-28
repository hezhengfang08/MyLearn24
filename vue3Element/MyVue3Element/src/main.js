import { createApp } from 'vue'
import './style.css'
import 'element-plus/dist/index.css'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate   from 'pinia-plugin-persistedstate' //持久化 之前的插件pinia-plugin-persist不支持前后存储方式了
import App from './App.vue'
import router from './router'
const pinia = createPinia();
pinia.use(piniaPluginPersistedstate );
createApp(App)
    .use(pinia)
    .use(router)
    .mount('#app')
