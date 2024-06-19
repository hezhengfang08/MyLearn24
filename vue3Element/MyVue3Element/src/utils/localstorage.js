//token 专用
//闭包
export const userToken = function()
{
    const tokenKey = 'token';
     const setToken = token => localStorage.setItem(tokenKey,token);
     const getToken = ()=> localStorage.getItem(tokenKey);
     const removeToken = ()=>localStorage.removeItem(tokenKey);
     return {
        setToken,
        getToken,
        removeToken

     }
}
//通用的
export const localManger = function()
{
   
     const setKey = (key,value) => localStorage.setItem(key,value);
     const getKey = key=> localStorage.getItem(key);
     const removeKey = key=>localStorage.removeItem(key);
     return {
        setKey,
        getKey,
        removeKey
     }
}