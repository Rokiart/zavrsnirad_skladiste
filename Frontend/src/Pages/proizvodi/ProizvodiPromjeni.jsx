import { useEffect, useRef, useState } from 'react';
import { Button, Col, Container, Form, Image, Row } from 'react-bootstrap';
import { useNavigate, useParams } from 'react-router-dom';
import Service from '../../services/ProizvodService';
import { dohvatiPorukeAlert } from '../../services/httpService';
import {App, RoutesNames } from '../../constants';
import useError from "../../hooks/useError";
import InputText from '../../Components/InputText';
import Akcije from '../../Components/Akcije';
import Cropper from 'react-cropper';
import 'cropperjs/dist/cropper.css';
import nepoznato from '../../assets/nepoznato.png'; 
import useLoading from '../../hooks/useLoading';





export default function ProizvodiPromjeni() {
  const [proizvod, setProizvod] = useState({});

  const cropperRef = useRef(null);
 

  const routeParams = useParams();
  const navigate = useNavigate();
  const { prikaziError } = useError();
  const { showLoading, hideLoading } = useLoading();
  const [trenutnaSlika, setTrenutnaSlika] = useState('');
  const [slikaZaCrop, setSlikaZaCrop] = useState('');
  const [slikaZaServer, setSlikaZaServer] = useState('');


  async function dohvatiProizvod() {
    showLoading();
    const odgovor = await Service.getBySifra('Proizvod',routeParams.sifra);
      if(!odgovor.ok){
        hideLoading();
        prikaziError(odgovor.podaci);
       
        return;
      }
      setProizvod(odgovor.podaci);
      if(odgovor.podaci.slika!=null){
        setTrenutnaSlika(App.URL + odgovor.podaci.slika + `?${Date.now()}`);
      }else{
        setTrenutnaSlika(nepoznato);
      }
      hideLoading();
    }

    useEffect(() => {
      dohvatiProizvod();
    }, []);

    async function promjeniProizvod(proizvod) {
      showLoading();
      const odgovor = await Service.promjeni('Proizvod',routeParams.sifra, proizvod);
      if(odgovor.ok){
        hideLoading();
        navigate(RoutesNames.PROIZVODI_PREGLED);
        return;
      }
      alert(dohvatiPorukeAlert(odgovor.podaci));
      hideLoading();
    }


  

    function handleSubmit(e) {
      e.preventDefault();
  
      const podaci = new FormData(e.target);
      promjeniProizvod({
        naziv: podaci.get('naziv'),
        sifraProizvoda: podaci.get('sifraProizvoda'),
        mjernaJedinica: podaci.get('mjernaJedinica'),
        slika: slikaZaServer || proizvod.slika || '' // Koristi novu sliku ako postoji, inače trenutnu sliku, ili prazan string ako nema nijednu
      });
    }
  
  

    function onCrop() {
      setSlikaZaServer(cropperRef.current.cropper.getCroppedCanvas().toDataURL());
    }
  
    function onChangeImage(e) {
      e.preventDefault();
  
      let files;
      if (e.dataTransfer) {
        files = e.dataTransfer.files;
      } else if (e.target) {
        files = e.target.files;
      }
      const reader = new FileReader();
      reader.onload = () => {
        setSlikaZaCrop(reader.result);
      };
      try {
        reader.readAsDataURL(files[0]);
      } catch (error) {
        console.error(error);
      }
    }
  
    async function spremiSliku() {
      showLoading();
      const base64 = slikaZaServer;
  
      const odgovor = await Service.postaviSliku(routeParams.sifra, {base64: base64.replace('data:image/png;base64,', '')});
      if(!odgovor.ok){
        hideLoading();
        prikaziError(odgovor.podaci);
        return;
      }
      //Date.now je zbog toga što se src na image komponenti cache-ira
      //pa kad promjenimo sliku url ostane isti i trenutna slika se ne updatea
      setTrenutnaSlika(slikaZaServer);
      hideLoading();
    }
  


return (
  <Container className='mt-4'>
     <Row>
      <Col key='1' sm={12} lg={6} md={6}>
        <Form onSubmit={handleSubmit}>
         <InputText atribut='Naziv' vrijednost={proizvod.naziv} />
         <InputText atribut='sifraProizvoda' vrijednost={proizvod.sifraProizvoda} />
         <InputText atribut='mjernaJedinica' vrijednost={proizvod.mjernaJedinica} />
          <Akcije odustani={RoutesNames.PROIZVODI_PREGLED} akcija='Promjeni proizvod' /> 
        </Form>
        <Row className='mb-4'>
            <Col key='1' sm={12} lg={6} md={12}>
              <p className='form-label'>Trenutna slika</p>
              <Image
               
                src={trenutnaSlika}
                className='slika'
              />
            </Col>
            <Col key='2' sm={12} lg={6} md={12}>
              {slikaZaServer && (
                <>
                  <p className='form-label'>Nova slika</p>
                  <Image
                    src={slikaZaServer || slikaZaCrop}
                    className='slika'
                  />
                </>
              )}
            </Col>
          </Row>
      </Col>
      <Col key='2' sm={12} lg={6} md={6}>
      <input className='mb-3' type='file' onChange={onChangeImage} />
            <Button disabled={!slikaZaServer} onClick={spremiSliku}>
              Spremi sliku
            </Button>

            <Cropper
              src={slikaZaCrop}
              style={{ height: 400, width: '100%' }}
              initialAspectRatio={1}
              guides={true}
              viewMode={1}
              minCropBoxWidth={50}
              minCropBoxHeight={50}
              cropBoxResizable={false}
              background={false}
              responsive={true}
              checkOrientation={false}
              cropstart={onCrop}
              cropend={onCrop}
              ref={cropperRef}
            />
      </Col>
    </Row>
    
  </Container>
);
}