import { Button, Col, Container, Form, Row } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { RoutesNames } from '../../constants';
import ProizvodService from '../../services/ProizvodService';
import useError from '../../hooks/useError';
import InputText from '../../Components/InputText';
import Akcije from '../../Components/Akcije';
import useError from "../../hooks/useError";




export default function ProizvodiDodaj() {
  const navigate = useNavigate();
  const { prikaziError } = useError();

  async function dodajProizvod(proizvod) {
    const odgovor = await ProizvodService.dodaj(proizvod);
    if (odgovor.ok) {
      navigate(RoutesNames.PROIZVODI_PREGLED);
    } else {
      console.log(odgovor);
      prikaziError(odgovor.podaci);
    }
  }

  function handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);
    dodajProizvod({
      naziv: podaci.get('naziv'),
      sifraProizvoda: podaci.get('sifraProizvoda'),
      mjernaJedinica: podaci.get('mjernaJedinica')
      
    });
  }

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit}>
        <InputText atribut='naziv' vrijednost='' />
        <InputText atribut='sifraProizvoda' vrijednost='' />
        <InputText atribut='mjernaJedinica' vrijednost='' />
        <Akcije odustani={RoutesNames.PROIZVODI_PREGLED} akcija='Dodaj prizvod' />  
      </Form>
    </Container>
  );
}
