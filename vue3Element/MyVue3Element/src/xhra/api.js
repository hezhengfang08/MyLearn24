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

//权限管理-添加操作员
export const createUser = data =>http.post('user/createUser',data);

//权限管理-操作员列表
export const userList = data =>http.get('user/list',data);

//权限管理-删除操作员
export const removeUser = id =>http.delete(`user/removeUser/${id}`);
//权限管理-查找所有目录
export const findModules = data =>http.get('permission/findModules',data);

//权限管理-获取所有操作员
export const getOpers = data =>http.get('user/opers',data);