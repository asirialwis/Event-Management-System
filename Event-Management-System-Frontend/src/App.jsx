import { useState } from 'react'
import {BrowserRouter,Routes,Route,Link} from 'react-router-dom'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
// import './App.css'
import Navbar from './components/Navbar'
import EventCreate from './components/EventCreate'
import EventDetail from './components/EventDetail'
import EventUpdate from './components/EventUpdate'
import Footer from './components/Footer'



function App() {
  

  return (
    <BrowserRouter>
    <Routes>
      <Route path="/events" element={<EventDetail/>}/>
      <Route path="/event-detail" element={<EventDetail/>}/>
      <Route path="/event-create" element={<EventCreate/>}/>
      <Route path="/event-update" element={<EventUpdate/>}/>
    </Routes>
      <Navbar/>
      
      <Footer/>
    </BrowserRouter>
  )
}

export default App
