@Rem manual tests
@cd ..\AddressBook\bin\Debug
@del Addresses.txt && del foo.csv && del bar.xml
@pause


AddressBook add Addresses.txt "Michelle Obama" "1600 Pennsylvania Ave, Washington, DC, 20500, USA"
@pause
AddressBook print Addresses.txt
@pause
AddressBook update Addresses.txt "John Doe" street "3645 Marine St."
@pause
AddressBook print Addresses.txt
@pause
AddressBook update Addresses.txt "John Doe" name "Bob Ball"
@pause
AddressBook print Addresses.txt
@pause
AddressBook remove Addresses.txt "Joe Bloggs"
@pause
AddressBook print Addresses.txt foo.csv
@pause
AddressBook print Addresses.txt bar.xml

@rem dir *.txt
@rem type Addresses.txt | more
@rem cls

cd ..\..\..\AddressBookTest