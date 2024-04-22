
import { httpService, obradiGresku, obradiUspjeh, get,obrisi,dodaj,getBySifra,promjeni  } from "./httpService";

async function getProizvodi(sifra){
  return await httpService.get('/Izdatnica/Proizvodi/' + sifra).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
async function dodajProizvod(izdatnica,proizvod) {
  return await httpService.post('/Izdatnica/' + izdatnica + '/dodaj/' + proizvod ).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
async function obrisiProizvod(izdatnica , proizvod) {
  return await httpService.delete('/Izdatnica/ObrisiProizvod/' +  izdatnica + '/' + proizvod).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}


export default{
    get,
    obrisi,
    dodaj,
    getBySifra,
    promjeni,
    getProizvodi,
    dodajProizvod,
    obrisiProizvod,
    
};