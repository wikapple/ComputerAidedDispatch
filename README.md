# 📱 Mobile Computer Aided Dispatch (Mobile CAD)

A prototype mobile Computer Aided Dispatch (CAD) system designed for first responders. Built with Flutter and Firebase for real-time field visibility of calls and unit statuses. Supports realistic simulation and testing via a custom C# dispatcher bot and ASP.NET Web API.

---

## 🚨 Purpose

Traditional CAD systems are often locked to desktop or vehicle-based laptops. This project brings CAD capabilities to mobile devices, ensuring first responders and supervisors have situational awareness wherever they go — with real-time data, GPS precision, and a clean user interface.

---

## 🧰 Tech Stack

| Layer             | Tech Used                                 |
|------------------|--------------------------------------------|
| Mobile App       | Flutter, Dart, Firebase (Cloud Firestore, Auth) |
| Backend API      | ASP.NET Core Web API, C#                   |
| Data Simulator   | C# Console App (Dispatcher Bot)            |
| Infrastructure   | Docker, Docker Compose                     |

---

## 📲 Features

- **Real-time call tracking** via Firebase Firestore streams
- **Unit and Call dashboards** with quick navigation
- **Secure login** with Firebase Authentication
- **Dispatcher simulator** generates live data to test app behavior
- **Stateless widgets + performance optimizations** for seamless UX
- **Custom dark theme** with accessibility-aware UI

---

## 📁 Project Structure

### 🔹 Flutter App (`mobile_cad`)
- `screens/` – App pages: Home, Call Details, Unit Status, etc.
- `services/` – Firebase stream readers
- `models/` – Firestore object mappers
- `shared/` – Reusable UI components and constants

### 🔹 Dispatcher Bot (`CadDispatcherBotConsole`)
- Simulates dispatcher events (calls, comments, unit movement)
- Outputs realistic Firestore call data using HTTP requests

### 🔹 API Server (`CadApi`)
- Translates dispatcher data to Firestore writes
- Handles collection/subcollection operations via repository pattern

---

## 🔄 Data Model Overview

- `CallForService` – Events requiring response
  - `CallComment` – Timeline/comments for the call
  - `UnitSummary` – Units assigned to this call
- `Unit` – Responders in the system
  - `CallSummary` – Calls assigned to each unit

Optimized for fast reads by mirroring key references across subcollections.

---

## 🧪 Testing

- Simulated dispatch activity using custom C# console bot
- Real-time updates via Firestore streams
- Stress-tested scroll performance and UI state management
- Dockerized for portable testing with API + simulator

---

## 🎯 Key Challenges Solved

- Implemented real-time updates without jarring scroll resets
- Managed Firestore read costs with optimized structure
- Integrated clean animations and deep-link navigation
- Worked around StreamProvider null init edge cases
- Converted complex UI overlays to flat color equivalents for performance

---

## 🔐 Authentication Strategy

Used Firebase Authentication. Each authenticated user matches a Firestore Unit document (IDs are synchronized) to provide a tailored dashboard view.

---

## 🧠 Skills Gained

- Flutter + Firebase real-time data architecture
- NoSQL (Firestore) schema and write strategy
- Advanced Dart state management using Provider
- Repository pattern with NoSQL in .NET
- Docker Compose container networking

---

## 📸 UI Previews

_(Consider including images here if adding to GitHub)_

---

## 🧾 References

See full report for detailed citations. Major sources include:

- [FlutterFire Docs](https://firebase.flutter.dev/)
- [Cloud Firestore Docs](https://firebase.google.com/docs/firestore)
- [ASP.NET Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/)
- [Docker Compose Docs](https://docs.docker.com/compose/)
- Dart & Flutter community tutorials

---

## 📦 Run Locally

```bash
# Flutter app
flutter pub get
flutter run

# Start Docker containers
docker compose up --build
