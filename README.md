<h1>COMP2084 LOLProject</h1>
<p>Hung Phung - 200452314</p>

	host: leagueoflegends.database.windows.net
	db: LOLProject
	username: lolproject
	pw: Admin123


	- Link website project ASP.NET: https://leagueoflegendsproject.azurewebsites.net

	- ClientId and ClientSecret for both Google and Facebook: 
		+ Google: 	"ClientId": "331118585088-vreh5ehap1j11b485pdtbapldg2ks457.apps.googleusercontent.com"
					"ClientSecret": "WBNSfAf7fIpxhFdAcexoeNHe"

		+ Facebook: "ClientId": "153575220178095",
      				"ClientSecret": "9cc2a605340ead5b9295275ba1e7da8e"

	- In case, if Error 500.30 -> Configure in Azure -> Configuration -> Add/Edit application setting -> name: EncryptionEnabled, value: true.

	
<p> 06-21-2021: I'm not done two pages "Guides" and "Posts". In Posts page, I need to get the AccountId 
from the Account table every time a user logs in. Then I can add, delete and edit posts. In Guides page, I will
change the content of page(show more information, and use hyperlink to other pages on website. In addition, I will try to
use Search function (search by Role, or Position) in Champions/Index.cshtml.
</p>

<p> 07-17-2021: Added 2 social authentication Facebook and Google, and search function in Champions page.
As admin, they can add, edit, or delete data PRIVATE. The add, delete, edit functions will be hidden if the 
user is "user" and "anonymous". In case, if the users who are not admin but intentionally accesses pages(Create, Delete, Edit) 
through the browser, it will return the login page if user is "anonymous", return the error "Access denied" if the user is "User".
</p>

<p> 07-19-2021: - Completed the Guides page. 
				- Added rich@gc.ca / Test123$ account as Admin role.
</p>
<p> 07-20-2021: - Completed the Forum page, but I still haven't refresh session every time I logout.
</p>

<p> 08-20-2021: - Added Unit Testing for Create (GET) and Edit (GET), and create 3 mock records by using the in-memory database.
</p>
