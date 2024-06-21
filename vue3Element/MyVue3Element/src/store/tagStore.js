
import { defineStore} from 'pinia'
export const tagStore = defineStore('tag',{
    state:()=>({
        tagList:[{menu_name:"首页",menu_url:'/index',name:'index'}]
    }),
    getters:{
        getTagList:state => state.tagList
    },
    actions:{
        addTag:item=>this.tagList.push(item),
        delTag:i=>this.tagList.splice(i,1)
    }
})