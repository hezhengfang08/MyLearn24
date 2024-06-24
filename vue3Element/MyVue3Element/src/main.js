import { createApp } from 'vue'
import './style.css'
import 'element-plus/dist/index.css'
import { createPinia } from 'pinia'
import piniaPluginPersist from 'pinia-plugin-persist'
import App from './App.vue'
import router from './router'
const pinia = createPinia();
pinia.use(piniaPluginPersist);
createApp(App)
    .use(pinia)
    .use(router)
    .mount('#app')
