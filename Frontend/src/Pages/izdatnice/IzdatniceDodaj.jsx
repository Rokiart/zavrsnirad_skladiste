import { Container, Form, Row} from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import useError from '../../hooks/useError';


import Service from '../../services/IzdatnicaService';
import useLoading from '../../hooks/useLoading';
import { RoutesNames } from '../../constants';
import Akcije from '../../Components/Akcije';





export default function IzdatniceDodaj() {
  const navigate = useNavigate();
   

  const [osobe , setOsobe] =useState([]);
  const [osobaSifra, setOsobaSifra] =useState(0);

  const [izdatniceProizvodi , setIzdatniceProizvodi] = useState([]);
  const [izdatnicaProizvodSifra, setIzdatnicaProizvodSifra] = useState(0);

  const [skladistari, setSkladistari] = useState([]);
  const [skladistarSifra, setSkladistarSifra] = useState(0);

  // const [proizvodi , setProizvodi] = useState([]);
  // const [proizvodSifra , setProizvodSifra] = (0);

  const { prikaziError } = useError();
  const { showLoading, hideLoading } = useLoading();
  
  async function dohvatiOsobe(){
    const odgovor = await Service.get('Osoba');
    if(!odgovor.ok){
      prikaziError(odgovor.podaci);
      return;
  }
  setOsobe(odgovor.podaci);
  setOsobaSifra(odgovor.podaci[0].sifra);
}

  async function dohvatiSkladistare(){
    const odgovor = await Service.get('Skladistar');
    if(!odgovor.ok){
      prikaziError(odgovor.podaci);
      return;
  }
  setSkladistari(odgovor.podaci);
  setSkladistarSifra(odgovor.podaci[0].sifra);
}

async function dohvatiIzdatniceProizvode(){
  const odgovor = await Service.get('IzdatnicaProizvod');
  if(!odgovor.ok){
    prikaziError(odgovor.podaci);
    return;
}
setIzdatniceProizvodi(odgovor.podaci);
setIzdatnicaProizvodSifra(odgovor.podaci[0].sifra);
}

// async function dohvatiProizvode(){
//   const odgovor =await Service.getProizvodi();
//   if(!odgovor.ok){
//     prikaziError(odgovor.podaci);
//     return;
// }
// setProizvodi(odgovor.podaci);
// setProizvodSifra(odgovor.podaci[0].sifra);
// }

async function ucitaj(){
  showLoading();

  await dohvatiOsobe();
  await dohvatiSkladistare();
  // await dohvatiProizvode();
  await dohvatiIzdatniceProizvode();
  hideLoading();
}

useEffect(()=>{
  ucitaj();
},[]);


async function dodaj(e) {
  showLoading();
  const odgovor = await Service.dodaj('Izdatnica',e);
  hideLoading();
  if(odgovor.ok){
    navigate(RoutesNames.IZDATNICE_PREGLED);
    return
  }
  prikaziError(odgovor.podaci);
  
}




  async function handleSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);
    
    if(podaci.get('datum')=='' && podaci.get('vrijeme')!=''){
      alert('Ako postavljate vrijeme morate i datum');
      return;
    }
    let datum ='';
    if(podaci.get('datum')!='' && podaci.get('vrijeme')==''){
      datum = podaci.get('datum') + 'T00:00:00.000Z';
    }else{
      datum = podaci.get('datum') + 'T' + podaci.get('vrijeme') + ':00.000Z';
    }
    

      dodaj({
        brojIzdatnice: podaci.get('brojIzdatnice'),
        datum: datum,
        // proizvodSifra: parseInt(proizvodSifra),
        osobaSifra: parseInt(osobaSifra),
        skladistarSifra: parseInt(skladistarSifra),
        izdatnicaProizvodSifra: parseInt(izdatnicaProizvodSifra),
        napomena: podaci.get('napomena')

      });
    }   
      

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit}>
 
      

        <Form.Group className='mb-3' controlId='brojIzdatnice'>
          <Form.Label>Broj Izdatnice</Form.Label>
          <Form.Control
            type='text'
            name='brojizdatnice'
            placeholder='brojIzdatnice'
            maxLength={50}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='datum'>
          <Form.Label>Datum</Form.Label>
          <Form.Control
            type='date'
            name='datum'
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='vrijeme'>
          <Form.Label>Vrijeme</Form.Label>
          <Form.Control
            type='time'
            name='vrijeme'
          />
         </Form.Group>

         {/* <Form.Group className='mb-3' controlId='proizvod'>
          <Form.Label>Proizvod</Form.Label>
            <Form.Select
              onChange={(e)=>{setProizvodSifra(e.target.value)}}
              >
               {proizvodi && proizvodi.map((s,index)=>(
                    <option key={index} value={s.sifra}>
                   {s.naziv}  
                   </option>
              ))}
             </Form.Select>
          </Form.Group> */}

          {/* <Form.Group className='mb-3' controlId='izdatnicaProizvod'>
          <Form.Label>Kolicina</Form.Label>
            <Form.Select
              onChange={(e)=>{setIzdatnicaProizvodSifra(e.target.value)}}
              >
               {izdatniceProizvodi && izdatniceProizvodi.map((e,index)=>(
                    <option key={index} value={e.sifra}>
                   {e.kolicina} 
                   </option>
              ))}
             </Form.Select>
          </Form.Group> */}

         <Form.Group className='mb-3' controlId='osoba'>
          <Form.Label>Osoba</Form.Label>
            <Form.Select
              onChange={(e)=>{setOsobaSifra(e.target.value)}}
              >
               {osobe && osobe.map((e,index)=>(
                    <option key={index} value={e.sifra}>
                   {e.ime} {e.prezime}
                   </option>
              ))}
             </Form.Select>
          </Form.Group>

          <Form.Group className='mb-3' controlId='skladistar'>
          <Form.Label>Skladištar</Form.Label>
            <Form.Select
              onChange={(e)=>{setSkladistarSifra(e.target.value)}}
              >
               {skladistari && skladistari.map((e,index)=>(
                    <option key={index} value={e.sifra}>
                   {e.ime} {e.prezime}
                   </option>
              ))}
             </Form.Select>
          </Form.Group>

          <Form.Group className='mb-3' controlId='napomena'>
          <Form.Label>Napomena</Form.Label>
          <Form.Control
            type='text'
            name='napomena'
            placeholder='napomena'
            maxLength={250}
          
            />
            </Form.Group> 
                

        {/* <Row>
          <Col>
            <Link className='btn btn-danger gumb' to={RoutesNames.IZDATNICE_PREGLED}>
              Odustani
            </Link>
          </Col>
          <Col>
            <Button variant='primary' className='gumb' type='submit'>
              Dodaj novu izdatnicu
            </Button>
          </Col>
        </Row> */}
        <Akcije odustani={RoutesNames.IZDATNICE_PREGLED} akcija='Dodaj izdatnicu' /> 
      </Form>
    </Container>
  );
}
