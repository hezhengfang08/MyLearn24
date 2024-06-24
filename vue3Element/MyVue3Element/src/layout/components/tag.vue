<script setup>
import { ref, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { tagStore } from '@/store/tag'

const stroe = tagStore();
const { getTagList } = storeToRefs(stroe);
const routes = useRoute();
const router = useRouter();
const defualtUrl = ref("/index");

watch(routes, (newVal, oldVal) => {
  defualtUrl.value = newVal.path;
}, { deep: true, immediate: true });

function handleClose(tag,i) {
  stroe.delTag(i);
  if(routes.path !==tag.menu_url){
        return;
  }
  else{
    if(i==getTagList.value.length)
    {
           router.push(getTagList.value[i-1].menu_url);
    }
    else
    {
           router.push(getTagList.value[i].menu_url);
    }
  }
};

function handleClick(tag) {
  if (routes.path == tag.menu_url) {
    return;
  }
  router.push(tag.menu_url);

};

</script>
<template>
  <el-tag v-for="(tag, i) in getTagList" :key="tag.menu_url" :closable="tag.menu_url !== '/index'"
    :type="tag.menu_url === defualtUrl ? '' : 'info'" @close="handleClose(tag,i)" @click="handleClick(tag)">
    {{ tag.menu_name }}
  </el-tag>
</template>
<style scoped></style>