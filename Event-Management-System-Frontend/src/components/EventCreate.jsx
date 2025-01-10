import React, { useState } from 'react';
import axios from 'axios';
import './EventCreate.css';

const EventCreate = () => {
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    date: '',
    location: '',
    createdBy: '',
    capacity: '',
    tags: '',
  });

  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
    setErrors({
      ...errors,
      [e.target.name]: '',
    });
  };

  const validateForm = () => {
    const newErrors = {};
    if (!formData.name) newErrors.name = "Name is required.";
    if (!formData.description) newErrors.description = "Description is required.";
    if (!formData.date) newErrors.date = "Date is required.";
    if (!formData.location) newErrors.location = "Location is required.";
    if (!formData.createdBy) newErrors.createdBy = "Created By is required.";
    if (!formData.capacity) newErrors.capacity = "Capacity is required.";
    if (!formData.tags) newErrors.tags = "Tags are required.";
    return newErrors;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    const formErrors = validateForm();
    if (Object.keys(formErrors).length > 0) {
      setErrors(formErrors);
      return;
    }
    
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
      setErrors({});
    } catch (error) {
      console.error('Error creating event:', error);
      if (error.response && error.response.data && error.response.data.errors) {
        setErrors(error.response.data.errors);
      }
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Name</label>
        <input type="text" name="name" value={formData.name} onChange={handleChange} />
        {errors.name && <p className="error">{errors.name}</p>}
      </div>
      <div>
        <label>Description</label>
        <textarea name="description" value={formData.description} onChange={handleChange}></textarea>
        {errors.description && <p className="error">{errors.description}</p>}
      </div>
      <div>
        <label>Date</label>
        <input type="date" name="date" value={formData.date} onChange={handleChange} />
        {errors.date && <p className="error">{errors.date}</p>}
      </div>
      <div>
        <label>Location</label>
        <input type="text" name="location" value={formData.location} onChange={handleChange} />
        {errors.location && <p className="error">{errors.location}</p>}
      </div>
      <div>
        <label>Created By</label>
        <input type="text" name="createdBy" value={formData.createdBy} onChange={handleChange} />
        {errors.createdBy && <p className="error">{errors.createdBy}</p>}
      </div>
      <div>
        <label>Capacity</label>
        <input type="text" name="capacity" value={formData.capacity} onChange={handleChange} />
        {errors.capacity && <p className="error">{errors.capacity}</p>}
      </div>
      <div>
        <label>Tags</label>
        <input type="text" name="tags" value={formData.tags} onChange={handleChange} />
        {errors.tags && <p className="error">{errors.tags}</p>}
      </div>
      <button type="submit">Create Event</button>
    </form>
  );
};

export default EventCreate;
