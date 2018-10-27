### Project Specifications

- Feature name : *eSport Statistics*
- Team name : *tazdingo mon*

- Team members:
Aleksander Peychev(leader) , Aleksander Stoychev , Georgi Zlatareff

- This project is all about providing statistics for **eSport** games such as "League of Legends" , "Dota2" and "Overwatch".
It contains **core information** about the game itself as well as data for **professional competition** such as:
  - Playable characters , Rules , Maps , Items , etc ...
  - Currenly active pro players
  - Currenly played team matches
  - Active Leagues
  - Active Tournaments
  - Active Series

- **Late game content**:
  - Regular repopulation of the database via public API (PandaScore). This includes loading data from external source(JSON serialization).
  - **History** of leagues , series , matches and tournaments
  - Create PDF reports of various statistics

- The system contains two types of users.
  - **Standart user** : Is limited to only **viewing** the content.
  - **Administrator user** : Has access to all statistics including modifing the data.The admin also has access to **most** of the standart user account information 
  including the ability to disable , enable or modify user credentials(including **user type**).
  
- Targets:
  - To be completed withing 80 work hours (at least 25 hours of testing)
  - Tech : 
    - .NET Core 
    - Microsoft SQL Server
    - Entity Framework
    - MSTest 
    - MOQ
  - External Tools : 
    - Trello  
    - SSMS
    - GitLab
  - Techniques : 
    - DIContainer
    - Command pattern
    - Code First Approach
    - SOLID
    - CRUD 
    - **Optionally** use Repository pattern or Service layer
  - User Interface(Console)