<script setup>
import { ref, onMounted } from 'vue'
import { findModules, getOpers } from '@/xhra/api'
import { commonConsts } from '@/utils/commonConsts.js'
import {CreateTree} from '@/utils/myFn.js'
let { apiSuccesCode } = commonConsts();
let opersList = ref([]);
let userId = ref('');
let menuList = ref([]);
const checkList = ref(['编辑','处理工单']);
onMounted(async () => {

  let { data: { code:code2, data: list2 } } = await findModules();
  if (code2 === apiSuccesCode) {
    handleTree(menuList.value, list2.list);
  }

  let { data: { code, data: list } } = await getOpers();
  if (code === apiSuccesCode) {
    opersList.value = list;
    menuList
  }
  debugger
})
 function handleTree(menu,data){
  data.map(item => {
    if(item.father_id ===0){
      menu.push({...item});
    }
  });
  CreateTree(menu,data);
}
const handleCommand = (command) => {

  userId.value = command;
}
</script>

<template>
  <el-card class="box-card marginBottom">
    <el-dropdown @command="handleCommand">
      <el-button class="primary">
        选择操作员<el-icon class="el-icon--right"><arrow-down /></el-icon>
      </el-button>
      <template #dropdown>
        <el-dropdown-menu>
          <el-dropdown-item v-for="v in opersList" :command="v.id">{{ v.account }}</el-dropdown-item>
        </el-dropdown-menu>
      </template>
    </el-dropdown>
  </el-card>
  <el-card class="box-card">
    <el-table :data="menuList" style="width: 100%; margin-bottom: 20px" row-key="id" border default-expand-all>
      <el-table-column prop="menu_name" label="菜单目录" />
      <el-table-column prop="permission" label="授权">
        <template #default="{ row }">
          <el-checkbox-group v-model="checkList">
            <el-checkbox v-for="(v, i) in row.permission" :disabled="!userId" :key="i" :label="v.label" />
          </el-checkbox-group>
        </template>
      </el-table-column>
    </el-table>
  </el-card>
</template>

<style scoped></style>
