import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
import Components from 'unplugin-vue-components/vite'
import { ElementPlusResolver } from 'unplugin-vue-components/resolvers'
// https://vitejs.dev/confElementUiResolverig/
export default defineConfig({
  server: {
    port: 8088,
    open: true,
  },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "src")
    }

  },
  plugins: [vue(),
  Components({
    resolvers: [ElementPlusResolver()]
  })
  ]
})
