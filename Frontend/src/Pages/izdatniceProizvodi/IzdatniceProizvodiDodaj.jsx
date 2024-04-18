import { Container, Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { RoutesNames } from '../../constants';
import ProizvodService from '../../services/IzdatnicaProizvodService';
import useError from '../../hooks/useError';
import InputText from '../../Components/InputText';
import Akcije from '../../Components/Akcije';

import useLoading from '../../hooks/useLoading';




export default function ProizvodiDodaj() {
  const navigate = useNavigate();
  const { prikaziError } = useError();
  const { showLoading, hideLoading } = useLoading();

  async function dodajProizvod(Proizvod) {
    showLoading();
    const odgovor = await ProizvodService.dodaj('Proizvod',Proizvod);
    if (odgovor.ok) {
      hideLoading();
      navigate(RoutesNames.PROIZVODI_PREGLED);
      return
    }
    prikaziError(odgovor.podaci);
    hideLoading();
  }

  function handleSubmit(e) {
    e.preventDefault();
    const podaci = new FormData(e.target);
    dodajProizvod({
      naziv: podaci.get('naziv'),
      sifraProizvoda: podaci.get('sifraProizvoda'),
      mjernaJedinica: podaci.get('mjernaJedinica'),
     
      slika: ''
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
