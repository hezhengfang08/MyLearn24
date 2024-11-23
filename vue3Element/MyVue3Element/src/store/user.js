import {defineStore} from 'pinia'

export const userStore = defineStore('user',{
    state:()=>({
       uid:0, //用户id
    }),
    actions:{
        setUid(id){
            this.uid = id;
        }
    },
    //持久化
    persist:{
        enabled:true
    }
})