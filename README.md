
## Geting Start

Open solution file  **LuStoreWebService.sln**  with Visual Studio 2017.

```Build=>Build Solution ```

```Test=>Run=>All Test ```

```Debug=>Start Debugging ```

user: **admin**

password : **123**

##Security
LuStore is using the token base authentication because :

 1. Scalability of Servers: The token sent to the server is self contained which holds all the user information needed for authentication, so adding more servers to your web farm is an easy task, there is no dependent on shared session stores.
 2. Loosely Coupling: front-end application is not tight to specific authentication mechanism, the token is generated from the server and your API is built in a way to understand this token and do the authentication.
 3. Mobile Friendly: Cookies and browsers like each other, but storing cookies on native platforms (Android, iOS, Windows Phone) is not a trivial task, having standard way to authenticate users will simplify our life if we decided to consume the back-end API from native applications.
