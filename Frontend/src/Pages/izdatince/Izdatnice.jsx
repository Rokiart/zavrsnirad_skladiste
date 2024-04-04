import { useEffect, useState } from "react";
import { Button,Container, Table } from "react-bootstrap";
import { ImManWoman } from "react-icons/im";
import { FaRegEdit } from "react-icons/fa";
import { FaRegTrashAlt } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import moment from "moment";

import IzdatnicaService from "../../services/IzdatnicaService";

import { RoutesNames } from "../../constants";
import useError from "../../hooks/useError";



export default function Izdatnice() {

    const [Izdatnice,setIzdatnice] = useState();
    let navigate = useNavigate(); 
     
     const { prikaziError } = useError();

    async function dohvatiIzdatnice(){
        const odgovor =await IzdatnicaService.get('Izdatnica');
        if(!odgovor.ok){
            prikaziError(odgovor.podaci);
            return;
        }
        setIzdatnice(odgovor.podaci);  
    }


    async function ObrisiIzdatnicu(sifra){
      
        const odgovor = await IzdatnicaService.obrisi('Izdatnica',sifra);
        prikaziError(odgovor.podaci);
        if (odgovor.ok){
           
            dohvatiIzdatnice();
         
        }
        
    }

  
       useEffect(()=>{
        dohvatiIzdatnice();
    

    },[]);

   
   function formatirajDatum(datum){
    let mdp = moment.utc(datum);
    if(mdp.hour()==0 && mdp.minutes()==0){
        return mdp.format('DD. MM. YYYY.');
    }
    return mdp.format('DD. MM. YYYY. HH:mm');
    
  }

    return(
        <Container>
             <Link to={RoutesNames.IZDATNICE_NOVI} className="btn btn-success gumb">
                <ImManWoman
                size={25}
                /> Dodaj
            </Link>
            <Table striped bordered hover responsive>
               <thead>
                  <tr>
                     <th>BrojIzdatnice</th>
                     <th>Datum</th>
                     <th>Proizvodi</th>
                     <th>Osoba</th>
                     <th>Skladistar</th> 
                     <th>Napomena</th>
                     <th>Akcija</th>
                  </tr>
               </thead>
               <tbody>
                    {Izdatnice && Izdatnice.map((izdatnica,index)=>(
                        <tr key={index}>
                            <td>{izdatnica.brojIzdatnice}</td>
                            <td>  <p>
                                {izdatnica.datum==null 
                                ? 'Nije definirano'
                                :   
                                formatirajDatum(izdatnica.datum)
                                }
                                </p>
                             
                               
                              </td>
                              <td>{izdatnica.proizvodiPopis}</td>
                            <td>{izdatnica.osobaImePrezime}</td>
                            <td>{izdatnica.skladistarImePrezime}</td> 
                            <td>{izdatnica.napomena}</td>
                            <td className="sredina">
                                <Button 
                                variant="primary"
                                onClick={()=>{navigate(`/izdatnica/${izdatnica.sifra}`)}}>
                                    <FaRegEdit
                                    size={25}
                                    />
                                </Button>
                                
                                    &nbsp;&nbsp;&nbsp;
                                <Button
                                    variant="danger"
                                    onClick={()=>ObrisiIzdatnicu(izdatnica.sifra)}
                                >
                                    <FaRegTrashAlt 
                                    size={25}
                                    />
                                </Button>

                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
      
    );


}