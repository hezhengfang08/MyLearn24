<script setup>
import { ref, defineProps, defineEmits } from 'vue'
const props = defineProps({
    currentPage: {
        type: Number,
        default: 1
    },
    pageSize: {
        type: Number,
        default: 10
    },
    pageSizes: {
        type: Array,
        default:()=> [10, 20, 30, 40]
    },
    layout: {
        type: String,
        default: 'total,sizes, prev, pager, next,jumper'
    },
    total: {
        type: Number,
        default: 1
    },
});
const emit = defineEmits(['handleChange', 'update:pageSize','update:currentPage'])

const handleSizeChange = (size) => {
    emit('update:pageSize', size)
    emit('handleChange');

}
const handleCurrentChange = (index) => {
    emit('update:currentPage', index)
    emit('handleChange');

}

</script>
<template>
   <!-- 分页 -->
    <el-pagination v-model:current-page="props.currentPage" v-model:page-size="props.pageSize"
        :page-sizes="props.pageSizes" :background="props.background" :layout="props.layout" :total="props.total"
        @size-change="handleSizeChange" @current-change="handleCurrentChange" />
</template>
<style scoped></style>