import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { Link, useNavigate,  } from "react-router-dom";
import { RoutesNames } from "../../constants";
import OsobaService from "../../services/OsobaService";
import useError from "../../hooks/useError";


export default function OsobeDodaj() {
    const navigate = useNavigate();
    const { prikaziError } = useError();
    

    async function dodajOsobu(osoba){
        const odgovor = await OsobaService.dodaj(osoba);
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
          <InputText atribut='brojTelefona' vrijednost='' />
          <InputText atribut='email' vrijednost='' />
          <Akcije odustani={RoutesNames.OSOBE_PREGLED} akcija='Dodaj osobu' />       
        </Form>
      </Container>
    );
  }