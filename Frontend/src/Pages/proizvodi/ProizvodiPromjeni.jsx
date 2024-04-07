import { useEffect, useRef, useState } from 'react';
import { Button, Col, Container, Form, Modal, Row, Table} from 'react-bootstrap';
import { useNavigate, useParams } from 'react-router-dom';
import ProizvodService from '../../services/ProizvodService';

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
  const [trenutnaSlika, setTrenutnaSlika] = useState('');
  const [slikaZaCrop, setSlikaZaCrop] = useState('');
  const [slikaZaServer, setSlikaZaServer] = useState('');


  async function dohvatiProizvod() {

    const odgovor = await ProizvodService
      .getBySifra('Proizvod',routeParams.sifra)
      if(!odgovor.ok){
        hideLoading();
        prikaziError(odgovor.podaci);
       
        return;
      }
      setProizvod(odgovor.podaci);
      if(odgovor.podaci.slika!=null){
        setTrenutnaSlika(App.URL + odgovor.podaci.slika + `?${Date.now()}`);
      }else{
        setTrenutnaSlika(nepoznato);
      }
      hideLoading();
    }

    useEffect(() => {
      dohvatiProizvod();
    }, []);

    async function promjeniProizvod(proizvod) {
      showLoading();
      const odgovor = await Service.promjeni('Proizvod',routeParams.sifra, proizvod);
      if(odgovor.ok){
        hideLoading();
        navigate(RoutesNames.PROIZVODI_PREGLED);
        return;
      }
      alert(dohvatiPorukeAlert(odgovor.podaci));
      hideLoading();
    }

    function handleSubmit(e) {
      e.preventDefault();
  
      const podaci = new FormData(e.target);
      promjeniProivod({
        naziv: podaci.get('naziv'),
        sifraProizvoda: podaci.get('sifraProizvoda'),
        mjernaJedinica: podaci.get('mjernaJedinica'),
        slika: ''
      });
    }
  
  

    function onCrop() {
      setSlikaZaServer(cropperRef.current.cropper.getCroppedCanvas().toDataURL());
    }
  
    function onChangeImage(e) {
      e.preventDefault();
  
      let files;
      if (e.dataTransfer) {
        files = e.dataTransfer.files;
      } else if (e.target) {
        files = e.target.files;
      }
      const reader = new FileReader();
      reader.onload = () => {
        setSlikaZaCrop(reader.result);
      };
      try {
        reader.readAsDataURL(files[0]);
      } catch (error) {
        console.error(error);
      }
    }
  
    async function spremiSliku() {
      showLoading();
      const base64 = slikaZaServer;
  
      const odgovor = await Service.postaviSliku(routeParams.sifra, {Base64: base64.replace('data:image/png;base64,', '')});
      if(!odgovor.ok){
        hideLoading();
        prikaziError(odgovor.podaci);
      }
      //Date.now je zbog toga što se src na image komponenti cache-ira
      //pa kad promjenimo sliku url ostane isti i trenutna slika se ne updatea
      setTrenutnaSlika(slikaZaServer);
      hideLoading();
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