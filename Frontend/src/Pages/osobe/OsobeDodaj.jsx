import { Container, Form } from "react-bootstrap";
import { useNavigate  } from "react-router-dom";
import { RoutesNames } from "../../constants";
import OsobaService from "../../services/OsobaService";
import useError from "../../hooks/useError";
import InputText from '../../Components/InputText';
import Akcije from "../../Components/Akcije";


export default function OsobeDodaj() {
    showLoading();
    const navigate = useNavigate();
    const { prikaziError } = useError();
    

    async function dodajOsobu(Osoba){
        const odgovor = await OsobaService.dodaj('Osoba',Osoba);
        if(odgovor.ok){
          hideLoading();
          navigate(RoutesNames.OSOBE_PREGLED);
          return
        }
        {
          //console.log(odgovor);
          prikaziError(odgovor.podaci);
          hideLoading();
        }
    }

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
       

        dodajOsobu({
          ime: podaci.get('ime'),
          prezime: podaci.get('prezime'),
          brojtelefona: podaci.get('brojtelefona'),
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
          <Akcije odustani={RoutesNames.OSOBE_PREGLED} akcija='Dodaj osobu' />       
        </Form>
      </Container>
    );
  }