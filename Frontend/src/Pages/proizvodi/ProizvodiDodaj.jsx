import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { RoutesNames } from '../../constants';
import ProizvodService from '../../services/ProizvodService';




export default function ProizvodiDodaj() {
  const navigate = useNavigate();


  async function dodajProizvod(proizvod) {
    const odgovor = await ProizvodService.dodaj(proizvod);
    if (odgovor.ok) {
      navigate(RoutesNames.PROIZVODI_PREGLED);
    } else {
      console.log(odgovor);
          alert(odgovor.poruka);
    }
  }

  function handleSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);


    dodajProizvod({
      naziv: podaci.get('naziv'),
      sifraProizvoda: podaci.get('sifraproizvoda'),
      mjernaJedinica: podaci.get('mjernajedinica')
      
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

        <Form.Group className='mb-3' controlId='sifraproizvoda'>
          <Form.Label>Sifra Proizvoda</Form.Label>
          <Form.Control
            type='text'
            name='sifraproizvoda'
            placeholder='sifraproizvoda'
            maxLength={255}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='mjernajedinica'>
          <Form.Label>Mjerna Jedinica</Form.Label>
          <Form.Control
            type='text'
            name='mjernajedinica'
            placeholder='mjernajedinica'
            maxLength={11}
            required
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
