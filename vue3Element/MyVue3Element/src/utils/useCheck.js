import { computed } from 'vue'
export const useCheck = function(){
  
    var filterCheck = computed(() => v=>{
        switch(v){
            case true:
                return '正常'
            case false:
                return '异常'  
            default:
                return '异常'  
        }
    });
    return{
        filterCheck
    }
}
