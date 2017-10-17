@Rem manual tests
cd ..\AddressBook\bin\Debug
pause

AddressBook find name "Blog"
pause
AddressBook find street "31"
pause
AddressBook update "John Doe" street "3645 Marine St."
pause
AddressBook update "John Doe" name "Bob Ball"
pause
AddressBook sort name
pause
AddressBook sort state
pause

cd ..\..\..\AddressBookTest