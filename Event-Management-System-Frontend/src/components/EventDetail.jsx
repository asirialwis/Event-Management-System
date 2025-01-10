import React, { useEffect, useState } from "react";
import axios from "axios";
import { FaTimes } from "react-icons/fa"; 
import "./EventDetail.css";

const EventDetail = () => {
  const [events, setEvents] = useState([]);
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [attendeeName, setAttendeeName] = useState("");
  const [attendeeEmail, setAttendeeEmail] = useState("");
  const [modalOpen, setModalOpen] = useState(false);
  const [errors, setErrors] = useState({ name: "", email: "", server: "" });

  useEffect(() => {     
    const fetchEvents = async () => {
      try {
        const response = await axios.get(
          "http://localhost:5071/api/event/getall"
        );
        setEvents(response.data);
      } catch (error) {
        console.error("Error fetching events:", error);
      }
    };

    fetchEvents();
  }, []);

  const handleEventClick = (event) => {
    setSelectedEvent(event);
    setModalOpen(true);
  };

  const handleAddAttendee = async () => {
    let valid = true;
    const newErrors = { name: "", email: "", server: "" };

    if (!attendeeName) {
      newErrors.name = "Name is required.";
      valid = false;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!attendeeEmail || !emailRegex.test(attendeeEmail)) {
      newErrors.email = "Please enter a valid email.";
      valid = false;
    }

    setErrors(newErrors);

    if (!valid) return;

    const newAttendee = {
      name: attendeeName,
      email: attendeeEmail,
    };

    try {
      await axios.post(
        `http://localhost:5071/api/attendee/add-attendee/${selectedEvent.id}`,
        newAttendee
      );
      alert("Attendee added successfully!");
      setErrors({ name: "", email: "", server: "" });
    } catch (error) {
      console.error("Error adding attendee:", error);
      setErrors((prevErrors) => ({
        ...prevErrors,
        server: error.response?.data?.message || "An unexpected error occurred.",
      }));
    }
  };

  const handleCloseModal = () => {
    setModalOpen(false);
    setSelectedEvent(null);
    setErrors({ name: "", email: "", server: "" });
  };

  return (
    <div className="event-detail">
      <h1>Event List</h1>
      <ul className="event-list">
        {events.map((event) => (
          <li key={event.id} onClick={() => handleEventClick(event)}>
            <h3>{event.name}</h3>
            <p>{new Date(event.date).toLocaleDateString()}</p>
          </li>
        ))}
      </ul>

      {modalOpen && selectedEvent && (
        <div className="modal">
          <div className="modal-content">
            <span className="close-btn" onClick={handleCloseModal}>
              <FaTimes />
            </span>
            <h2>{selectedEvent.name}</h2>
            <p>{selectedEvent.description}</p>
            <p>Date: {new Date(selectedEvent.date).toLocaleDateString()}</p>
            <p>Location: {selectedEvent.location}</p>
            <p>Remaining Capacity: {selectedEvent.remainingCapacity}</p>

            <h3>Attendees:</h3>
            <ul>
              {selectedEvent.attendees.map((attendee) => (
                <li key={attendee.id}>
                  {attendee.name} ({attendee.email})
                </li>
              ))}
            </ul>

            <h3>Add Attendee</h3>
            <div className="input-container">
              <input
                type="text"
                placeholder="Name"
                value={attendeeName}
                onChange={(e) => {
                  setAttendeeName(e.target.value);
                  if (e.target.value) setErrors((prev) => ({ ...prev, name: "" }));
                }}
              />
              {errors.name && <p className="error-message">{errors.name}</p>}
              
              <input
                type="email"
                placeholder="Email"
                value={attendeeEmail}
                onChange={(e) => {
                  setAttendeeEmail(e.target.value);
                  if (e.target.value && emailRegex.test(e.target.value)) {
                    setErrors((prev) => ({ ...prev, email: "" }));
                  }
                }}
              />
              {errors.email && <p className="error-message">{errors.email}</p>}
            </div>

            <button onClick={handleAddAttendee}>Add Attendee</button>
            {errors.server && <p className="error-message">{errors.server}</p>}
          </div>
        </div>
      )}
    </div>
  );
};

export default EventDetail;
