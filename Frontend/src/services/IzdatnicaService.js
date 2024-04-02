
import { httpService, obradiGresku, obradiUspjeh, get,obrisi,dodaj,getBySifra,promjeni  } from "./httpService";

async function getProizvodi(naziv,sifra){
  return await httpService.get('/' + naziv + '/Proizvodi/' + sifra).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
async function dodajProizvod(naziv,izdatnica, proizvod) {
  return await httpService.post('/' + naziv + '/' + izdatnica + '/dodaj/' + polaznik).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
async function obrisiProizvod(naziv,izdatnica, proizvod) {
  return await httpService.delete('/'+naziv+'/' + grupa + '/obrisi/' + polaznik).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});

}


export default{
    get,
    obrisi,
    dodaj,
    getBySifra,
    promjeni,
    getProizvodi,
    dodajProizvod,
    obrisiProizvod
};