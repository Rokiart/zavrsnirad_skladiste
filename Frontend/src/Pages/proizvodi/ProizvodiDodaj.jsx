import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import ProizvodService from '../../services/ProizvodService';
import { RoutesNames } from '../../constants';



export default function ProizvodiDodaj() {
  const navigate = useNavigate();


  async function dodajProizvod(Proizvod) {
    const odgovor = await ProizvodService.dodaj(Proizvod);
    if (odgovor.ok) {
      navigate(RoutesNames.PROIZVODI_PREGLED);
    } else {
      alert(odgovor.poruka.errors);
    }
  }

  function handleSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);


    dodajProizvod({
      naziv: podaci.get('naziv'),
      sifraProizvoda: podaci.get('Šifra proizvoda'),
      mjernaJedinica: podaci.get('Mjerna jedinica')
      
    });
  }

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit}>
        <Form.Group className='mb-3' controlId='naziv'>
          <Form.Label>Naziv</Form.Label>
          <Form.Control
            type='text'
            name='naziv'
            placeholder='Naziv'
            maxLength={255}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='sifraProizvoda'>
          <Form.Label>Sifra Proizvoda</Form.Label>
          <Form.Control
            type='text'
            name='sifraProizvoda'
            placeholder='Šifra Proizvoda'
            maxLength={255}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='mjernaJedinica'>
          <Form.Label>Mjerna Jedinica</Form.Label>
          <Form.Control
            type='text'
            name='mjernaJedinica'
            placeholder='Mjerna jedinica'
            maxLength={11}
          />
        </Form.Group>

        <Row>
          <Col>
            <Link className='btn btn-danger gumb' to={RoutesNames.PROIZVODI_PREGLED}>
              Odustani
            </Link>
          </Col>
          <Col>
            <Button variant='primary' className='gumb' type='submit'>
              Dodaj Proizvod
            </Button>
          </Col>
        </Row>
      </Form>
    </Container>
  );
}
