import { useEffect, useState } from "react";
import { Button,Container, Table } from "react-bootstrap";
import { ImManWoman } from "react-icons/im";
import { FaRegEdit } from "react-icons/fa";
import { FaRegTrashAlt } from "react-icons/fa";
import { Link, useNavigate } from "react-router-dom";
import moment from "moment";

import Service from "../../services/IzdatnicaService";

import { RoutesNames } from "../../constants";
import useError from "../../hooks/useError";
import useLoading from "../../hooks/useLoading";



export default function Izdatnice() {

    const [izdatnice,setIzdatnice] = useState();
    let navigate = useNavigate(); 
    const { showLoading, hideLoading } = useLoading();
    const { prikaziError } = useError();

    async function dohvatiIzdatnice(){
        showLoading();
        const odgovor =await Service.get('Izdatnica');
        if(!odgovor.ok){
            hideLoading();
            prikaziError(odgovor.podaci);
            return;
        }
        setIzdatnice(odgovor.podaci);  
        hideLoading();
    }


    async function ObrisiIzdatnicu(sifra){
        showLoading();
        const odgovor = await Service.obrisi('Izdatnica',sifra);
        hideLoading();
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
                     <th>Proizvod</th>
                     <th>Kolicina</th>
                     <th>Osoba</th>
                     <th>Skladistar</th> 
                     <th>Napomena</th>
                     <th>Akcija</th>
                  </tr>
               </thead>
               <tbody>
                    {izdatnice && izdatnice.map((entitet,index)=>(
                        <tr key={index}>
                            <td>{entitet.brojIzdatnice}</td>
                            <td>  <p>
                                {entitet.datum==null 
                                ? 'Nije definirano'
                                :   
                                formatirajDatum(entitet.datum)
                                }
                                </p>
                             
                               
                              </td>
                              <td>{entitet.proizvodNaziv}</td>
                              <td>{entitet.izdatnicaProizvodKolicina}</td>
                              <td>{entitet.osobaImePrezime}</td>
                              <td>{entitet.skladistarImePrezime}</td> 
                              <td>{entitet.napomena}</td>
                              <td className="sredina">
                                <Button 
                                variant="primary"
                                onClick={()=>{navigate(`/izdatnica/${entitet.sifra}`)}}>
                                    <FaRegEdit
                                    size={25}
                                    />
                                </Button>
                                
                                    &nbsp;&nbsp;&nbsp;
                                <Button
                                    variant="danger"
                                    onClick={()=>ObrisiIzdatnicu(entitet.sifra)}
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