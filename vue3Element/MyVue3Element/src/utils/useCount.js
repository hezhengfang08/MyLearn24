import {ref,watch} from 'vue'
export const useCount = function(init)
{
    const count = ref(init);
    const increase = ()=> count.value +=1;
    const decrease = ()=> count.value -=1;
    const reset = ()=> count.value = init;
    const set = val => count.value = val;
    watch(count,(newVal,oldVal)=>{
console.log(newVal);
    });

    return{
        count,
        increase,
        decrease,
        reset,
        set
    }
}