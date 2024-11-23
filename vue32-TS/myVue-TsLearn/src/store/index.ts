import { createStore } from 'vuex'
import { IGlobalState } from '../utils/dataType'
import home from './modules/home'
export default createStore<IGlobalState>({
 // state: {  //每个模块中单独的定义
    //   
    // },
mutations:{

},
actions:{

},
modules:{
    home
}

})