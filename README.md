<h5>Set Multiple Startup Projects</h5>

<ol  type="1">
<li>Category.Api</li>
<li>Product.Api</li>
<li>TandGSample.ApiGateway</li>
<li>TandGSample.Web</li>
</ol>
<br/>

<h5>Dependencies</h5>

Docker Mongo Install...
<br/><code>docker run -d --name mongossrv -p 27017:27017 -v etc/mongod.conf -e MONGO_INITDB_ROOT_USERNAME=ping -e MONGO_INITDB_ROOT_PASSWORD=pong mongo</code>
<br/>
<br/>Mongo DB Connection
<br/> App :  <a href="https://downloads.mongodb.com/compass/mongodb-compass-1.33.1-win32-x64.exe"> Mongo Compas Download</a>
<br/> Uri : <code>mongodb://ping:pong@localhost:27017/TandGSampleCategory?authSource=admin</code>
  
<br/>Docker MSSQL Install...
<br/><code>docker run --cap-add SYS_PTRACE -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=19Mayis1919!" -p 1433:1433 --name mssqlsrv -d mcr.microsoft.com/azure-sql-edge</code>




