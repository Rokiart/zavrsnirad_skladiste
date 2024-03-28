import { useEffect, useState, useRef } from "react";
import { Button, Col, Container, Form, Row ,Table } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { AsyncTypeahead } from 'react-bootstrap-typeahead';
import { RoutesNames } from "../../constants";
import IzdatnicaService from "../../services/IzdatnicaService";
import { FaTrash } from 'react-icons/fa';
import moment from 'moment';


import Service from '../../services/IzdatnicaService';
import SkladistarService from '../../services/SkladistarService';
import OsobaService from '../../services/OsobaService';
import ProizvodService from '../../services/ProizvodService';
import { dohvatiPorukeAlert } from '../../services/httpService';


export default function IzdatnicePromjeni(){

    const navigate =useNavigate();
    const routeParams = useParams();
    const [izdatnica,setIzdatnica] = useState({});

    const [Osobe, setOsobe] = useState([]);
    const [sifraOsoba, setSifraOsoba] = useState(0);

    const [skladistari, setSkladistari] = useState([]);
    const [sifraSkladistar, setSifraSkladistar] = useState(0);

    const [proizvodi, setProizvodi] = useState([]);
    const [sifraProizvod, setSifraProizvod] = useState(0);

    const [searchName, setSearchName] = useState('');

    const typeaheadRef = useRef(null);


    async function dohvatiIzdatnica() {
        const odgovor = await Service.getBySifra(routeParams.sifra);
        if(!odgovor.ok){
          alert(dohvatiPorukeAlert(odgovor.podaci));
          return;
        }
      }

    async function dohvatiProizvodi() {
        const odgovor = await Service.getProizvodi(routeParams.sifra);
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setProizvodi(odgovor.podaci);
      }


      async function dohvatiOsobe() {
        const odgovor =  await OsobaService.getOsobe();
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setOsobe(odgovor.podaci);
        setSifraOsoba(odgovor.podaci[0].sifra);
          
      }

      async function dohvatiSkladistari() {
        const odgovor =  await SkladistarService.get();
        if(!odgovor.ok){
          alert(dohvatiPorukeAlert(odgovor.podaci));
          return;
        }
        setSkladistari(odgovor.podaci);
        setSifraSkladistar(odgovor.podaci[0].sifra);
      }

      async function traziProizvod(uvjet) {
        const odgovor =  await ProizvodService.traziProizvod(uvjet);
        if(!odgovor.ok){
          alert(dohvatiPorukeAlert(odgovor.podaci));
          return;
        }
        setPronadeniProizvodi(odgovor.podaci);
        setSearchName(uvjet);
      }

      async function traziOsoba(uvjet) {
        const odgovor =  await OsobaService.traziOsoba(uvjet);
        if(!odgovor.ok){
          alert(dohvatiPorukeAlert(odgovor.podaci));
          return;
        }
        setPronadeniOsobe(odgovor.podaci);
        setSearchName(uvjet);
      }

      async function dohvatiInicijalnePodatke() {
        await dohvatiOsobe();
        await dohvatiSkladistare();
        await dohvatiIzdatnica();
        await dohvatiProizvodi();
      }
    
      useEffect(() => {
        dohvatiInicijalnePodatke();
      }, []);

      async function promjeni(e) {
        const odgovor = await Service.promjeni(routeParams.sifra, e);
        if(odgovor.ok){
          navigate(RoutesNames.IZDATNICE_PROMJENI_PREGLED);
          return;
        }
        alert(dohvatiPorukeAlert(odgovor.podaci));
      }
    
      async function obrisiProizvod(grupa, proizvod) {
        const odgovor = await Service.obrisiProizvod(grupa, proizvod);
        if(odgovor.ok){
          await dohvatiProizvodi();
          return;
        }
        alert(dohvatiPorukeAlert(odgovor.podaci));
     
        async function dodajProizvode(e) {
            //console.log(e[0]);
            const odgovor = await Service.dodajProizvod(routeParams.sifra, e[0].sifra);
            if(odgovor.ok){
              await dohvatiProizvodi();
              return;
            }
            alert(dohvatiPorukeAlert(odgovor.podaci));
          }
      }
    


    function handleSubmit(e){
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

   
      
          promjeni({
           brojIzdatnice: podaci.get('naziv'),
            datum: datum,
            OsobaSifra: parseInt(sifraOsoba), 
            SkladistarSifra: parseInt(sifraSkladistar),
            napomena: podaci.get('napomena')
     
        });
      }

      
          async function dodajRucnoProizvod(Proizvod) {
            const odgovor = await ProizvodService.dodaj(Proizvod);
            if (odgovor.ok) {
              const odgovor2 = await Service.dodajProizvod(routeParams.sifra, odgovor.podaci.sifra);
              if (odgovor2?.ok) {
                typeaheadRef.current.clear();
                await dohvatiProizvodi();
                return;
              }
              alert(dohvatiPorukeAlert(odgovor2.podaci));
              return;
            }
            alert(dohvatiPorukeAlert(odgovor.podaci));
              
          }
    
          function dodajRucnoPolaznika(){
            let niz = searchName.split(' ');
            if(niz.length<2){
              alert('Obavezno ime i prezime');
              return;
            }
        
            dodajRucnoPolaznik({
              ime: niz[0],
              prezime: niz[1],
              oib: '',
              email: '',
              brojugovora: ''
            });
        
            
          }

          return (
        <Container className='mt-4'>
           
           <Form onSubmit={handleSubmit}>

                <Form.Group  className='mb-3' controlId="brojIzdatnice">
                    <Form.Label>Broj Izdatnice</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.brojIzdatnice}
                        name="brojIzdatnice"
                        maxLength={50}
                       
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="datum">
                    <Form.Label>Datum</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.datum}
                        name="datum"
                        
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId='vrijeme'>
                    <Form.Label>Vrijeme</Form.Label>
                    <Form.Control
                         type='time'
                         name='vrijeme'
                         
                    />
                </Form.Group>


                {/* <Form.Group className='mb-3' controlId="proizvod">
                    <Form.Label>Proizvod</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.proizvod}
                        name="proizvod"
                        required
                    />
                </Form.Group> */}

                

                <Form.Group className='mb-3' controlId="osoba">
                    <Form.Label>Osoba</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.osoba}
                        name="osoba"
                        
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="skladistar">
                    <Form.Label>Skladistar</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.skladistar}
                        name="skladistar"
                       
                    />
                </Form.Group>

                <Form.Group className='mb-3' controlId="napomena">
                    <Form.Label>Napomena</Form.Label>
                    <Form.Control 
                        type="text"
                        defaultValue={izdatnica.napomena}
                        name="napomena"
                        maxLength={250}
                    />

                </Form.Group>                     

                
                <Row className="akcije">
                    <Col>
                        <Link 
                        className="btn btn-danger"
                        to={RoutesNames.IZDATNICE_PREGLED}>Odustani</Link>
                    </Col>
                    <Col>
                        <Button
                            variant="primary"
                            type="submit"
                        >
                            Promjeni podatke izdatnice
                        </Button>
                    </Col>
                </Row>
                
           </Form>

        </Container>

    );

}