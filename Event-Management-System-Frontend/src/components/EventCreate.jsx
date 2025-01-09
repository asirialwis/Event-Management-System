import React, { useState } from 'react';
import axios from 'axios';
import './EventCreate.css';

const EventCreate = () => {
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    date: '',
    location: '',
    created_by: '',
    capacity: '',
    tags: '',
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  
   
    const formattedDate = new Date(formData.date).toISOString();
  
    try {
      const response = await axios.post('http://localhost:5071/api/event/create', {
        ...formData,
        date: formattedDate, 
      });
      console.log('Event created:', response.data);
      
      setFormData({
        name: '',
        description: '',
        date: '',
        location: '',
        createdBy: '',
        capacity: '',
        tags: '',
      });
    } catch (error) {
      console.error('Error creating event:', error);
      console.log(error.response.data);
    }
  };
  

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Name</label>
        <input type="text" name="name" value={formData.name} onChange={handleChange} required />
      </div>
      <div>
        <label>Description</label>
        <textarea name="description" value={formData.description} onChange={handleChange} required></textarea>
      </div>
      <div>
        <label>Date</label>
        <input type="date" name="date" value={formData.date} onChange={handleChange} required />
      </div>
      <div>
        <label>Location</label>
        <input type="text" name="location" value={formData.location} onChange={handleChange} required />
      </div>
      <div>
        <label>Created By</label>
        <input type="text" name="createdBy" value={formData.createdBy} onChange={handleChange} required />
      </div>
      <div>
        <label>Capacity</label>
        <input type="text" name="capacity" value={formData.capacity} onChange={handleChange} required />
      </div>
      <div>
        <label>Tags</label>
        <input type="text" name="tags" value={formData.tags} onChange={handleChange} required />
      </div>
      <button type="submit">Create Event</button>
    </form>
  );
};

export default EventCreate;
