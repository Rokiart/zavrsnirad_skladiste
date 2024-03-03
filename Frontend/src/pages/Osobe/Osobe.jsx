import { useEffect, useState } from "react";
import { Container, Table } from "react-bootstrap";
import OsobaService from "../../services/OsobaService";
import { Link } from "react-router-dom";
import { ImUsers } from "react-icons/im";
import { RiDeleteBin7Fill } from "react-icons/ri";
import { RoutesNames } from "../../Constants";




export default function Osobe(){
    const[Osobe,setOsobe] =useState();

    async function dohvatiOsobe(){
        await OsobaService.getOsobe()
        .then((res)=>{
            setOsobe(res.data);
        })
        .catch((e)=>{
            alert(e);
        });
    }

useEffect(()=>{
    dohvatiOsobe();
},[]);

  function upisano(osoba){
    if (osoba.upisano==null) return 'Nije upisano';
    
    return 'email';
  }



    return(
       <Container>
        <Link to={RoutesNames.OSOBE_NOVE} className="btn btn-success gumb">
          <ImUsers
          size={25}
          /> Dodaj
        </Link>
         <Table>
            <thead>
                <tr>
                    <th>Ime</th>
                    <th>Prezime</th>
                    <th>Broj Telefona</th>
                    <th>Email</th>
                    <th>Akcija</th>
                </tr>
            </thead>
            <tbody>
                {osobe && osobe.map((osoba,index)=>(
                    <tr key={index}>
                        <td className="sredina">{osoba.ime}</td>
                        <td className="sredina">{osoba.prezime}</td>
                        <td className="sredina">
                            {osoba.brojTelefona==null ? 'Nije upisano'
                            : value={osoba.brojTelefona}}
                        </td>
                        <td className="sredina">
                        {upisano(osoba.upisano)}
                        </td>
                        <td className="sredina" >
                        <Link to={RoutesNames.OSOBE_PROMJENI}>
                          <ImTab
                             size={25}
                             /> 
                        </Link>
                           &nbsp; &nbsp; &nbsp;
                        <Link>
                          <RiDeleteBin7Fill
                             size={25}
                             /> 
                        </Link>
                        </td>
                    </tr>
                ))}
            </tbody>
         </Table>
       </Container>
    );
}