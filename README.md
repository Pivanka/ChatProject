<h1>Chat Online</h1>

🎯This is the example of my skills in <i>ASP.NET Core 6</i>. In this project, I used 3-layer architecture. 

📌 <b>DAL</b> is layer with fundamental repository operations for interacting with database entity-tables..<br>
     Also contains a Unit of Work pattern that collect all the repositories into one unit to make easier access for the repositories and be confident that we use one context<br>
	To work with database I used <i>Entity Framework Core</i>.<br>
	Main methods are covered by <i>NUnit tests</i> with <i>Moq</i>.

📌 <b>BLL</b> contains all the main logic to work with chat like:<br> 
    creating new account, adding friends and groups, searching by groups, sending/editing/replying/deleting messages.<br>
	To transfer data I was using <i>AutoMapper</i> to Map base objects created in folder "DTOs"

📌 <b>PL</b> is Web Api that works with BLL methods.
    <br>
    For authentication I was using <i>ASP.NET Core Identity</i> with <i>JWT</i>
    <br>
		