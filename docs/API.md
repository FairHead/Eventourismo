# Eventourismo API Documentation

## Authentication

### POST /api/auth/login
Authenticate a user and receive a JWT token.

**Request:**
```json
{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "success": true,
  "data": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "message": "Login successful"
}
```

### POST /api/auth/register
Register a new user account.

**Request:**
```json
{
  "email": "user@example.com",
  "userName": "musiclover",
  "firstName": "John",
  "lastName": "Doe",
  "password": "password123"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": "guid",
    "email": "user@example.com",
    "userName": "musiclover",
    "firstName": "John",
    "lastName": "Doe"
  },
  "message": "Registration successful"
}
```

## Events

### GET /api/events
Get a list of events with optional location filtering.

**Query Parameters:**
- `latitude` (optional): Latitude coordinate
- `longitude` (optional): Longitude coordinate
- `radiusKm` (optional): Search radius in kilometers (default: 10)
- `upcomingOnly` (optional): Filter upcoming events only (default: true)

**Response:**
```json
{
  "success": true,
  "data": [
    {
      "id": "guid",
      "title": "Street Jazz Session",
      "description": "Join us for an evening of jazz in the park",
      "startTime": "2024-06-15T19:00:00Z",
      "endTime": "2024-06-15T22:00:00Z",
      "type": 0,
      "location": {
        "latitude": 40.7128,
        "longitude": -74.0060,
        "address": "Central Park",
        "city": "New York",
        "country": "USA"
      },
      "createdByUserId": "guid",
      "createdByUserName": "musiclover",
      "isPublic": true,
      "likesCount": 15,
      "commentsCount": 8,
      "favoritesCount": 12
    }
  ]
}
```

### POST /api/events
Create a new event (requires authentication).

**Request:**
```json
{
  "title": "Street Jazz Session",
  "description": "Join us for an evening of jazz in the park",
  "startTime": "2024-06-15T19:00:00Z",
  "endTime": "2024-06-15T22:00:00Z",
  "type": 0,
  "location": {
    "latitude": 40.7128,
    "longitude": -74.0060,
    "address": "Central Park",
    "city": "New York",
    "country": "USA"
  },
  "isPublic": true,
  "maxAttendees": 50,
  "ticketPrice": 0.00,
  "contactInfo": "contact@example.com"
}
```

### GET /api/events/{id}
Get details of a specific event.

**Response:**
```json
{
  "success": true,
  "data": {
    "id": "guid",
    "title": "Street Jazz Session",
    "description": "Join us for an evening of jazz in the park",
    "startTime": "2024-06-15T19:00:00Z",
    "endTime": "2024-06-15T22:00:00Z",
    "type": 0,
    "location": {
      "latitude": 40.7128,
      "longitude": -74.0060,
      "address": "Central Park",
      "city": "New York",
      "country": "USA"
    },
    "createdByUserId": "guid",
    "createdByUserName": "musiclover",
    "isPublic": true,
    "likesCount": 15,
    "commentsCount": 8,
    "favoritesCount": 12,
    "venue": null
  }
}
```

## Event Types

- `0` - StreetMusic
- `1` - Concert
- `2` - Festival
- `3` - Busking
- `4` - OpenMic
- `5` - Workshop
- `6` - Jam
- `7` - Other

## User Roles

- `0` - User
- `1` - Moderator
- `2` - Admin

## SignalR Hub

### Event Hub (`/hubs/event`)

**Methods:**
- `JoinEventGroup(eventId)` - Join an event-specific group
- `LeaveEventGroup(eventId)` - Leave an event-specific group
- `SendEventUpdate(eventId, message)` - Send update to event group

**Events:**
- `EventUpdate` - Receive real-time event updates

## Error Handling

All API responses follow this format:

**Success Response:**
```json
{
  "success": true,
  "data": { /* response data */ },
  "message": "Operation successful"
}
```

**Error Response:**
```json
{
  "success": false,
  "message": "Error description",
  "errors": ["Detailed error 1", "Detailed error 2"]
}
```

## Authentication

Include the JWT token in the Authorization header:
```
Authorization: Bearer <your-jwt-token>
```

## Rate Limiting

- Authentication endpoints: 5 requests per minute
- Event endpoints: 100 requests per minute
- Other endpoints: 60 requests per minute