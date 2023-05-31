# All Your Healthy Foods (AYHF) Online Shopping Solution

## Table of Contents

1. [Project Overview](#project-overview)
2. [Installation](#installation)
3. [Features](#features)
4. [Usage](#usage)
5. [Project Architecture](#project-architecture)
6. [Technologies Used](#technologies-used)
7. [Testing](#testing)
8. [Contributions](#contributions)

## Project Overview

AYHF is a Progressive Web App (PWA) designed to provide a nationwide online shopping platform for the All Your Healthy Foods (AYHF) Health Food store. This PWA brings the store to the fingertips of customers regardless of their location and enables the business to expand its reach and improve customer service and business analytics.

## Installation

### Prerequisites
* Node.js
* .NET SDK
* SQLite
* A modern web browser

### Instructions
1. Clone the repository
2. Navigate to the `backend` directory and run `dotnet restore` to install the backend dependencies
3. Run `dotnet run` to start the backend server
4. Navigate to the `frontend` directory and run `npm install` to install the frontend dependencies
5. Run `npm start` to launch the frontend
6. Open your web browser and navigate to `http://localhost:3000` (or whichever port your React app is running on)

## Features

- User authentication and account management
- Product browsing and searching
- Shopping cart and checkout functionalities
- Order history viewing and tracking
- User feedback submission
- Administrator controls for product and order management
- Offline capabilities

## Usage

After installation, users can create an account or log in if they already have one. They can browse and search for products, add products to their shopping cart, and proceed to checkout. Users can also view their order history, track their orders, and leave feedback. Administrators have additional controls for managing products and orders.

## Project Architecture

The project employs an Onion Architecture and utilizes the dotnet 7.0 Minimal API for the backend, a SQLite database for data storage, and the React framework for the frontend. The backend includes features such as JWT authentication and uses Data Transfer Objects (DTOs) to facilitate communication between the backend and frontend.

## Technologies Used

- Frontend: React
- Backend: .NET Minimal API
- Database: SQLite
- Service Worker for PWA capabilities
- Workbox for efficient caching strategies
- JWT for user authentication

## Testing

We employ a combination of unit tests, integration tests, and end-to-end tests to ensure our application functions as expected. Manual testing is also conducted to evaluate user experience and interface design.

## Contributions

This project was developed as a team assignment for a University course. The team members are:

- [Julian Codespoti](mailto:juliancodespoti@gmail.com)
- [Marella Mourad](mailto:marella99mourad@gmail.com)
- [Enzo Peperkamp](mailto:enzopkp@gmail.com)
- [Alex Kyriacou](mailto:a.kyriacou14@gmail.com)
