import { da } from "element-plus/es/locales.mjs";
import http from "./http";

export const login = data => http.post('user/login',data);
export const userInfo = () => http.get('user/info');
export const getCarList = data => http.get('car/list',data);
