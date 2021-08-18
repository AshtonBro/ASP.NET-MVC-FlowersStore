# Flowers Store <img src="https://raw.githubusercontent.com/AshtonBro/ASP.NET-MVC-FlowersStore/44007b3d12a97d91126dfc74e29a15106754f296/FlowersStore.WebUI/wwwroot/Image/accessdeniedfl.svg" alt="logo" width="30" height="30">
     
Flowers Store is an online store project, written for practice and development of key skills in developing .Net applications. Suitable for Full Stack developers. 
The project uses the MVC pattern to separate the user interface, data and business logic of the application, and also has a layered architecture. 
The user interface pages are written in cshtml, the page is animated using JS, and an additional request handler is implemented in C # in conjunction with AJAX.
The DataAccess layer uses Ms Sql, working with models is implemented using Entity Framework Core, but the database can be easily changed if necessary.
Used by AutoMapper to work correctly with models. ASP.NET Core Identity is configured for authorization in the system. The project is being developed and supplemented
with new features.

## 
<p align="center"><b>Home Page</b></p>
<p align="center">
  <img src="https://nimbusweb.me/box/attachment/5930871/5v5k8ycu2e32ytuognls/UTAbT42Kzl0MYien/screenshot-localhost_5001-2021.08.18-01_30_32.png" alt="Home" width="750">
</p>

## 

<p align="center"><b>SignIn / SignUp</b></p>
<p align="center">
  <img src="https://nimbusweb.me/box/attachment/5930910/r3lq91o0ckyimsboxr7o/b9J8zAK7AG8r4n9Z/screenshot-localhost_5001-2021.08.18-01_50_41.png" alt="SignIn / SignUp" width="750">
  
  <img src="https://nimbusweb.me/box/attachment/5930917/21l55rvo0dzkqtdel4hm/4etxq8HQXvebx9Ze/screenshot-localhost_5001-2021.08.18-01_55_23.png" alt="SignIn / SignUp" width="750">
</p>

## 

<p align="center"><b>Store</b></p>
<p align="center">
  <img src="https://nimbusweb.me/box/attachment/5931776/983i0qw5mkq1x1tbn3ml/elIxTC9fnCQAkZmC/screenshot-localhost_5001-2021.08.18-10_21_24.png" alt="Store" width="750">
</p>

## 

<p align="center"><b>Selected products</b></p>
<p align="center">
  <img src="https://nimbusweb.me/box/attachment/5931780/3rvgx2apkmf8ymi9cy71/bwbC2QxYNTGw5fQm/screenshot-localhost_5001-2021.08.18-10_24_57.png" alt="Store" width="750">
</p>

## 

<p align="center"><b>Order Form</b></p>
<p align="center">
  <img src="https://nimbusweb.me/box/attachment/5931788/te4qd30xxesx8dn8etdi/baH5G00kMqebJw8T/screenshot-localhost_5001-2021.08.18-10_28_27.png" alt="Store" width="750">
</p>

## 

<p align="center"><b>Profile</b></p>
<p align="center">
  <img src="https://nimbusweb.me/box/attachment/5931802/ii4gwa3xefydrzeun5qm/wuinQBrneebqYdhS/screenshot-localhost_5001-2021.08.18-10_31_54.png" alt="Store" width="750">
</p>
<p align="center">On the profile page, you can edit your details</p>

## 

<p align="center"><b>Admin Panel</b></p>
<p align="center">
  <img src="https://nimbusweb.me/box/attachment/5931823/vdu7lfsedun50ah949e8/pwFBgNs0cQCODl8z/screenshot-localhost_5001-2021.08.18-10_39_50.png" alt="Store" width="750">
</p>
<p align="center">To get to the admin panel, you must fill in the administrator data in the root file of the system and the admin user will be created automatically when you first start the program(More details below).</p>

## How to Run?
1. Download this repository
2. Add your database connection string to `appsettings.Development.json`
3. Installing EF Core <br>
   Open terminal and enter commands: <br>
   Create tool-manifest `dotnet new tool-manifest` <br>
   Install the tool `dotnet tool install dotnet-ef` <br>
   Check that the context is defined `dotnet ef dbcontext info -p FlowersStore.DataAccess.MSSQL -s FlowersStore.WebUI` <br>
4. Create DB <br>
   Add migrations `dotnet ef migrations add InitDb -p FlowersStore.DataAccess.MSSQL  -s FlowersStore.WebUI` <br>
   Initialize the database `dotnet ef database update -p FlowersStore.DataAccess.MSSQL  -s FlowersStore.WebUI` <br>
5. Before starting the program, you need to add settings for the Admin as it is created automatically. <br>
   To do this, right-click on the project and select Manage User Secrets. <br>
  
   <img src="http://dl3.joxi.net/drive/2021/08/18/0023/3726/1527438/38/2bccb95eab.jpg" width="450">
 
    Fill the secrets.json file.
    ```
    {
      "AdminConfig": {
        "Name": "***",
        "Email": "***",
        "Password": "***"
      }
    } 
    ```
 6. Then press F5, at the first start of the project, the database will be filled and an admin user will be created.
   
   
   
   
   
