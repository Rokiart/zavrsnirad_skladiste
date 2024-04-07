import  { httpService, obradiGresku, obradiUspjeh, get,obrisi,dodaj,getBySifra,promjeni  } from "./httpService";

async function traziKolicina(proizvod,izdatnica,izdatniceProizvodi) {
  return await httpService    .get('/' + proizvod +'/' + izdatnica + '/trazi/' + izdatniceProizvodi).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
}

export default{
  get,
  obrisi,
  dodaj,
  getBySifra,
  promjeni,
  traziKolicina
};