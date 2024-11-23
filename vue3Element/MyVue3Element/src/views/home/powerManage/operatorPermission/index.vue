<script setup>
import { ref, onMounted } from 'vue'
import { findModules, getOpers,findModulesByUid,updatePermission } from '@/xhra/api'
import { commonConsts } from '@/utils/commonConsts.js'
import {createTree} from '@/utils/myFn.js'
let { apiSuccesCode } = commonConsts();
let opersList = ref([]);
let userId = ref('');
let menuList = ref([]);
let userName = ref('');
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
//组装tree结构的数据
 function handleTree(menu,data){
  data.map(item => {
    if(item.father_id ===0){
      menu.push({...item});
    }
  });
  createTree(menu,data);
}
async function handleUpdate(row,v)
{ 
  if(!userId.value) return;
  let data= {
    uid:userId.value,
    moduleJsonStr:JSON.stringify({mid:row.id, permission:[v]})
  }
 let res = await updatePermission(data);
}

(function() {
    // 函数体
    alert(1);
})();
// (function(){
//   //要立即执行的语句函数体
// })();
//箭头函数（Arrow Function）:
const handleCommand2 = async(command)=>{
  userId.value = command.id;
  userName.value = command.account; //操作员名称
  let res = await findModulesByUid({id:command.id});
  menuList.value = [];
  //数据结构转换
  handleTree(menuList.value,res.data.data.list);
  console.log(menuList.value);
}
//选择操作员
async function handleCommand(command){
  userId.value = command.id;
  userName.value = command.account; //操作员名称
  let res = await findModulesByUid({id:command.id});
  menuList.value = [];
  //数据结构转换
  handleTree(menuList.value,res.data.data.list);
  console.log(menuList.value);
}
</script>

<template>
  <el-card class="box-card marginBottom">
    <el-dropdown @command="handleCommand2">
      <el-button class="primary">
       {{userId? userName:"选择操作员"}} <el-icon class="el-icon--right"><arrow-down /></el-icon>
      </el-button>
      <template #dropdown>
        <el-dropdown-menu>
          <el-dropdown-item v-for="v in opersList" :command="v">{{ v.account }}</el-dropdown-item>
        </el-dropdown-menu>
      </template>
    </el-dropdown>
  </el-card>
  <el-card class="box-card">
    <el-table :data="menuList" style="width: 100%; margin-bottom: 20px" row-key="id" border default-expand-all>
      <el-table-column prop="menu_name" label="菜单目录" />
      <el-table-column prop="permission" label="授权">
        <template #default="{ row }">
          <el-checkbox-group v-model="row.userPermission">
            <el-checkbox v-for="(v, i) in row.permission" :disabled="!userId" :key="i" :label="v.label" :value="v.label" @change="handleUpdate(row,v)" />
          </el-checkbox-group>
        </template>
      </el-table-column>
    </el-table>
  </el-card>
</template>

<style scoped></style>
