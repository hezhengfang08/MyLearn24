import service from ".";
const http =({
    get:(url,params)=>service.get(url,{params}),
    post:(url,params)=>service.post(url,params),
    delete:(url,params)=>service.delete(url,params),
    put:(url,params)=>service.put(url,params),
    upload:(url,files)=>service.upload(url,files,{ 
        headers:{'Content-Type':'multipart/form-data'}
    })

})
export default http