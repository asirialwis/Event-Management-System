import React, { useEffect, useState } from "react";
import axios from "axios";
import { FaTimes } from "react-icons/fa"; // Import the close icon from react-icons
import "./EventDetail.css";

const EventDetail = () => {
  const [events, setEvents] = useState([]);
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [attendeeName, setAttendeeName] = useState("");
  const [attendeeEmail, setAttendeeEmail] = useState("");
  const [modalOpen, setModalOpen] = useState(false); // State to control modal visibility

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
    setModalOpen(true); // Open the modal on event click
  };

  const handleAddAttendee = async () => {
    if (!selectedEvent) return;

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
    } catch (error) {
      console.error("Error adding attendee:", error);
    }
  };

  const handleCloseModal = () => {
    setModalOpen(false);
    setSelectedEvent(null);
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

      {/* Modal for event details */}
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
                onChange={(e) => setAttendeeName(e.target.value)}
              />
              <input
                type="email"
                placeholder="Email"
                value={attendeeEmail}
                onChange={(e) => setAttendeeEmail(e.target.value)}
              />
            </div>
            <button onClick={handleAddAttendee}>Add Attendee</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default EventDetail;
