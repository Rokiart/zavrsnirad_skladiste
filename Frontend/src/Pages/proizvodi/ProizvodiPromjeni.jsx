import { useEffect, useRef, useState } from 'react';
import { Button, Col, Container, Form, Modal, Row, Table} from 'react-bootstrap';
import { useNavigate, useParams } from 'react-router-dom';
import ProizvodService from '../../services/ProizvodService';
import KolicinaService from "../../services/KolicinaService";
import { RoutesNames } from '../../constants';
import useError from "../../hooks/useError";
import InputText from '../../Components/InputText';
import Akcije from '../../Components/Akcije';
import { FaTrash } from "react-icons/fa";
import { AsyncTypeahead } from "react-bootstrap-typeahead";



export default function ProizvodiPromjeni() {
  const [proizvod, setProizvod] = useState({});
  const [kolicine, setKolicine] = useState([]);
  const [pronadeneKolicine, setPronadeneKolicine] = useState([]);
  const typeaheadRef = useRef(null);
  const [prikaziModal, setPrikaziModal] = useState(false);
  const [odabranaKolicina, setOdabranaKolicina] = useState(false);
  const routeParams = useParams();
  const navigate = useNavigate();
  const { prikaziError } = useError();


  async function dohvatiProizvod() {

    const odgovor = await ProizvodService
      .getBySifra('Proizvod',routeParams.sifra)
      if(!odgovor.ok){
        prikaziError(odgovor.podaci);
        navigate(RoutesNames.PROIZVODI_PREGLED);
        return;
      }
      setProizvod(odgovor.podaci);
      setPrikaziModal(false);

  }

  async function traziKolicina(uvjet) {
    const odgovor =  await KolicinaService.traziKolicina('Kolicina',uvjet);
    if(!odgovor.ok){
      prikaziError(odgovor.podaci);
      return;
    }
    setPronadeneKolicine(odgovor.podaci);
  }

async function dohvatiKolicine() {
    const odgovor = await Service.getKolicine(routeParams.sifra);
    if(!odgovor.ok){
      prikaziError(odgovor.podaci);
      return;
    }
    setKolicine(odgovor.podaci);
  }


  useEffect(() => {
    dohvatiProizvod();
    dohvatiKolicine();
  }, []);

  async function promjeniProizvod(proizvod) {
    const odgovor = await ProizvodService.promjeni('Proizvod',routeParams.sifra, proizvod);

    if (odgovor.ok) {
      navigate(RoutesNames.PROIZVODI_PREGLED);
      return;
    }
    prikaziError(odgovor.podaci);
  }
  async function obrisiKolicinu(sifra) {
    const odgovor = await Service.obrisiKolicinu(sifra);
    if(odgovor.ok){
      await dohvatiKolicine();
      return;
    }
    prikaziError(odgovor.podaci);
  }

  async function dodajKolicinuModal(e) {
    setOdabranaKolicina(e[0]);
    setPrikaziModal(true);
  }

  async function dodajKolicinu() {
    const odgovor = await Service.dodajKolicinu({
      proizvodSifra: routeParams.sifra,
      kolicinaSifra: odabranaKolicina.sifra,
      napomena: document.getElementById('napomena').value
    });
    if(odgovor.ok){
      setPrikaziModal(false);
      await dohvatiKolicine();
      typeaheadRef.current.clear();
      return;
    }
    prikaziError(odgovor.podaci);
  }

  function handleSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);
    promjeniProizvod({
      naziv: podaci.get('naziv'),
      sifraProizvoda: podaci.get('sifraProizvoda'),
      mjernaJedinica: podaci.get('mjernaJedinica')
     
    });
  }

  function zatvoriModal(){
    setPrikaziModal(false);
}


  return (
    <>
    <Container >
      <Form onSubmit={handleSubmit}>
      <Row>
      <Col key='1' sm={12} lg={6} md={6}>
        <InputText atribut='Naziv' vrijednost={proizvod.naziv} />
        <InputText atribut='sifraProizvoda' vrijednost={proizvod.sifraProizvoda} />
        <InputText atribut='mjernaJedinica' vrijednost={proizvodk.mjernaJedinica} />
       
        <Akcije odustani={RoutesNames.PROIZVODI_PREGLED} akcija='Promjeni proizvod' /> 
        </Col>
                <Col key='2' sm={12} lg={6} md={6}>
                <Form.Label>Traži kolicinu</Form.Label>
                  <AsyncTypeahead
                  className='autocomplete'
                  id='uvjet'
                  emptyLabel='Nema rezultata'
                  searchText='Tražim...'
                  labelKey={(o) => `${o.naziv}`}
                  minLength={3}
                  options={pronadeneKolicine}
                  onSearch={traziKolicinu}
                  placeholder='dio naziva kolicine'
                  renderMenuItemChildren={(o) => (
                    <>
                      <span>
                        {o.naziv}
                      </span>
                    </>
                  )}
                  onChange={dodajKolicinuModal}
                  ref={typeaheadRef}
                  />
                        <Table striped bordered hover>
                        <thead>
                        <tr>
                            <th>Kolicine proizvoda</th>
                            <th>Akcija</th>
                        </tr>
                        </thead>
                        <tbody>
                        {kolicine &&
                            kolicine.map((o, index) => (
                            <tr key={index}>
                                <td>
                                {o.kolicina}
                                <hr />
                                {o.napomena}
                                </td>
                                <td>
                                <Button
                                    variant='danger'
                                    onClick={() =>
                                    obrisiKolicinu(o.sifra)
                                    }
                                >
                                    <FaTrash />
                                </Button>
                                </td>
                            </tr>
                            ))}
                        </tbody>
                    </Table>
                </Col>
            </Row>
            
                 
           </Form>
        </Container>

        <Modal show={prikaziModal} onHide={zatvoriModal}>
        <Modal.Header closeButton>
        <Modal.Title>Dodavanje nove kolicne proizvodar <br /> {proizvod.kolicina}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            Oznaka: {odabranaKolicina.naziv}
            <Form>
                <Form.Group>
                    <Form.Label>Napomena</Form.Label>
                    <Form.Control
                    autoFocus
                    id='napomena'
                    as='textarea' rows={3}
                    name='napomena'
                    />
                </Form.Group>
                <hr />
                <Button variant='primary' onClick={dodajKolicinu}>
                    Dodaj
                </Button>
            </Form>
        </Modal.Body>
        <Modal.Footer>
        <Button variant='secondary' onClick={zatvoriModal}>
            Zatvori
        </Button>
        </Modal.Footer>
        </Modal>

        </>

    );

}