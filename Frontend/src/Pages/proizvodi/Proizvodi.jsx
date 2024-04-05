import { useEffect, useState } from "react";
import { Button,  Card, Col, Container, Form, Pagination, Row  } from "react-bootstrap";
import ProizvodService from "../../services/ProizvodService";
import { IoIosAdd } from "react-icons/io";
import { FaEdit, FaTrash } from "react-icons/fa";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { RoutesNames,App } from "../../constants";
import nepoznato from '../../assets/nepoznato.png'; 
import useError from "../../hooks/useError";


export default function Proizvodi(){
    const [Proizvodi,setProizvodi] = useState();
    const navigate = useNavigate();
    const [uvjet, setUvjet] = useState('');
    const { prikaziError } = useError(); 
    const { showLoading, hideLoading } = useLoading();

    async function dohvatiProizvode(){
        showLoading();
        const odgovor = await ProizvodService.get('Proizvod');
        if(!odgovor.ok){
            prikaziError(odgovor.podaci);
            hideLoading();
            return;
        }
        if(odgovor.podaci.length==0){
        setProizvodi(odgovor.podaci);
        hideLoading();
        return;

    }
    setProizvodi(odgovor.podaci);
    hideLoading();
}

   


    async function obrisiProizvod(sifra) {
        showLoading();
        const odgovor = await ProizvodService.obrisi('Proizvod',sifra);
        prikaziError(odgovor.podaci);
        if (odgovor.ok) {
            dohvatiProizvode();
      
        }
        hideLoading();
      }

      useEffect(()=>{
        dohvatiProizvode();
    },[stranica, uvjet]);

    function slika(polaznik){
        if(polaznik.slika!=null){
            return App.URL + polaznik.slika+ `?${Date.now()}`;
        }
        return nepoznato;
    }
    function promjeniUvjet(e) {
        if(e.nativeEvent.key == "Enter"){
            setStranica(1);
            setUvjet(e.nativeEvent.srcElement.value);
            setPolaznici([]);
        }
    }

    function povecajStranicu() {
        setStranica(stranica + 1);
      }
    
      function smanjiStranicu() {
        if(stranica==1){
            return;
        }
        setStranica(stranica - 1);
      }
 

      return (

        <Container>
           
            <Row>
                <Col key={1} sm={12} lg={4} md={4}>
                    <Form.Control
                    type='text'
                    name='trazilica'
                    placeholder='Dio naziva [Enter]'
                    maxLength={255}
                    defaultValue=''
                    onKeyUp={promjeniUvjet}
                    />
                </Col>
                <Col key={2} sm={12} lg={4} md={4}>
                    {proizvodi && proizvodi.length > 0 && (
                            <div style={{ display: "flex", justifyContent: "center" }}>
                                <Pagination size="lg">
                                <Pagination.Prev onClick={smanjiStranicu} />
                                <Pagination.Item disabled>{stranica}</Pagination.Item> 
                                <Pagination.Next
                                    onClick={povecajStranicu}
                                />
                            </Pagination>
                        </div>
                    )}
                </Col>
                <Col key={3} sm={12} lg={4} md={4}>
                    <Link to={RoutesNames.PROIZVODI_NOVI} className="btn btn-success gumb">
                        <IoIosAdd
                        size={25}
                        /> Dodaj
                    </Link>
                </Col>
            </Row>
            
                
            <Row>
                
            { proizvodi && proizvodi.map((p) => (
           
           <Col key={p.sifra} sm={12} lg={3} md={3}>
              <Card style={{ marginTop: '1rem' }}>
              <Card.Img variant="top" src={slika(p)} className="slika"/>
                <Card.Body>
                  <Card.Title>{p.naziv} </Card.Title>
                  <Card.Text>
                    {p.naziv}
                  </Card.Text>
                  <Row>
                      <Col>
                      <Link className="btn btn-primary gumb" to={`/proizvodi/${p.sifra}`}><FaEdit /></Link>
                      </Col>
                      <Col>
                      <Button variant="danger" className="gumb"  onClick={() => obrisiProizvod(p.sifra)}><FaTrash /></Button>
                      </Col>
                    </Row>
                </Card.Body>
              </Card>
            </Col>
          ))
      }
      </Row>
      <hr />
              {proizvodi && proizvodi.length > 0 && (
                <div style={{ display: "flex", justifyContent: "center" }}>
                    <Pagination size="lg">
                    <Pagination.Prev onClick={smanjiStranicu} />
                    <Pagination.Item disabled>{stranica}</Pagination.Item> 
                    <Pagination.Next
                        onClick={povecajStranicu}
                    />
                    </Pagination>
                </div>
                )}
        </Container>


    );

}