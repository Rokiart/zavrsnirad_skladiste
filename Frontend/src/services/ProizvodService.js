
import  { httpService, obradiGresku, obradiUspjeh, get,obrisi,dodaj,getBySifra,promjeni } from "./httpService";

async function traziProizvod(naziv,uvjet) {
  return await httpService.get('/' + naziv +'/trazi/' + uvjet).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
async function getKolicine(sifra){
  return await httpService.get('/Proizvod/Kolicine/' + sifra).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}

async function obrisiKolicinu(sifra){
  return await httpService.delete('/Proizvod/ObrisiKolicinu/' + sifra)
      .then((res)=>{
          return obradiUspjehBrisanje(res);
      }).catch((e)=>{
          return obradiGresku(e);
      });
}

async function dodajKolicinu(proizvodKolicina) {
  return await httpService.post('/Proizvod/DodajKolicinu/',proizvodKolicina).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}
export default{
  get,
  obrisi,
  dodaj,
  getBySifra,
  promjeni,
  traziProizvod,
  getKolicine,
  obrisiKolicinu,
  dodajKolicinu
};