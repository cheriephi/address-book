@Rem manual tests
cd ..\AddressBook\bin\Debug
pause

AddressBook update "John Doe" street "3645 Marine St."
pause
AddressBook update "John Doe" name "Bob Ball"
pause
AddressBook print
pause

cd ..\..\..\AddressBookTest