import React from 'react';
import './Navbar.css';
import { FaUserCircle } from 'react-icons/fa';

const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="navbar-logo">
        <h1>EventSys</h1>
      </div>
      <ul className="navbar-links">
        <li><a href="/events">Event List</a></li>
        <li><a href="/event-detail">Event Detail</a></li>
        <li><a href="/event-create">Create Event</a></li>
        <li><a href="/event-update">Update Event</a></li>
      </ul>
      <div className="navbar-profile">
        <FaUserCircle size={30} />
      </div>
    </nav>
  );
};

export default Navbar;
