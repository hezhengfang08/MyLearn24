import { da } from "element-plus/es/locales.mjs";
import http from "./http";

export const login = data => http.post('user/login',data);
export const userInfo = () => http.post('user/info');
