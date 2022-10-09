
##########################################################
sqlite db integration for azure function app .net core 3.1
##########################################################


##########################################################
Firstly I have created sqltite db using sqlitestudio-3.3.3
then imported csv formated data.
Included database to SQLiteDB directory

##########################################################
Here I have used clean architecture and azure function app
.net core 3.1 and Microsoft.EntityFrameworkCore.Sqlite to 
integrate.

#########################################################
Postman CURL for test 2 api endpoints
curl --location --request GET 'http://localhost:7071/api/population?state=2,3,5'
curl --location --request GET 'http://localhost:7071/api/household?state=as'
#########################################################



