
import { httpService, obradiGresku, obradiUspjeh, get,obrisi,dodaj,getBySifra,promjeni  } from "./httpService";

async function getProizvodi(sifra){
  return await httpService.get('/Izdatnica/Proizvodi/' + sifra).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
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
    getProizvodi,
    dodajIzdatnicaProizvod,
    obrisiIzdatnicaProizvod,
    
};