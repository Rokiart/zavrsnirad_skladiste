import { App } from "../Constants";
import { httpService } from "./httpServices";

async function getOsobe(){
    return await httpService.get('/Osoba')
    .then((res)=>{
        if(App.DEV) console.table(res.data);

        console.table(res.data);
        return res;
    }).catch((e)=>{
        console.log(e);
    });
}


 export default{
    getOsobe
 };