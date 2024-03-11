
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

function App() {

  
  return (
    <>
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
      </>
     </Routes>
    </>
  )
}

export default App
