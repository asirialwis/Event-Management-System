import React from 'react';
import './Footer.css'; // Ensure to create a corresponding CSS file

const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer-container">
        <div className="footer-about">
          <h3>About Us</h3>
          <p>We provide a seamless event management experience, helping you organize and attend events with ease.</p>
        </div>
        <div className="footer-links">
          <h3>Quick Links</h3>
          <ul>
            <li><a href="#event-list">Event List</a></li>
            <li><a href="#event-detail">Event Detail</a></li>
            <li><a href="#event-create">Create Event</a></li>
            <li><a href="#event-update">Update Event</a></li>
          </ul>
        </div>
        <div className="footer-contact">
          <h3>Contact Us</h3>
          <p>Email: contact@eventmanager.com</p>
          <p>Phone: +123 456 7890</p>
          <div className="footer-social">
            <a href="#"><i className="fab fa-facebook-f"></i></a>
            <a href="#"><i className="fab fa-twitter"></i></a>
            <a href="#"><i className="fab fa-instagram"></i></a>
          </div>
        </div>
      </div>
      <div className="footer-bottom">
        <p>&copy; 2025 Event Management System. All rights reserved.</p>
      </div>
    </footer>
  );
};

export default Footer;
