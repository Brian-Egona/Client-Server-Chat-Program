`CLIENT-SERVER-CHAT-PROGRAM
`

📋 Project Overview
The Client-Server Chat Program is a Windows Forms application designed to facilitate real-time communication between a server and a single client over a local network. This project simulates the behavior of a basic one-on-one chatroom, enabling text-based communication between the client and the server. Built in C# with .NET, the program uses TCP connections to establish a simple and efficient communication channel.

⚙️ Key Features
Real-Time Messaging – Supports bidirectional text messaging between the server and a single client.
Rich TextBox Interface – Messages are displayed in a RichTextBox for better formatting and readability.
Placeholder Text – Input fields include placeholder text that disappears when the user types, enhancing the user experience.
Server Control – The server can disconnect the client and shut down the communication channel.
Client Disconnection – Clients can disconnect from the server, with status updates reflected on the server side.
Dynamic Connection Status – The server tracks the connection status of the client and displays it in the control panel (Form2).

🛠️ Planned Enhancements
Multi-Client Support – Extend the server to handle multiple client connections simultaneously.
Direct and Broadcast Messaging – Enable the server to broadcast messages to all connected clients or message individual clients.
File Sending – Implement file/image transfer between the server and clients.
Client List Display – Dynamically update and display the list of connected clients in the server chat interface.

🖥️ Application Structure
The program consists of three primary forms for both the client and server:
Form1 (Startup Form)
Provides a simple interface to start the client or server.
Form2 (Main Control Panel)
Displays the client’s connection status on the server side.
On the client side, shows the status of the connection and provides access to the chatroom.
Form3 (Chat Interface)
The main chat interface for messaging between the server and client.
RichTextBox is used to display chat history, and a ComboBox on the server side (currently limited to "All") is in place for future multi-client support.

🔧 Technologies Used
Language: C#
Framework: .NET (Windows Forms)
Networking: TCP/IP (System.Net.Sockets)
IDE: Visual Studio

🚀 How to Run
Prerequisites
Visual Studio – Download and install Visual Studio.
.NET Desktop Development – Ensure the ".NET Desktop Development" workload is installed in Visual Studio.
1. Clone or Download the Repository
bash
Copy code
git clone <repository-url>
Alternatively, download the ZIP file and extract it.

2. Open the Solution in Visual Studio
Open Visual Studio.
Click on File > Open > Project/Solution.
Navigate to the project directory and open the .sln file.
3. Build the Project
In Visual Studio, click on Build > Build Solution or press Ctrl + Shift + B.
Ensure there are no build errors.
4. Run the Server
In Visual Studio, set the Server project as the startup project.
Click Start (F5) to run the server.
The server will open on Form1 with an option to start and transition to the control panel (Form2).
5. Run the Client
Open a new Visual Studio instance.
Set the Client project as the startup project.
Click Start (F5) to run the client.
Connect to the server by clicking the "Connect" button.
The chat interface (Form3) will open after connection.
6. Messaging
Type messages in the chatbox on the client side.
Messages will be displayed on both the server and client.
The server can send messages or stop the connection by interacting with Form3.
7. Disconnecting
The client can disconnect from the server at any time by clicking "Disconnect."
The server will update the client’s status to "Disconnected."
