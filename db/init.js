db = db.getSiblingDB("mydatabase");

db.createCollection("weekdays");

const weekdays = [
  { day: "Monday", morningabsence: null, eveningabsence: null },
  { day: "Tuesday", morningabsence: null, eveningabsence: null },
  { day: "Wednesday", morningabsence: null, eveningabsence: null },
  { day: "Thursday", morningabsence: null, eveningabsence: null },
  { day: "Friday", morningabsence: null, eveningabsence: null },
  { day: "Saturday", morningabsence: null, eveningabsence: null },
  { day: "Sunday", morningabsence: null, eveningabsence: null }
];

db.weekdays.insertMany(weekdays);

db.createCollection("places");

const places = [
  { name: "Central Park", lat: 40.785091, long: -73.968285 },
  { name: "Eiffel Tower", lat: 48.8584, long: 2.2945 }
];

db.places.insertMany(places);