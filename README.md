# Windows-OS-Parameters-Monitoring-Tool

#Introduction:
This is an Agentless Monitoring Tool(no agent to be deployed on the Client Server) for monitoring Windows OS.
The Tool communicates to the server with WMI on dynamic port.(Port allocated during communication initialization).

#Network Connections:
The ports that need to be opened are mentioned in the Architecture Diagram. Make sure to check the architecture diagram.
Protocols: RPC over TCP, WMI over TCP.

To configure WMI to use fixed port, follow the following steps on the Client Server( The server to be monitored)
At the command prompt, type winmgmt -standalonehost
Stop the WMI service by typing the command net stop “Windows Management Instrumentation”
Restart the WMI service again in a new service host by typing net start “Windows Management Instrumentation”
Establish a new port number for the WMI service by typing netsh firewall add portopening TCP 24158 WMIFixedPort

#Parameters Monitored:
Please refer the document for the monitored parameters.
Please note that quite a few terms of the parameters are different across OS versions, Databases and softwares. Please do not assume all the terms in Task Manager, GPU-Z,CPU-z, SQL Databases to be the same as of WMI.

#Tested on :
Windows Server 2008 R2.
