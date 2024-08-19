import {Module} from 'vuex'
import { IGlobalState,IHomeState,ISwiper } from '@/utils/dataType'
import * as ActionTypes from '../action-type'
import { getSwiperImg } from '@/api/http'
const state:IHomeState = {
    swiper:[],
    navState:true
}
const home:Module<IHomeState,IGlobalState> = {
    namespaced:true,
    state,
    mutations:{
         //轮播数据
         [ActionTypes.SET_SWIPER_LIST](state,payload:ISwiper[]){
            state.swiper = payload
         },
          //底部导航数据
          [ActionTypes.SET_BOTTOM_NAV](state,payload:boolean){
            state.navState = payload
          }
    },
    actions:{
         //轮播数据
         async [ActionTypes.SET_SWIPER_LIST]({commit}){
            let swiperData = await getSwiperImg<ISwiper[]>();
            commit(ActionTypes.SET_SWIPER_LIST,swiperData.data)
         },
         //底部导航数据
         [ActionTypes.SET_BOTTOM_NAV]({commit},status)
         {
                   commit(ActionTypes.SET_BOTTOM_NAV,status)
         }
    }
}
export default home