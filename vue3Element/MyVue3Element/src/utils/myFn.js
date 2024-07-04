//tree结构 递归
//pData 结果数据， 
//sData 原来数据，
export function CreateTree(pData, sData) {
    for (let i = 0; i < pData.length; i++) {
        for (let j = 0; j < sData.length; j++) {
            if (pData[i].id == sData[j].father_id) {
                if (!pData[i].children)
                    pData[i].children = [];
                pData[i].children.push({ ...sData[j] });
            }
        }
        if(pData[i].children){
            CreateTree(pData[i].children, sData)
        }
    }
}