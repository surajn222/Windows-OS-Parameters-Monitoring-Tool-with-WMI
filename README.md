# Windows-OS-Parameters-Monitoring-Tool <br />

#Introduction: <br />
This is an Agentless Monitoring Tool(no agent to be deployed on the Client Server) for monitoring Windows OS. <br />
The Tool communicates to the server with WMI on dynamic port.(Port allocated during communication initialization). <br />

#Network Connections: <br />
The ports that need to be opened are mentioned in the Architecture Diagram. Make sure to check the architecture diagram. <br />
Protocols: RPC over TCP, WMI over TCP. <br />

To configure WMI to use fixed port, follow the following steps on the Client Server( The server to be monitored) <br />
At the command prompt, type winmgmt -standalonehost <br />
Stop the WMI service by typing the command net stop “Windows Management Instrumentation” <br />
Restart the WMI service again in a new service host by typing net start “Windows Management Instrumentation” <br />
Establish a new port number for the WMI service by typing netsh firewall add portopening TCP 24158 WMIFixedPort <br />

#Parameters Monitored: <br />
Please refer the document for the monitored parameters. <br />
Please note that quite a few terms of the parameters are different across OS versions, Databases and softwares. Please do not assume all the terms in Task Manager, GPU-Z,CPU-z, SQL Databases to be the same as of WMI. <br />

#Tested on : <br />
Windows Server 2008 R2. <br />

![alt tag](https://github.com/surajn222/Windows-OS-Parameters-Monitoring-Tool-with-WMI/blob/master/Windows%20OS%20Monitoring%20Tool.png)

NEXT STEPS: Remove methods and make a common class for all parameters
