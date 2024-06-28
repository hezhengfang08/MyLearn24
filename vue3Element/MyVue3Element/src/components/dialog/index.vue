<script setup>
import { ref, defineProps, defineEmits } from 'vue'
const props = defineProps({
    dialogHeader: {
        type: String,
        default: "开窗标题"
    },
    btnText: {
        type: String,
        default: "确定"
    },
    visible: {
        type: Boolean,
        default: false
    }
});
const emit = defineEmits(['confirm','update:visible'])
// 取消触发
const cancel = ()=>{
    emit('update:visible',false);
}
// 确定出发
const confirm = ()=>{
    emit('confirm');
}
</script>
<template>
    <el-dialog 
    v-model="props.visible" 
    :title="props.dialogHeader" width="500" :before-close="cancel">
        <slot ><p>弹框内容</p></slot>
        <template #footer>
            <div class="dialog-footer">
                <el-button @click="cancel = false">取消</el-button>
                <el-button type="primary" @click="confirm = false">
                  {{props.btnText}}
                </el-button>
            </div>
        </template>
    </el-dialog>
</template>
<style scoped></style>