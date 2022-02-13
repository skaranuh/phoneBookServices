# phoneBookServices
Phonebook services consists of two micro services. One service creates phone book entities, the other creates reports. A queue mechanism is used for asynchronous processing of reporting requests.   
&nbsp;   
### install docker images     
//PostgreSql, Kafka, Zookeeper   
cd src   
docker-compose up  
&nbsp;   
### create database user for api   
winpty docker exec -it postgres-phonebook-api bash   
su postgres   
createuser -P -d -E -e phonebookuser   
//password: phoneBookPass   
psql -h localhost -U postgres -c "CREATE DATABASE phonebook;"   
psql -h localhost -U postgres -c "GRANT ALL PRIVILEGES ON DATABASE phonebook to phonebookuser;"   
exit   
exit   
&nbsp;   
### update api database   
cd .\PhoneBook.Api.DataContext\   
dotnet ef database update --startup-project ..\PhoneBook.Api\  
&nbsp;   
### create database user for report 
winpty docker exec -it postgres-phonebook-report bash 
su postgres   
createuser -P -d -E -e phonebookreportuser   
//password: phoneBookReportPass  
psql -h localhost -U postgres -c "CREATE DATABASE phonebookreport;"   
psql -h localhost -U postgres -c "GRANT ALL PRIVILEGES ON DATABASE phonebookreport to phonebookreportuser;"   
exit   
exit   
&nbsp; 
### update report database   
cd .\PhoneBook.Report.Api.DataContext\   
dotnet ef database update --startup-project ..\PhoneBook.Report.Api\  