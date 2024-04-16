
import { useEffect, useState } from "react";
import { Container, Form } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import OsobaService from "../../services/OsobaService";
import { RoutesNames } from "../../constants";
import useError from "../../hooks/useError";

import Akcije from "../../Components/Akcije";
import InputText from "../../Components/InputText";





export default function OsobePromjeni(){

    const [osoba,setOsoba] = useState({});
    const routeParams = useParams();
    const navigate =useNavigate();
    const { prikaziError } = useError();
    
    

    async function dohvatiOsobu(){
        const odgovor = await OsobaService
        .getBySifra('Osoba',routeParams.sifra)
        if(!odgovor.ok){
            prikaziError(odgovor.podaci);
            return;
          }
          setOsoba(odgovor.podaci);
    }

    async function promjeniOsobu(osoba){
        const odgovor = await OsobaService.promjeni('Osoba',routeParams.sifra,osoba);
        if(odgovor.ok){
          navigate(RoutesNames.OSOBE_PREGLED);
          return;
        }
        prikaziError(odgovor.podaci);
    }

    useEffect(()=>{
        
        dohvatiOsobu();
    },[]);

   

    function handleSubmit(e){
        e.preventDefault();
        const podaci = new FormData(e.target);
            
            promjeniOsobu({
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