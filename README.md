# 🚗 Driving License App (DVLD)

> A desktop application for managing local and international driving licenses, built with C# and SQL Server using a clean 3-layer architecture.

---

## Table of Contents

- [About](#about)
- [Features](#features)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

---

## About

The **Driving Vehicle License Department (DVLD)** application is a Windows desktop system designed to manage the full lifecycle of driving licenses — from new applications to renewals, replacements, and international license issuance. It simulates a real-world government licensing department workflow.

---

## Features

- 🪪 **Local License Management** — Issue, renew, and revoke local driving licenses
- 🌍 **International License Management** — Process and manage international driving permits
- 👤 **Person/Applicant Management** — Register and maintain applicant records
- 📝 **Application Processing** — Handle license applications with full status tracking
- 🧪 **Tests & Exams Management** — Schedule and record vision, written, and driving tests
- 📋 **Detain & Release** — Manage license detention and release workflows

---

## Architecture

The project follows a clean **3-Layer Architecture** for separation of concerns:

```
┌──────────────────────────────┐
│         DVLD (UI Layer)      │  ← Windows Forms Presentation
├──────────────────────────────┤
│    DVLD_Business (BLL)       │  ← Business Logic Layer
├──────────────────────────────┤
│   DVLD_DataAccess (DAL)      │  ← Data Access Layer (SQL Server)
└──────────────────────────────┘
```

| Layer | Project | Responsibility |
|---|---|---|
| Presentation | `DVLD` | Windows Forms UI, user interaction |
| Business Logic | `DVLD_Buisness` | Validation, rules, and workflows |
| Data Access | `DVLD_DataAccess` | SQL Server queries and data operations |

---

## Tech Stack

- **Language:** C# (.NET Framework)
- **UI Framework:** Windows Forms (WinForms)
- **Database:** Microsoft SQL Server
- **IDE:** Visual Studio
- **Architecture:** 3-Layer (Presentation / Business / Data Access)

---

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework (4.x recommended)
- Microsoft SQL Server (LocalDB or full instance)
- SQL Server Management Studio (SSMS) — optional but recommended

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/abdelrahman1a/Driving-License-App.git
   cd Driving-License-App
   ```

2. **Set up the database**
   - Open SQL Server Management Studio
   - Run the provided SQL script to create and seed the database (if included)
   - Update the connection string in `DVLD_DataAccess` to match your SQL Server instance

3. **Open the solution**
   - Launch Visual Studio
   - Open `DVLD.sln`
   - Restore NuGet packages if prompted

4. **Build & Run**
   - Set `DVLD` as the startup project
   - Press `F5` or click **Start** to run the application

---

## Project Structure

```
Driving-License-App/
│
├── DVLD/                    # UI Layer — Windows Forms
│   ├── Forms/               # All application forms/screens
│   └── Program.cs           # Application entry point
│
├── DVLD_Buisness/           # Business Logic Layer
│   ├── clsPerson.cs
│   ├── clsLicense.cs
│   ├── clsApplication.cs
│   └── ...                  # Other business entities
│
└── DVLD_DataAccess/         # Data Access Layer
    ├── clsPersonData.cs
    ├── clsLicenseData.cs
    └── ...                  # SQL Server data handlers
```

---

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

## License

This project is open source and available under the [MIT License](LICENSE).

---

> Built by [Abdelrahman Ahmed](https://github.com/abdelrahman1a) 🛠️
