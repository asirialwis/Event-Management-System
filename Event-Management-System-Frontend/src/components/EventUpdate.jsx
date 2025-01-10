import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './EventUpdate.css';

const EventUpdate = () => {
  const [events, setEvents] = useState([]);
  const [selectedEvent, setSelectedEvent] = useState(null);
  const [selectedAttendee, setSelectedAttendee] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [updateType, setUpdateType] = useState('event'); // 'event' or 'attendee'

  // Event fields
  const [eventDetails, setEventDetails] = useState({
    name: '',
    description: '',
    date: '',
    location: '',
    createdBy: '',
    capacity: '',
    tags: '',
  });

  // Attendee fields
  const [attendeeName, setAttendeeName] = useState('');

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const response = await axios.get('http://localhost:5071/api/event/getall');
        setEvents(response.data);
      } catch (error) {
        console.error('Error fetching events:', error);
      }
    };

    fetchEvents();
  }, []);

  const handleEventUpdate = async () => {
    try {
      await axios.put(`http://localhost:5071/api/event/update-event/${selectedEvent.id}`, eventDetails);
      alert('Event updated successfully!');
    } catch (error) {
      console.error('Error updating event:', error);
    }
  };

  const handleAttendeeUpdate = async () => {
    try {
      await axios.put(`http://localhost:5071/api/attendee/${selectedEvent.id}/update/${selectedAttendee.id}`, {
        name: attendeeName,
      });
      alert('Attendee updated successfully!');
    } catch (error) {
      console.error('Error updating attendee:', error);
    }
  };

  const handleDelete = async () => {
    if (updateType === 'event') {
      try {
        await axios.delete(`http://localhost:5071/api/event/${selectedEvent.id}`);
        alert('Event deleted successfully!');
        setEvents(events.filter(event => event.id !== selectedEvent.id));
      } catch (error) {
        console.error('Error deleting event:', error);
      }
    } else {
      try {
        await axios.delete(`http://localhost:5071/api/attendee/${selectedEvent.id}/attendees/${selectedAttendee.id}`);
        alert('Attendee deleted successfully!');
        setSelectedEvent({
          ...selectedEvent,
          attendees: selectedEvent.attendees.filter(att => att.id !== selectedAttendee.id),
        });
      } catch (error) {
        console.error('Error deleting attendee:', error);
      }
    }
    setModalOpen(false);
  };

  const openUpdateModal = (type, event = null, attendee = null) => {
    setUpdateType(type);
    setSelectedEvent(event);
    setSelectedAttendee(attendee);
    if (type === 'event') {
      setEventDetails({
        name: event.name,
        description: event.description,
        date: event.date,
        location: event.location,
        createdBy: event.createdBy,
        capacity: event.capacity,
        tags: event.tags,
      });
    } else if (type === 'attendee') {
      setAttendeeName(attendee.name);
    }
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
    setSelectedEvent(null);
    setSelectedAttendee(null);
  };

  return (
    <div className="event-update">
      <h1 className="heading">Event Management</h1>
      <ul className="event-list">
        {events.map(event => (
          <li key={event.id}>
            <div className="event-info" onClick={() => openUpdateModal('event', event)}>
              <h3>{event.name}</h3>
              <p>Date: {new Date(event.date).toLocaleDateString()}</p>
            </div>
            <ul className="attendee-list">
              {event.attendees.map(attendee => (
                <li key={attendee.id} onClick={() => openUpdateModal('attendee', event, attendee)}>
                  <p>{attendee.name} ({attendee.email})</p>
                </li>
              ))}
            </ul>
          </li>
        ))}
      </ul>

      {modalOpen && (
        <div className="modal">
          <div className="modal-content">
            <span className="close-btn" onClick={closeModal}>&times;</span>
            <h2 className="modal-heading">{updateType === 'event' ? 'Update Event' : 'Update Attendee'}</h2>
            {updateType === 'event' ? (
              <>
                {Object.keys(eventDetails).map(field => (
                  <div key={field}>
                    <label>{field.charAt(0).toUpperCase() + field.slice(1)}:</label>
                    <input
                      type="text"
                      value={eventDetails[field]}
                      onChange={(e) => setEventDetails({ ...eventDetails, [field]: e.target.value })}
                    />
                  </div>
                ))}
                <button className="update-btn" onClick={handleEventUpdate}>Update</button>
              </>
            ) : (
              <>
                <label>Name:</label>
                <input
                  type="text"
                  value={attendeeName}
                  onChange={(e) => setAttendeeName(e.target.value)}
                />
                <button className="update-btn" onClick={handleAttendeeUpdate}>Update</button>
              </>
            )}
            <button className="delete-btn" onClick={handleDelete}>Delete</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default EventUpdate;
