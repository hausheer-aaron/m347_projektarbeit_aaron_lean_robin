// init.js
db = db.getSiblingDB("mydatabase");

// --- Weekdays Collection ---
db.createCollection("weekdays");

const weekdayDocs = [
  { day: "Monday" },
  { day: "Tuesday" },
  { day: "Wednesday" },
  { day: "Thursday" },
  { day: "Friday" },
  { day: "Saturday" },
  { day: "Sunday" }
];

const resultWeekdays = db.weekdays.insertMany(weekdayDocs);
const weekdayIds = resultWeekdays.insertedIds;

// --- Places Collection ---
db.createCollection("places");

const placeDocs = [
  { name: "Central Park", lat: 40.785091, long: -73.968285 },
  { name: "Eiffel Tower", lat: 48.8584, long: 2.2945 }
];

const resultPlaces = db.places.insertMany(placeDocs);
const placeIds = resultPlaces.insertedIds;

// --- Absences Collection ---
db.createCollection("absences");

// Absences mit Timeranges + Ort
const absences = [
  {
    day_id: weekdayIds["0"], // Monday
    ranges: [
      { start: new Date("1970-01-01T11:00:00Z"), end: new Date("1970-01-01T13:00:00Z"), place_id: placeIds["0"], recurring: true },
      { start: new Date("1970-01-01T15:00:00Z"), end: new Date("1970-01-01T17:00:00Z"), place_id: placeIds["1"], recurring: false }
    ]
  },
  {
    day_id: weekdayIds["1"], // Tuesday
    ranges: [
      { start: new Date("1970-01-01T09:00:00Z"), end: new Date("1970-01-01T12:00:00Z"), place_id: placeIds["0"], recurring: true }
    ]
  }
  // Weitere Tage nach Bedarf…
];

db.absences.insertMany(absences);

print("✅ Weekdays, Places & Absences ready, Timeranges mit Place referenziert");