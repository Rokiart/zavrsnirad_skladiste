import { Container, Form } from "react-bootstrap";
import { useNavigate  } from "react-router-dom";
import { RoutesNames } from "../../constants";
import Service from "../../services/OsobaService";
import useError from "../../hooks/useError";
import InputText from '../../Components/InputText';
import Akcije from "../../Components/Akcije";


export default function OsobeDodaj() {
  
    const navigate = useNavigate();
    const { prikaziError } = useError();
    

    async function dodajOsobu(Osoba){
        const odgovor = await Service.dodaj('Osoba',Osoba);
        if(odgovor.ok){
       
          navigate(RoutesNames.OSOBE_PREGLED);
          return
        }
        {
          //console.log(odgovor);
          prikaziError(odgovor.podaci);
          
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
          <InputText atribut='brojtelefona' vrijednost='' />
          <InputText atribut='email' vrijednost='' />
          <Akcije odustani={RoutesNames.OSOBE_PREGLED} akcija='Dodaj osobu' />       
        </Form>
      </Container>
    );
  }