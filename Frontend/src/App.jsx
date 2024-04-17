
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

import Skladistari from './Pages/skladistari/Skladistari'
import SkladistariDodaj from './Pages/skladistari/SkladistariDodaj'
import SkladistariPromjeni from './Pages/skladistari/SkladistariPromjeni'


import Izdatnice from './Pages/izdatnice/Izdatnice'
import IzdatniceDodaj from './Pages/izdatnice/IzdatniceDodaj'
import IzdatnicePromjeni from './Pages/izdatnice/IzdatnicePromjeni'

import IzdatniceProizvodi from './Pages/izdatniceProizvodi/IzdatniceProizvodi'

import useError from "./hooks/useError"
import ErrorModal from './Components/ErrorModal'

import LoadingSpinner from "./Components/LoadingSpinner"
import Login from './Pages/Login'
//import NadzornaPloca from './Pages/NadzornaPloca'
import useAuth from "./hooks/useAuth"






function App() {

  
  const { errors, prikaziErrorModal, sakrijError } = useError();
  const { isLoggedIn } = useAuth();
  return (
    <>
      <LoadingSpinner />
      <ErrorModal show={prikaziErrorModal} errors={errors} onHide={sakrijError} />
      <NavBar />
      <Routes>
      <Route path={RoutesNames.HOME} element={<Pocetna />} />
      {isLoggedIn ? (  
      <>
        {/* <Route path={RoutesNames.NADZORNA_PLOCA} element={<NadzornaPloca />} /> */}
       

        <Route path={RoutesNames.IZDATNICEPROIZVODI_PREGLED} element={<IzdatniceProizvodi/>} />
        
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
    ) : (
          <>
            <Route path={RoutesNames.LOGIN} element={<Login />} />
          </>
        )}
     </Routes>
    </>
  )
}

export default App
