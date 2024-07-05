//tree结构 递归
//pData结果数据， 树状结构

import component from "element-plus/es/components/tree-select/src/tree-select-option.mjs";

//sData原来数据， id pid
export function createTree(pData, sData) {
    for (let i = 0; i < pData.length; i++) {
        for (let j = 0; j < sData.length; j++) {
            if (pData[i].id == sData[j].father_id) {
                if (!pData[i].children)
                    pData[i].children = [];
                pData[i].children.push({ ...sData[j] });
            }
        }
        if(pData[i].children){
            createTree(pData[i].children, sData)
        }
    }
}

//路由数据格式
let modules = import.meta.glob("../views/home/**/index.vue");  //这里解析不了全局的@
//let modules = import.meta.glob("@/views/home/**/index.vue");  //这里解析不了全局的@ 这样不能显示内容
export const initRoutes = (data,subObj) =>{
    data.forEach(item => {
        let obj = {
            path:item.menu_url,
            name:item.name,
            mate:{title:item.menu_name},
            component:modules[`../views/home/${item.component}/index.vue`]
        };
        //逻辑还得理理
        if(subObj instanceof Array){
            subObj.push(obj);
        }else
        {
            if(!subObj.children){
                subObj.children = [];
            };
            subObj.children.push(obj)
        }
        //递归调用 不确定层级的情况
        if(item.children){
            initRoutes(item.children,obj);
        }
    });
}