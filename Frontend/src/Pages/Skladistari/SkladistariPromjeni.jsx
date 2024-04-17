
import { useEffect, useState } from "react";
import { Container, Form } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import Service from "../../services/SkladistarService";
import { RoutesNames } from "../../constants";
import useError from "../../hooks/useError";
import InputText from "../../Components/InputText";
import Akcije from "../../Components/Akcije";


export default function SkladistaraPromjeni(){

    const [skladistar, setSkladistar] = useState({});

    const routeParams = useParams();
    const navigate = useNavigate();
    const { prikaziError } = useError();

    async function dohvatiSkladistara(){
        const odgovor = await Service.getBySifra('Skladistar',routeParams.sifra);
        if(!odgovor.ok){
            prikaziError(odgovor.podaci);
            return;
          }
          setSkladistar(odgovor.podaci);
    }

    async function promjeniSkladistar(skladistar){
        const odgovor = await Service.promjeni('Skladistar',routeParams.sifra,skladistar);
        if(odgovor.ok){
          navigate(RoutesNames.SKLADISTARI_PREGLED);
          return;
        }
        prikaziError(odgovor.podaci);
    }

    useEffect(()=>{
        
        dohvatiSkladistara();
    },[]);

  

    function handleSubmit(e){
        e.preventDefault();
        

        const podaci = new FormData(e.target);
        
       promjeniSkladistar({
        ime: podaci.get('ime'),
        prezime: podaci.get('prezime'),
        brojTelefona: podaci.get('brojtelefona'),
        email: podaci.get('email')
       }); 
         
    }


    return (
        <Container>
          <Form onSubmit={handleSubmit}>
            <InputText atribut='ime' vrijednost={skladistar.ime} />
            <InputText atribut='prezime' vrijednost={skladistar.prezime} />
            <InputText atribut='brojTelefona' vrijednost={skladistar.brojTelefona} />
            <InputText atribut='email' vrijednost={skladistar.email} />
           <Akcije odustani={RoutesNames.SKLADISTARI_PREGLED} akcija='Promjeni skladistara' />
          </Form>
        </Container>
      );
    }
    