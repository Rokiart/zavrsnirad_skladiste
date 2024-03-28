import { useEffect, useState } from "react";
import { Button,Container, Table } from "react-bootstrap";
import { ImManWoman } from "react-icons/im";
import { FaRegEdit } from "react-icons/fa";
import { FaRegTrashAlt } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import moment from "moment";

import IzdatnicaService from "../../services/IzdatnicaService";
import { dohvatiPorukeAlert } from "../../services/httpService";
import ProizvodService from "../../services/ProizvodService";
import { RoutesNames } from "../../constants";



export default function Izdatnice() {

    const [Izdatnice,setIzdatnice] = useState();
    let navigate = useNavigate(); 




    async function dohvatiIzdatnice(){
        const odgovor =await IzdatnicaService.get();
        if(!odgovor.ok){
            alert(dohvatiPorukeAlert(odgovor.podaci));
            return;
        }
        setIzdatnice(odgovor.podaci);
        setSifraIzdatnica(odgovor.podaci[0].sifra);
    }

  

    async function ObrisiIzdatnicu(sifra){
      
        const odgovor = await IzdatnicaService.obrisi(sifra);
        alert(dohvatiPorukeAlert(odgovor.podaci));
        if (odgovor.ok){
           
            dohvatiIzdatnice();
            dohvatiProizvodi();
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