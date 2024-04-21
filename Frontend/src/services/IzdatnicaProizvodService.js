// import  { httpService, obradiGresku, obradiUspjeh, get,obrisi,dodaj,getBySifra,promjeni  } from "./httpService";
import  { get,obrisi,dodaj,getBySifra,promjeni  } from "./httpService";
// async function traziIzdatnicaProizvod(kolicina,izdatnica,izdatniceProizvodi) {
//   return await httpService    .get('/' + Kolicina +'/' + izdatnica + '/trazi/' + izdatniceProizvodi).then((res)=>{return obradiUspjeh(res);}).catch((e)=>{ return obradiGresku(e);});
// }

export default{
  get,
  obrisi,
  dodaj,
  getBySifra,
  promjeni
  // traziIzdatnicaProizvod
};