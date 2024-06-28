import {computed} from 'vue'
export const useDate = function()
{
   const filterDate =computed(()=> v=>{
    const d = new Date(v);
    const dd = d.toLocaleDateString();
    return dd;
   });
    return{
        filterDate
    }
}