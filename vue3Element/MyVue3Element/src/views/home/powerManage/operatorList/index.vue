<script setup>
import { ref, onMounted } from 'vue'
import { userList,removeUser } from '@/xhra/api'
import { useDate } from '@/utils/useDate'
import myDialog from '@/components/dialog/index.vue'
import myPagination from '@/components/pagination/index.vue'
import { ElNotification } from 'element-plus'
import {commonConsts} from '@/utils/commonConsts'

const {apiSuccesCode} = commonConsts();
const userLists = ref([]);
let category = ref('')
let total = ref(1);
const tempData = ref({});
const { filterDate } = useDate();
let isShowDeleteDialog = ref(false);
let isExist = ref(false);
const listQuery = ref({
  pageNo: 1,
  pageSize: 10
})
onMounted(() => {
  getDataList();
})

async function getDataList() {
  const res = await userList(listQuery.value);
  debugger
  const { code, data: { list,rows } } = res.data;
  userLists.value = list;
  total.value = rows; 
}

async function handleDelete(row){
  isShowDeleteDialog.value = false;
 var {data:{code}} = await removeUser(tempData.value.id);
 if(code === apiSuccesCode){
  ElNotification({
      title: '删除成功',
      message: '删除成功',
      duration: 1000,
    });
    getDataList();
 }
}
async function showDeleteCarInfo(row){
  isShowDeleteDialog.value = true;
  tempData.value = {...row};
}

</script>

<template>
  <el-card class="el-card marginBottom">
    <p>操作员列表</p>
  </el-card>
  <el-card class="box-card">
    <el-table :data="userLists" style="width: 100%">
      <el-table-column prop="account" label="用户名" />
      <el-table-column prop="password" label="密码" />
      <el-table-column prop="reg_time" label="创建时间" >
        <template #default="{row}">
          {{filterDate(row.reg_time)}}
        </template>
      </el-table-column>

      <el-table-column label="操作" >
        <template #default="{row}">
          <el-button type="danger" @click="showDeleteCarInfo(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

     <myPagination  :total="total" v-model:currentPage="listQuery.pageNo" v-model:pageSize="listQuery.pageSize" @handleChange="getDataList" />
  </el-card>
  
  <Dialog v-model:visible="isShowDeleteDialog" :dialogHeader="'删除操作员'" :btnText="'删除'" @confirm="handleDelete">
    <p>是否删除该用户数据</p>
  </Dialog>
</template>

<style scoped></style>
