import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import Service from '../../services/IzdatnicaService';
import { RoutesNames } from '../../constants';


export default function IzdatniceDodaj() {
  const navigate = useNavigate();
  const [errors, setErrors] = useState({});


  async function dodajIzdatnice(e) {
    const odgovor = await Service.dodaj(e);
    if (odgovor.ok) {
      navigate(RoutesNames.IZDATNICE_PREGLED);
    } else {
      setErrors(odgovor.poruka.errors || {});
    }
  }

  async function handleSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);

    const osobaSifra = podaci.get('osobaSifra');
    const skladistarSifra = podaci.get('skladistarSifra');

    if (!osobaSifra || !skladistarSifra) {
      alert('Odaberite osobu i skladistara!');
      return;
    }

    // Poziv funkcija dodajOsoba i dodajSkladistar s pripadajućim podacima
    const osobaOdgovor = await OsobaService.dodajOsoba({ sifra: osobaSifra });
    const skladistarOdgovor = await SkladistarService.dodajSkladistar({ sifra: skladistarSifra });

    if (osobaOdgovor.ok && skladistarOdgovor.ok) {
      // Ako oba poziva budu uspješna, dodajIzdatnice se izvršava
      dodajIzdatnice({
        brojIzdatnice: podaci.get('brojizdatnice'),
        datum: podaci.get('datum'),
        osobaSifra: osobaSifra,
        skladistarSifra: skladistarSifra,
        napomena: podaci.get('napomena')
      });
    } else {
      // Ako bilo koji od poziva ne uspije, korisnik će dobiti odgovarajuću poruku o grešci
      alert('Greška prilikom dodavanja osobe ili skladistara!');
    }
  }

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit}>
        <Form.Group className='mb-3' controlId='brojIzdatnice'>
          <Form.Label>Naziv</Form.Label>
          <Form.Control
            type='text'
            name='brojizdatnice'
            placeholder='brojIzdatnice'
            maxLength={255}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='datum'>
          <Form.Label>Datum</Form.Label>
          <Form.Control
            type='date'
            name='datum'
            required
          />
        </Form.Group>
        
        <Form.Group className='mb-3' controlId='osobaSifra'>
          <Form.Label>Osoba</Form.Label>
          <Form.Control
            type='date'
            name='osobaSifra'
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='skladistarSifra'>
          <Form.Label>Skladistar</Form.Label>
          <Form.Control
            type='date'
            name='skladistarSifra'
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='napomena'>
          <Form.Label>Vrijeme</Form.Label>
          <Form.Control
            type='text'
            name='napomena'
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
              Dodaj Polaznika
            </Button>
          </Col>
        </Row>
      </Form>
    </Container>
  );
}
