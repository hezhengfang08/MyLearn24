<script setup>
import { ref, onMounted } from 'vue'
import { getCarList } from '@/xhra/api'
import { useDate } from '@/utils/useDate'
import { useCheck } from '@/utils/useCheck'
import myDialog from '@/components/dialog/index.vue'
const carList = ref([]);
const category = ref('')
const tempData = ref({});
const { filterDate } = useDate();
const { filterCheck } = useCheck();
const isShowDialog = ref(false);
const listQuery = ref({
  pageNo: 1,
  pageSize: 10
})
onMounted(() => {
  getDataList();
})
async function getDataList() {

  const res = await getCarList(listQuery.value);
  const { code, data: { list } } = res.data;
  carList.value = list;
  console.log(code);
  console.log(list);
}
function handleCarInfo(tp, row) {
  category.value = tp;
  isShowDialog.value = true;
  tp == 'add' ? tempData.value = {} : tempData.value = { ...row }
}

</script>

<template>
  <el-card class="el-card marginBottom">
    <el-button type="success" @click="handleCarInfo('add', '')">新增车辆</el-button>
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
          <el-button type="primary" @click="handleCarInfo('update',row)">编辑</el-button>
          <el-button type="danger" >删除</el-button>
        </template>
      </el-table-column>
    </el-table>
  </el-card>
  <Dialog v-model:visible="isShowDialog" :dialogHeader="'新增车辆'" :btnText="'新增'">
    <p>是否新增车辆</p>
  </Dialog>
</template>

<style scoped></style>
