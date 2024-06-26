import { da } from "element-plus/es/locales.mjs";
import http from "./http";

export const login = data => http.post('user/login',data);
export const userInfo = () => http.get('user/info');
export const getCarList = data => http.get('car/list',data);
//车辆列表-添加OR 编辑
export const createOrUpdCarInfo = data =>http.post('car/createOrUpd',data);

//车辆列表-车牌校验
export const isExistCarNumber = data =>http.get('car/isExist',data);

//车辆列表-删除
export const deleteCarInfo = id =>http.delete(`car/delete/${id}`);