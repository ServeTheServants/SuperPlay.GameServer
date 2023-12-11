# SuperPlay.GameServer
SuperPlay Home Assignment


# Net6.0 Game Server Solution

## Requirements

Create a Net6.0 Solution with the relevant projects for the below requirements.

### Game Server

- Create a basic Game Server that accepts WebSockets (use Dotnet’s System.Net only, do not use signalR or mediatR or any other lib).
- Implement handlers for the following socket messages (all messages from the client should be sent through the socket only):
  - **Login**
    - Accept DeviceId(UDID) and respond with PlayerId.
    - Make sure the player is not connected already. If so, respond accordingly.
  - **UpdateResources**
    - Accept ResourceType(coins, rolls), ResourceValue, and respond with the new balance.
  - **SendGift**
    - Accept FriendPlayerId, ResourceType, and ResourceValue.
    - Update the sending player’s player state and the friend player state.
    - If the friend is online, then send a GiftEvent with the relevant information to him.

- Make sure that the socket messages routing can be easily extendable.
- Player state can be saved in RAM or SQLite.

### Client Console Application

- Create a client console application to test the GameServer APIs.

### Logging

- Use the Serilog library to effectively log both Server and Client.

### General Guidelines

- Ensure a clean and highly professional attitude when preparing this solution.
