import { Container, Form } from "react-bootstrap";
import { useNavigate  } from "react-router-dom";
import { RoutesNames } from "../../constants";
import SkladistarService from "../../services/SkladistarService";
import useError from "../../hooks/useError";
import InputText from "../../Components/InputText";
import Akcije from "../../Components/Akcije";



export default function SkladistareDodaj() {
    const navigate = useNavigate();
    const { prikaziError } = useError();

    async function dodajSkladistara(skladistar){
        const odgovor = await SkladistarService.dodaj(skladistar);
        if(odgovor.ok){
          navigate(RoutesNames.SKLADISTARI_PREGLED);
          return
        }
        prikaziError(odgovor.podaci);
      }

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
       

        dodajSkladistara({
            ime: podaci.get('ime'),
            prezime: podaci.get('prezime'),
            brojTelefona: podaci.get('Broj Telefona'),
            email: podaci.get('email')
            
          });         
    }

    return (
      <Container>
        <Form onSubmit={handleSubmit}>
          <InputText atribut='ime' vrijednost='' />
          <InputText atribut='prezime' vrijednost='' />
          <InputText atribut='brojTelefona' vrijednost='' />
          <InputText atribut='email' vrijednost='' />
         <Akcije odustani={RoutesNames.SKLADISTARI_PREGLED} akcija='Dodaj predavača' />       
        </Form>
      </Container>
    );
  }
  