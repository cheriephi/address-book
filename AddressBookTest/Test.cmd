@Rem manual tests
cd ..\AddressBook\bin\Debug
pause

AddressBook update "John Doe" street "3645 Marine St."
pause
AddressBook update "John Doe" name "Bob Ball"
pause
AddressBook print
pause
AddressBook print foo.csv
pause
AddressBook print bar.xml
pause

cd ..\..\..\AddressBookTest