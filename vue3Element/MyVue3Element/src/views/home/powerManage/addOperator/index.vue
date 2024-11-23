<script setup>
import { ref ,reactive} from 'vue'
import {createUser} from '@/xhra/api'
import md5 from 'md5'
import {useRouter} from 'vue-router'

const router = useRouter();
const ruleFormRef = ref();
const ruleForm = reactive({
  account:'',
  password:'',
  password2:''
});
//自定义校验
const validatePass2 = (rule,value,callback)=>{
  if(value===''){
    callback(new Error('请两次输入密码'))
  }else if(value !== ruleForm.password){
    callback(new Error('两次密码不一致'))
  } else {
    callback()
  }
}
const rules = reactive({
  account: [
    { required: true, message: '请输入用户名', trigger: 'blur' },
    { min: 2, max: 18, message: 'Length should be 2 to 18', trigger: 'blur' },
  ],
  password: [
    { required: true, message: '请输入密码', trigger: 'blur' },
    { min: 3, max: 18, message: 'Length should be 3 to 18', trigger: 'blur' },
  ],
  password2: [
    { required: true, validator: validatePass2, trigger: 'blur' },
  ],
})

//添加
const submitForm = (formEl) => {

  if (!formEl) return
  formEl.validate(async(valid) => {
    if (valid) {
      //发送请求
      ruleForm.password = md5(ruleForm.password);
      ruleForm.password2 = md5(ruleForm.password2);
      //需要删除password2
      let res = await createUser(ruleForm);
      console.log(res);
      //判断状态
      //跳转
      router.push('/powerManage/operatorList');
    } else {
      console.log('error submit!')
      return false
    }
  })
}
</script>

<template>
  <el-card class="box-card marginBottom">
    <p>添加操作员</p>
  </el-card>

  <el-card class="box-card">
    <el-form
      ref="ruleFormRef"
      :model="ruleForm"
      status-icon
      :rules="rules"
      label-width="120px"
      class="demo-ruleForm"
    >
      <el-form-item label="用户名" prop="account">
        <el-input v-model ="ruleForm.account" placeholder="用户名"/>
      </el-form-item>

      <el-form-item label="密码" prop="password">
        <el-input v-model="ruleForm.password" type="password" autocomplete="off" placeholder="密码"/>
      </el-form-item>

      <el-form-item label="确认密码" prop="password2">
        <el-input
          v-model="ruleForm.password2"
          type="password"
          autocomplete="off"
          placeholder="确认密码"
        />
      </el-form-item>
      
      <el-form-item>
        <el-button type="primary" @click="submitForm(ruleFormRef)"
          >添加</el-button>
      </el-form-item>
    </el-form>
  </el-card>
</template>

<style scoped>

</style>
