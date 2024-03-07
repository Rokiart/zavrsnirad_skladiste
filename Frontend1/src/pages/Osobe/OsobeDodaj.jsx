import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import OsobaService from '../../services/OsobaService';
import { RoutesNames } from '../../Constants';



export default function OsobeDodaj() {
  const navigate = useNavigate();


  async function dodajOsoba(Osoba) {
    const odgovor = await OsobaService.dodaj(Osoba);
    if (odgovor.ok) {
      navigate(RoutesNames.OSOBE_PREGLED);
    } else {
      alert(odgovor.poruka.errors);
    }
  }

  function handleSubmit(e) {
    e.preventDefault();

    const podaci = new FormData(e.target);


    dodajOsoba({
      ime: podaci.get('ime'),
      prezime: podaci.get('prezime'),
      oib: podaci.get('brojTelefona'),
      email: podaci.get('email')
      
    });
  }

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit}>
        <Form.Group className='mb-3' controlId='ime'>
          <Form.Label>Ime</Form.Label>
          <Form.Control
            type='text'
            name='ime'
            placeholder='Ime Osobe'
            maxLength={255}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='prezime'>
          <Form.Label>Prezime</Form.Label>
          <Form.Control
            type='text'
            name='prezime'
            placeholder='Prezime Osobe'
            maxLength={255}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='brojTelefona'>
          <Form.Label>Broj Telefona</Form.Label>
          <Form.Control
            type='text'
            name='brojTelefona'
            placeholder='Broj Telefona'
            maxLength={25}
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='email'>
          <Form.Label>Email</Form.Label>
          <Form.Control
            type='email'
            name='email'
            placeholder='Email Osobe'
            maxLength={255}
          />
        </Form.Group>

       

       

        <Row>
          <Col>
            <Link className='btn btn-danger gumb' to={RoutesNames.OSOBE_PREGLED}>
              Odustani
            </Link>
          </Col>
          <Col>
            <Button variant='primary' className='gumb' type='submit'>
              Dodaj Osobu
            </Button>
          </Col>
        </Row>
      </Form>
    </Container>
  );
}
