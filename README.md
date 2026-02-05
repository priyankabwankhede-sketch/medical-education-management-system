# Medical Education Management System



A full-stack web application for managing \*\*medical trainees, rotations, supervisors, and schedules\*\*.



This system is designed to help medical education departments track trainee assignments, clinical rotations, and supervision in a structured and scalable way.



---



## Tech Stack



### Backend

- ASP.NET Core Web API

- Entity Framework Core

- SQL Server

- Swagger (OpenAPI)



### Frontend

- Angular

- Tailwind CSS

- PostCSS

- TypeScript



---



## Project Structure



---



## Core Features



### Trainees

- Create and manage trainee profiles

- Assign trainees to rotations

- View trainee rotation history



### Rotations

- Create clinical rotations

- Define start and end dates

- Link rotations to trainees



---



## Trainee → Rotation Relationship



- One trainee can have \*\*multiple rotations\*\*

- One rotation can have \*\*multiple trainees\*\*

- Each rotation has:

&nbsp; - Department

&nbsp; - Date range



---



## Tailwind CSS Configuration



### `src/style.css`

```css

@tailwind base;

@tailwind components;

@tailwind utilities;






