import { useEffect, useState, useRef } from "react";
import { Button, Col, Container, Form, Row ,Table } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { AsyncTypeahead } from 'react-bootstrap-typeahead';
import { RoutesNames } from "../../constants";

import { FaTrash } from 'react-icons/fa';

import useLoading from '../../hooks/useLoading';
import InputText from '../../Components/InputText';
import useError from '../../hooks/useError';
import Service from '../../services/IzdatnicaService';
import SkladistarService from '../../services/SkladistarService';
import OsobaService from '../../services/OsobaService';
import ProizvodService from '../../services/ProizvodService';



export default function IzdatnicePromjeni(){

    const navigate =useNavigate();
    const routeParams = useParams();
    const [izdatnica, setIzdatnica] = useState([]);
 
    const [osobe, setOsobe] = useState([]);
    const [sifraOsoba, setSifraOsoba] = useState(0);

    const [skladistari, setSkladistari] = useState([]);
    const [sifraSkladistar, setSifraSkladistar] = useState(0);

    const [proizvodi, setProizvodi] = useState([]);
    const [pronadeniProizvodi, setPronadeniProizvodi] = useState([]);

    const [searchName, setSearchName] = useState('');

    const typeaheadRef = useRef(null);
    const { prikaziError } = useError();
    const { showLoading, hideLoading } = useLoading();


    async function dohvatiIzdatnica() {
        const odgovor = await Service.getBySifra('Izdatnica',routeParams.sifra);
        if(!odgovor.ok){
          prikaziError(odgovor.podaci);
          return;
        }
        setIzdatnica(odgovor.podaci);
        setSifraOsoba(izdatnica.OsobaSifra);
        if(izdatnica.skladistarSifra!=null){
          setSifraSkladistar(izdatnica.skladistarSifra);
        }       
      }

    async function dohvatiProizvodi() {
        const odgovor = await Service.getProizvodi(routeParams.sifra);
        if(!odgovor.ok){
          prikaziError(odgovor.podaci);
            return;
        }
        setProizvodi(odgovor.podaci);
        
      }


      async function dohvatiOsobe() {
        const odgovor =  await OsobaService.get('Osoba');
        if(!odgovor.ok){
          prikaziError(odgovor.podaci);
            return;
        }
        setOsobe(odgovor.podaci);
        setSifraOsoba(odgovor.podaci[0].sifra);
          
      }

      async function dohvatiSkladistare() {
        const odgovor =  await SkladistarService.get('Skladistar');
        if(!odgovor.ok){
          prikaziError(odgovor.podaci);
          return;
        }
        setSkladistari(odgovor.podaci);
        setSifraSkladistar(odgovor.podaci[0].sifra);
      }


      async function traziProizvod(uvjet) {
        const odgovor =  await ProizvodService.traziProizvod('Proizvod',uvjet);
        if(!odgovor.ok){
          prikaziError(odgovor.podaci);
          return;
        }
        setPronadeniProizvodi(odgovor.podaci);
        setSearchName(uvjet);
      }



      async function dohvatiInicijalnePodatke() {
        showLoading();
        await dohvatiOsobe();
        await dohvatiSkladistare();
        await dohvatiIzdatnica();
        await dohvatiProizvodi();
        hideLoading();
      }
    
      useEffect(() => {
        dohvatiInicijalnePodatke();
      }, []);

      async function promjeni(e) {
        const odgovor = await Service.promjeni('Izdatnica',routeParams.sifra, e);
        if(odgovor.ok){
          navigate(RoutesNames.IZDATNICE_PREGLED);
          return;
        }
        prikaziError(odgovor.podaci);
      }
    
      async function obrisiProizvod(izdatnica, proizvod) {
        const odgovor = await Service.obrisiProizvod(izdatnica, proizvod);
        if(odgovor.ok){
          await dohvatiProizvodi();
          return;
        }
        prikaziError(odgovor.podaci);
      }
        async function dodajProizvod(e) {
            //console.log(e[0]);
            const odgovor = await Service.dodajProizvod(routeParams.sifra, e[0].sifra);
            if(odgovor.ok){
              await dohvatiProizvodi();
              hideLoading();
              return;
            }
            hideLoading();
            prikaziError(odgovor.podaci);
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
            proizvodSifra:podaci.get('proizvod'),
            izdatnicaProizvodSifra:podaci.get('kolicina'),
            napomena: podaci.get('napomena')
     
        });
      }

      
          async function dodajRucnoProizvod(Proizvod) {
            const odgovor = await ProizvodService.dodaj('Proizvod', Proizvod);
            if (odgovor.ok) {
              const odgovor2 = await Service.dodajProizvod('Izdatnica', routeParams.sifra, odgovor.podaci.sifra);
              if (odgovor2?.ok) {
                typeaheadRef.current.clear();
                await dohvatiProizvodi();
                hideLoading();
                return;
              }
              hideLoading();
              prikaziError(odgovor2.podaci);
              return;
            }
            hideLoading();
            prikaziError(odgovor.podaci);
             
          }
    
          function dodajRucnoProizvod(){
            let niz = searchName.split(' ');
            if(niz.length<2){
              prikaziError([{svojstvo:'',poruka:'Obavezan naziv'}]);
              return;
            }
        
            dodajRucnoProizvod({
              naziv: niz[0],
              sifraProizvoda: '',
              mjernaJedinica: '',
             
            });
        
            
          }

          return (
        <Container className='mt-4'>
           
           <Form onSubmit={handleSubmit}>
           <Row>
          <Col key='1' sm={12} lg={6} md={6}>
            <InputText atribut='brojIzdatnice' vrijednost={izdatnica.brojizdatnice} />
            <Row>
              <Col key='1' sm={12} lg={6} md={6}>
                <Form.Group className='mb-3' controlId='datum'>
                  <Form.Label>Datum</Form.Label>
                  <Form.Control
                    type='date'
                    name='datum'
                    defaultValue={izdatnica.datum}
                  />
                </Form.Group>
              </Col>
              <Col key='2' sm={12} lg={6} md={6}>
                <Form.Group className='mb-3' controlId='vrijeme'>
                  <Form.Label>Vrijeme</Form.Label>
                  <Form.Control
                    type='time'
                    name='vrijeme'
                    defaultValue={izdatnica.vrijeme}
                  />
                </Form.Group>
              </Col>
            </Row>      
                
                <Form.Group className='mb-3' controlId='osoba'>
          <Form.Label>Osoba</Form.Label>
          <Form.Select
             value={sifraOsoba}
             onChange={(e) => {
               setSifraOsoba(e.target.value);
             }}
          >
            {osobe &&
              osobe.map((osoba, index) => (
                <option key={index} value={osoba.sifra}>
                  {osoba.ime} {osoba.prezime}
                </option>
              ))}
          </Form.Select>
        </Form.Group>

        <Form.Group className='mb-3' controlId='skladistar'>
          <Form.Label>Skladistar</Form.Label>
          <Form.Select
            value={sifraSkladistar}
            onChange={(e) => {
              setSifraSkladistar(e.target.value);
            }}
          >
            {skladistari &&
              skladistari.map((skladistar, index) => (
                <option key={index} value={skladistar.sifra}>
                  {skladistar.ime} {skladistar.prezime}
                </option>
              ))}
          </Form.Select>
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
                </Col>
          <Col key='2' sm={12} lg={6} md={6}>
          <Form.Group className='mb-3' controlId='uvjet'>
                <Row>
                <Form.Label>Traži proizvod</Form.Label>
                <Col key='1' sm={12} lg={10} md={10}>
                
              <AsyncTypeahead
                className='autocomplete'
                id='uvjet'
                emptyLabel='Nema rezultata'
                searchText='Tražim...'
                labelKey={(proizvod) => `${proizvod.naziv} `}
                minLength={3}
                options={pronadeniProizvodi}
                onSearch={traziProizvod}
                placeholder='dio naziva proizvoda'
                renderMenuItemChildren={(proizvod) => (
                  <>
                    <span>
                      {proizvod.naziv}
                    </span>
                  </>
                )}
                onChange={dodajProizvod}
                ref={typeaheadRef}
              />
                  </Col>
                  <Col key='2' sm={12} lg={2} md={2}>
                  <Button
                          onClick={dodajRucnoProizvod}
                        >
                         Dodaj
                        </Button>
                  </Col>
                </Row>
              

            </Form.Group>
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>Proizvodi na izdatnici</th>
                  <th>Akcija</th>
                </tr>
              </thead>
              <tbody>
                {proizvodi &&
                  proizvodi.map((proizvod, index) => (
                    <tr key={index}>
                      <td>
                        {proizvod.naziv} 
                        
                      </td>
                      <td>
                        <Button
                          variant='danger'
                          onClick={() =>
                            obrisiProizvod(routeParams.sifra, proizvod.sifra)
                          }
                        >
                          <FaTrash />
                        </Button>
                             &nbsp;
                        <Button
                
                        onClick={()=>{navigate(`/proizvodi/${proizvod.sifra}`)}}
                       >Detalji</Button>
                      </td>
                    </tr>
                  ))}
              </tbody>
            </Table>
          </Col>
          </Row>
           </Form>

        </Container>

    );

}