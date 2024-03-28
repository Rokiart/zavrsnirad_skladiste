import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';


import Service from '../../services/IzdatnicaService';
import SkladistarService from '../../services/SkladistarService';
import OsobaService from '../../services/OsobaService';
import ProizvodService from '../../services/ProizvodService';
import { RoutesNames } from '../../constants';



export default function IzdatniceDodaj() {
  const navigate = useNavigate();
  

  const [osobe , setOsobe] =useState([]);
  const [osobaSifra, setOsobaSifra] =useState(0);

  const [skladistari, setSkladistari] = useState([]);
  const [skladistarSifra, setSkladistarSifra] = useState(0);

  const [proizvodi , setProizvodi] =useState([]);
  const [pronadeniProizvodi, setPronadeniProizvodi] = useState([]);

  const [searchName, setSearchName] = useState('');

  const typeaheadRef = useRef(null);

  
  async function dohvatiOsobe(){
    const odgovor = await OsobaService.get();
    if(!odgovor.ok){
      alert(dohvatiPorukeAlert(odgovor.podaci));
      return;
  }
  setOsobe(odgovor.podaci);
  setOsobaSifra(odgovor.podaci[0].sifra);
}

  async function dohvatiSkladistare(){
    const odgovor = await SkladistarService.get();
    if(!odgovor.ok){
      alert(dohvatiPorukeAlert(odgovor.podaci));
      return;
  }
  setSkladistari(odgovor.podaci);
  setSkladistarSifra(odgovor.podaci[0].sifra);
}

  async function dohvatiProizvode(){
    const odgovor =await ProizvodService.get();
    if(!odgovor.ok){
      alert(dohvatiPorukeAlert(odgovor.podaci));
      return;
  }
  setProizvodi(odgovor.podaci);
  setProizvodSifra(odgovor.podaci[0].sifra);
}

  async function ucitaj(){
    await dohvatiOsobe();
    await dohvatiSkladistare();
    await dohvatiProizvode();
  }

  useEffect(()=>{
    ucitaj();
  },[]);

  async function dodaj(e) {
    const odgovor = await Service.dodaj(e);
    if (odgovor.ok) {
      navigate(RoutesNames.IZDATNICE_PREGLED);
    } else {
      alert(odgovor.poruka.podaci);
    }
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
        brojIzdatnice: podaci.get('brojizdatnice'),
        datum: datum,
        proizvodSifra: parseInt(proizvodSifra),
        osobaSifra: parseInt(osobaSifra),
        skladistarSifra: parseInt(skladistarSifra),
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

         <Form.Group className='mb-3' controlId='proizvod'>
          <Form.Label>Proizvod</Form.Label>
            <Form.Select
              onChange={(e)=>{setProizvodSifra(e.target.value)}}
              >
               {proizvodi && proizvodi.map((e,index)=>(
                    <option key={index} value={e.sifra}>
                   {e.naziv} 
                   </option>
              ))}
             </Form.Select>
          </Form.Group>

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
                

        <Row>
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
        </Row>
      </Form>
    </Container>
  );
}
