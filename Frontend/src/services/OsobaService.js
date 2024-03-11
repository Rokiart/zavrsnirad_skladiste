import { App } from '../constants'
import { httpService } from "./httpService";

async function getOsobe(){
     return await httpService. get('/Osoba')
     .then((res)=>{
         if(App.DEV) console.table(res.data);
        return res;

     }).catch((e)=>{
        console.log(e);
     });
}
async function obrisiOsoba(sifra){
    return await httpService.delete('/Osoba/' + sifra)
    .then((res)=>{
        return {ok: true, poruka: res};
    }).catch((e)=>{
        console.log(e);
    });
}

async function dodajOsobu(osoba){
    const odgovor = await httpService.post('/Osoba',osoba)
    .then(()=>{
        return {ok: true, poruka: 'Uspješno dodano'}
    })
    .catch((e)=>{
        console.log(e.response.data.errors);
        return {ok: false, poruka: 'Greška'}
    });
    return odgovor;
}

async function promjeniOsobu(sifra,osoba){
    const odgovor = await httpService.put('/Osoba/'+sifra,osoba)
    .then(()=>{
        return {ok: true, poruka: 'Uspješno promjenjeno'}
    })
    .catch((e)=>{
        console.log(e.response.data.errors);
        return {ok: false, poruka: 'Greška'}
    });
    return odgovor;
}

async function getBySifra(sifra){
    return await httpService.get('/Osoba/' + sifra)
    .then((res)=>{
        if(App.DEV) console.table(res.data);

        return res;
    }).catch((e)=>{
        console.log(e);
        return {poruka: e}
    });
}


export default{
    getOsobe,
    obrisiOsoba,
    dodajOsobu,
    promjeniOsobu,
    getBySifra
};