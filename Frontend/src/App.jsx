
import { Route, Routes,  } from 'react-router-dom'
import Pocetna from './Pages/Pocetna'
import { RoutesNames } from './constants'
import NavBar from './Components/NavBar'

import 'bootstrap/dist/css/bootstrap.min.css'
import './App.css';

import Osobe from './Pages/osobe/Osobe'
import OsobeDodaj from './Pages/osobe/OsobeDodaj'
import OsobePromjeni from './Pages/osobe/OsobePromjeni'

import Proizvodi from './Pages/proizvodi/Proizvodi'
import ProizvodiDodaj from './Pages/proizvodi/ProizvodiDodaj'
import ProizvodiPromjeni from './Pages/proizvodi/ProizvodiPromjeni'

import Skladistari from './Pages/Skladistari/Skladistari'
import SkladistariDodaj from './Pages/Skladistari/SkladistariDodaj'
import SkladistariPromjeni from './Pages/Skladistari/SkladistariPromjeni'


import Izdatnice from './Pages/izdatince/Izdatnice'
import IzdatniceDodaj from './Pages/izdatince/IzdatniceDodaj'
import IzdatnicePromjeni from './Pages/izdatince/IzdatnicePromjeni'


import useError from "./hooks/useError"
import ErrorModal from './Components/ErrorModal'

function App() {

  
  const { errors, prikaziErrorModal, sakrijError } = useError();
  return (
    <>
      <ErrorModal show={prikaziErrorModal} errors={errors} onHide={sakrijError} />
      <NavBar />
      <Routes>  
      <>
        <Route path={RoutesNames.HOME} element={<Pocetna/>} />
        
        <Route path={RoutesNames.OSOBE_PREGLED} element={<Osobe/>} />
        <Route path={RoutesNames.OSOBE_NOVI} element={<OsobeDodaj/>} />
        <Route path={RoutesNames.OSOBE_PROMJENI} element={<OsobePromjeni/>} />

        <Route path={RoutesNames.PROIZVODI_PREGLED} element={<Proizvodi/>} />
        <Route path={RoutesNames.PROIZVODI_NOVI} element={<ProizvodiDodaj/>} />
        <Route path={RoutesNames.PROIZVODI_PROMJENI} element={<ProizvodiPromjeni/>} />

        <Route path={RoutesNames.SKLADISTARI_PREGLED} element={<Skladistari/>} />
        <Route path={RoutesNames.SKLADISTARI_NOVI} element={<SkladistariDodaj/>} />
        <Route path={RoutesNames.SKLADISTARI_PROMJENI} element={<SkladistariPromjeni/>} />

        <Route path={RoutesNames.IZDATNICE_PREGLED} element={<Izdatnice/>} />
        <Route path={RoutesNames.IZDATNICE_NOVI} element={<IzdatniceDodaj/>} />
        <Route path={RoutesNames.IZDATNICE_PROMJENI} element={<IzdatnicePromjeni/>} />
      </>
     </Routes>
    </>
  )
}

export default App
