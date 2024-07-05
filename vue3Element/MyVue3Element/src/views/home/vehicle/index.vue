<script setup>
import { ref, onMounted } from 'vue'
import { getCarList,createOrUpdCarInfo,isExistCarNumber,deleteCarInfo } from '@/xhra/api'
import { useDate } from '@/utils/useDate'
import { useCheck } from '@/utils/useCheck'
import myDialog from '@/components/dialog/index.vue'
import myPagination from '@/components/pagination/index.vue'
import { ElNotification } from 'element-plus'
import {commonConsts} from '@/utils/commonConsts'
import useMenuPermission from '@/utils/useMenuPermission'  //vue-hooks

const {apiSuccesCode} = commonConsts();
const {menuStatus} = useMenuPermission();
const carList = ref([]);
let category = ref('')
let total = ref(1);
const tempData = ref({});
const { filterDate } = useDate();
const { filterCheck } = useCheck();
let isShowHandleDialog = ref(false);
let isShowDeleteDialog = ref(false);
let isExist = ref(false);
const listQuery = ref({
  pageNo: 1,
  pageSize: 10
})
onMounted(() => {
  getDataList();
})


async function judgExist(numberplate)
{
  var {data:{code,data:{exist}}} = await isExistCarNumber({numberplate});
  if(exist){
    isExist.value = true;
  }
}
async function handleConfirm(){
  isShowHandleDialog.value = false;
  let res = await createOrUpdCarInfo(tempData.value);
  debugger
if(res.data.code === apiSuccesCode)
{
 let sumbitText = tempData.value.id ==undefined ? "新增成功":"修改成功"
ElNotification({
      title: sumbitText,
      message: sumbitText,
      duration: 1000,
    });
  getDataList();
}
}
async function getDataList() {

  const res = await getCarList(listQuery.value);
  const { code, data: { list,rows } } = res.data;
  carList.value = list;
  total.value = rows;
  
}
function handleCarInfo(tp, row) {
  isExist.value = false; //打开时隐藏错误提示
  category.value = tp;
  isShowHandleDialog.value = true;
  tp == 'add' ? tempData.value = {} : tempData.value = { ...row }
}
async function handleDelete(row){
  isShowDeleteDialog.value = false;
 var {data:{code}} = await deleteCarInfo(tempData.value.id);
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
    <el-button type="success" @click="handleCarInfo('add', '')" :disabled="menuStatus('新增车辆')" >新增车辆</el-button>
  </el-card>
  <el-card class="box-card">
    <el-table :data="carList" style="width: 100%" :row-class-name="tableRowClassName">
      <el-table-column prop="number" label="车辆自编号" />
      <el-table-column prop="numberplate" label="车牌号" />
      <el-table-column prop="date" label="车辆出厂日期">
        <template #default="{ row }">
          {{ filterDate(row.date) }}
        </template>
      </el-table-column>
      <el-table-column prop="check" label="车辆状态">
        <template #default="{ row }">
          <el-tag class="ml-2" :type="row.check ? 'success' : 'danger'">{{ filterCheck(row.check) }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template #default="{row}">
          <el-button type="primary" @click="handleCarInfo('update',row)" :disabled="menuStatus('编辑')">编辑</el-button>
          <el-button type="danger" @click="showDeleteCarInfo(row)"  :disabled="menuStatus('删除')" >删除</el-button>
        </template>
      </el-table-column>
    </el-table>
     <myPagination  :total="total" v-model:currentPage="listQuery.pageNo" v-model:pageSize="listQuery.pageSize" @handleChange="getDataList" />
  </el-card>
  <Dialog v-model:visible="isShowHandleDialog" :dialogHeader=" category=='add'?
  '新增车辆':'编辑车辆'" :btnText=" category=='add'?'新增':'编辑'" @confirm="handleConfirm">
    <el-form :model="tempData" label-width="120px">
      <el-form-item label="车辆编号">
        <el-input v-model="tempData.number" />
      </el-form-item>
      <el-alert title="车牌号重复!" type="error" center show-icon v-show="isExist"/>
      <el-form-item label="车牌号">
        <el-input v-model="tempData.numberplate" :disabled="category=='add'?false:true" @change="judgExist(tempData.numberplate)" />
      </el-form-item>
      <el-form-item label="车辆出厂日期">
        <el-date-picker type="date" v-model="tempData.date"  />
      </el-form-item>
      <el-form-item label="车辆状态">
        <el-switch v-model="tempData.check" />
      </el-form-item>

    </el-form>
  </Dialog>
  <Dialog v-model:visible="isShowDeleteDialog" :dialogHeader="'删除车辆'" :btnText="'删除'" @confirm="handleDelete">
    <p>是否删除该车辆数据</p>
  </Dialog>
</template>

<style scoped></style>
