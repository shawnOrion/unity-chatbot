# Project Notes
### What does the project do?
This project has a chat interface where users can interact with an AI, GPT-4. It allows for conversations that carry on across multiple sessions, so users can pick up where they left off in previous chats.

### Why is this project important?
I took this project on to blend my full-stack knowledge with Unity, pushing myself to create with technologies that are new to me. It’s about building something complex and learning through the process.

### How did I build it?
Built with Express and Node.js for the backend, I leaned on these for their simplicity and my familiarity. The front end uses Unity and C#, which I had little experience with at first. This mix allowed me to use what I know to tackle server communication and data management in Unity.

### How is the project structured?
- **Front-End (Unity & C#):** 
  - Follows MVC pattern for user, chat, and message models. 
  - Views manage UI, Controllers link UI and data, and Services handle data exchanges with the backend.
- **Back-End (Express, Node.js, Mongoose):** 
  - Structured around Models (data blueprints), Routes (API endpoints), Controllers (request handling), and Services (data manipulation).

### What was challenging about this project?
Starting with MVC in Unity, I struggled with controllers doing more than they're supposed to, leading to a cluttered codebase. The turning point was working with C#'s encapsulation to reduce their responsibilities. Now, services manage data, while controllers oversee the flow. This change clarified the code, helping me reach my goal of creating an intuitive codebase for an interactive AI application.

### What have I learned?
Stepping into C# and Unity was a leap out of my comfort zone. But this change helped me ditch old habits for a cleaner, more organized codebase. I was challenged to pick up new patterns, like MVC, and rethink my approach to app development. So, I learned that embracing new tech can make me a much better coder, and remind me how much I love building apps.
