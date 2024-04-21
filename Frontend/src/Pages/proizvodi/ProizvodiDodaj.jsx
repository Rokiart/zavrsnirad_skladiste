import { Container, Form } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

import Service from '../../services/ProizvodService';
import { RoutesNames } from '../../constants';
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
    const odgovor = await Service.dodaj('Proizvod',Proizvod);
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
    const slika = podaci.get('slika'); // Prikupljanje slike iz FormData objekta
    dodajProizvod({
      naziv: podaci.get('naziv'),
      sifraProizvoda: podaci.get('sifraProizvoda'),
      mjernaJedinica: podaci.get('mjernaJedinica'),
     
      slika: slika ? slika : '' // Ako ne postoji slika, ostavljamo prazno polje
    });
  }

  

  return (
    <Container className='mt-4'>
      <Form onSubmit={handleSubmit} enctype="multipart/form-data">
        <InputText atribut='naziv' vrijednost='' />
        <InputText atribut='sifraProizvoda' vrijednost='' />
        <InputText atribut='mjernaJedinica' vrijednost='' />
         Input polje za odabir slike
         <Form.Group controlId="slika">
          <Form.Label>Odaberi sliku</Form.Label>
          <Form.Control type="file" name="slika" accept="image/*" />
        </Form.Group>
        <Akcije odustani={RoutesNames.PROIZVODI_PREGLED} akcija='Dodaj prizvod' />  
      </Form>
    </Container>
  );
}
