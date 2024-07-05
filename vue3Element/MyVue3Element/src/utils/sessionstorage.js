//token 专用 hooks
//闭包
export const userToken = function()
{
    const tokenKey = 'token';
     const setToken = token => sessionStorage.setItem(tokenKey,token);
     const getToken = ()=> sessionStorage.getItem(tokenKey);
     const removeToken = ()=>sessionStorage.removeItem(tokenKey);
     return {
        setToken,
        getToken,
        removeToken

     }
}
//通用的
export const sessionManger = function()
{
   
     const setKey = (key,value) => sessionStorage.setItem(key,value);
     const getKey = key=> sessionStorage.getItem(key);
     const removeKey = key=>sessionStorage.removeItem(key);
     return {
        setKey,
        getKey,
        removeKey
     }
}