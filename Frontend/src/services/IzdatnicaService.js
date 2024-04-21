
import { httpService, obradiGresku, obradiUspjeh, get,obrisi,dodaj,getBySifra,promjeni  } from "./httpService";

async function getIzdatniceProizvodi(naziv,sifra,kolicina){
  return await httpService.get('/' + naziv + '/IzdatniceProizvodi/' + sifra + kolicina).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
async function dodajIzdatnicaProizvod(naziv,izdatnica,kolicina) {
  return await httpService.post('/' + naziv + '/' + izdatnica + '/dodaj/' + kolicina).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
async function obrisiIzdatnicaProizvod(naziv,izdatnica , kolicina) {
  return await httpService.delete('/'+ naziv +'/' + izdatnica + '/obrisi/' + kolicina).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}


export default{
    get,
    obrisi,
    dodaj,
    getBySifra,
    promjeni,
    getIzdatniceProizvodi,
    dodajIzdatnicaProizvod,
    obrisiIzdatnicaProizvod,
    
};