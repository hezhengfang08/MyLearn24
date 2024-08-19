import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  server: {
      open: true, // 是否在浏览器打开
      port: 8088, // 端口
   },
  resolve:{
    alias:{
      "@":path.resolve(__dirname,"src"),
    }
  }
})
