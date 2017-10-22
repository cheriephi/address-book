@Rem manual tests
@cd ..\AddressBook\bin\Debug
@pause

AddressBook update "John Doe" street "3645 Marine St."
@pause
AddressBook update "John Doe" name "Bob Ball"
@pause
AddressBook remove "Joe Bloggs"
@pause
AddressBook print foo.csv
@pause
AddressBook print bar.xml
@pause
AddressBook print
@pause
AddressBook save Addresses.txt
@rem dir *.txt
@rem type Addresses.txt | more
@rem cls
@pause
AddressBook load Addresses.txt
@pause
AddressBook print
@dpause

cd ..\..\..\AddressBookTest