import { useEffect, useState } from "react";
import { Container, Table } from "react-bootstrap";
import OsobaService from "../../services/OsobaService";
import { Link } from "react-router-dom";
import { ImUsers } from "react-icons/im";
import { RiDeleteBin7Fill } from "react-icons/ri";
import { RoutesNames } from "../../Constants";




export default function Osobe(){
    const[Osobe,setOsobe] =useState();
    let navigate = useNavigate(); 

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

async function obrisiOsoba(sifra) {
    const odgovor = await OsobaService.obrisi(sifra);

    if (odgovor.ok) {
        dohvatiOsobe();
    } else {
      alert(odgovor.poruka);
    }
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
                        <td className="sredina">{osoba.brojTelefona}</td>
                        <td className="sredina">{osoba.email}
                       
                        <Button
                                        variant='primary'
                                        onClick={()=>{navigate(`/osobe/${osoba.sifra}`)}}
                                    >
                                        <FaEdit 
                                    size={25}
                                    />
                                    </Button>
                               
                                
                                    &nbsp;&nbsp;&nbsp;
                                    <Button
                                        variant='danger'
                                        onClick={() => obrisiOsoba(osoba.sifra)}
                                    >
                                        <FaTrash
                                        size={25}/>
                                    </Button>
                        </td>
                    </tr>
                ))}
            </tbody>
         </Table>
       </Container>
    );
}