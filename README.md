# phoneBookServices
Phonebook services consists of two micro services. One service creates phone book entities, the other creates reports. A queue mechanism is used for asynchronous processing of reporting requests.   
&nbsp;   
### install docker images     
//PostgreSql, Kafka, Zookeeper, PhoneBookApi, PhoneBookReport      
&nbsp;   
//create phonebook-report-api image   
docker build --rm --pull -f "src/PhoneBook.Report.Api/Dockerfile" --label "com.microsoft.created-by=visual-studio-code" -t "phonebook-report-api:latest" "."   

//create phonebook-api image   
docker build --rm --pull -f "src/PhoneBook.Api/Dockerfile" --label "com.microsoft.created-by=visual-studio-code" -t "phonebook-api:latest" "."   
&nbsp;   
cd src   
docker-compose up    

&nbsp;   
### create database user for api   
docker exec -it postgres_phonebook_api bash   
su postgres   
&nbsp;   
createuser -P -d -E -e phonebookuser   
//password: phoneBookPass   
&nbsp;   

psql -h localhost -U postgres -c "CREATE DATABASE phonebook;"   
psql -h localhost -U postgres -c "GRANT ALL PRIVILEGES ON DATABASE phonebook to phonebookuser;"   
exit   
exit   
&nbsp;   

### update api database  
//update server info in connection string in ".\src\PhoneBook.Api\appsettings.json"' as localhost
cd .\PhoneBook.Api.DataContext\   
dotnet ef database update --startup-project ..\PhoneBook.Api\  
cd ..   
&nbsp;   
### create database user for report 
docker exec -it postgres_phonebook_report bash   
su postgres    
&nbsp;     
createuser -P -d -E -e phonebookreportuser   
//password: phoneBookReportPass  
&nbsp;   

psql -h localhost -U postgres -c "CREATE DATABASE phonebookreport;"   
psql -h localhost -U postgres -c "GRANT ALL PRIVILEGES ON DATABASE phonebookreport to phonebookreportuser;"   
exit   
exit   
&nbsp; 

### update report database  
//update server info in connection string in ".\src\PhoneBook.Report.Api\appsettings.json"' as localhost 
cd .\PhoneBook.Report.Api.DataContext\   
dotnet ef database update --startup-project ..\PhoneBook.Report.Api\  
&nbsp;  
&nbsp;  
//PhoneBookApi   
http://localhost:5000   
&nbsp;  
//PhoneBookReport   
http://localhost:5003     

