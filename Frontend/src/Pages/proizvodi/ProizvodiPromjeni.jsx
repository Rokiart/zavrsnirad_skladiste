import { useEffect, useState } from 'react';
import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link, useNavigate, useParams } from 'react-router-dom';
import ProizvodService from '../../services/ProizvodService';
import { RoutesNames } from '../../constants';
import { dohvatiPorukeAlert } from '../../services/httpService';


export default function ProizvodiPromjeni() {
  const [proizvod, setProizvod] = useState({});

  const routeParams = useParams();
  const navigate = useNavigate();


  async function dohvatiProizvod() {

    const odgovor = await ProizvodService
      .getBySifra(routeParams.sifra)
      if(!odgovor.ok){
        alert(dohvatiPorukeAlert(odgovor.podaci));
        return;
      }
      setProizvod(odgovor.podaci);

  }

  useEffect(() => {
    dohvatiProizvod();
  }, []);

  async function promjeniProizvod(proizvod) {
    const odgovor = await ProizvodService.promjeni(routeParams.sifra, proizvod);

    if (odgovor.ok) {
      navigate(RoutesNames.PROIZVODI_PREGLED);
      return;
    }
    alert(dohvatiPorukeAlert(odgovor.podaci));
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

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit}>

      <Form.Group className='mb-3' controlId='naziv'>
          <Form.Label>Naziv</Form.Label>
          <Form.Control
            type='text'
            name='naziv'
            defaultValue={proizvod.naziv}
            maxLength={50}
            required
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='sifraProizvoda'>
          <Form.Label>Šifra Proizvoda</Form.Label>
          <Form.Control
            type='text'
            name='sifraProizvoda'
            defaultValue={proizvod.sifraProizvoda}
            maxLength={50}
          
          />
        </Form.Group>

        <Form.Group className='mb-3' controlId='mjernaJedinica'>
          <Form.Label>Mjerna Jedinica</Form.Label>
          <Form.Control
            type='text'
            name='mjernaJedinica'
            defaultValue={proizvod.mjernaJedinica}
            maxLength={20}
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
              Promjeni podatke proizvoda
            </Button>
          </Col>
        </Row>
      </Form>
    </Container>
  );
}
