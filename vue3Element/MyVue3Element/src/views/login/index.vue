<script setup>
import { ref, reactive } from 'vue'
import { Connection, MessageBox } from '@element-plus/icons-vue'
import {login}  from '@/xhra/api'
import service  from '@/xhra/index'
import {userToken} from '@/utils/sessionstorage'
import {useCount} from '@/utils/useCount'
import {commonConsts} from '@/utils/commonConsts'
import router from   '@/router/index'
import md5 from 'md5'
const ruleFormRef = ref();
const {apiSuccesCode} = commonConsts();
const {setToken} = userToken();
const {count:n,set,reset,increase,decrease} = useCount(100);
const ruleForm = reactive({
    account: 'admin',
    password: 'admin@123'
});
const rules = reactive({
    account: [
        { required: true, message: 'Please input Activity name', trigger: 'blur' },
        { min: 3, max: 18, message: 'Length should be 3 to 18', trigger: 'blur' },
    ],
    password: [
        { required: true, message: 'Please input Activity name', trigger: 'blur' },
        { min: 3, max: 18, message: 'Length should be 3 to 18', trigger: 'blur' },
    ],
})
function submitForm(formEl) {
    formEl.validate(async valid => {
        if (valid) {

            ruleForm.password = md5(ruleForm.password);
           
            let res = await  login(ruleForm);
            let {code,data:{token}} = res.data;
            if(code===apiSuccesCode)
            {
                setToken(token);
                router.push('/home');
            }
        }
        else {
            console.log('submit! faile');
            return false;
        }
    })
}
</script>
<template>
    <div class="login">
        <div class="login-content">
            <div class="login-item login-box">
                <i>
                    <el-icon>
                        <Connection />
                    </el-icon>
                </i>
                <p>第一个</p>
            </div>
            <div class="login-item login-box">
                <i>
                    <el-icon>
                        <Connection />
                    </el-icon> </i>
                <p>第二个</p>
            </div>
            <div class="login-item login-form">
            <el-form ref="ruleFormRef" :model="ruleForm" status-icon :rules="rules" label-width="120px"
                class="demo-ruleForm">
                <el-form-item label="账号" prop="account">
                    <el-input v-model="ruleForm.account" type="text" autocomplete="off" />
                </el-form-item>
                <el-form-item label="密码" prop="password">
                    <el-input v-model="ruleForm.password" type="password" autocomplete="off" />
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="submitForm(ruleFormRef)">登录</el-button>
                </el-form-item>
                   <!-- <el-form-item>
                        <el-input v-model="n" />
                    </el-form-item>
                    <el-form-item>
                        <el-button type="success" @click="increase">
                            添加
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="success" @click="decrease">
                            减少
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="success" @click="set(77)">
                            设置
                        </el-button>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="success" @click="reset">
                            重置
                        </el-button>
                    </el-form-item> -->
            </el-form>
        </div>
            <div class="login-item login-box">
                <i>
                    <el-icon>
                        <MessageBox />
                    </el-icon> </i>
                <p>第三个</p>
            </div>
        </div>
    </div>
</template>
<style scoped></style>