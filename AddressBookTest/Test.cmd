@Rem manual tests
@cd ..\AddressBook\bin\Debug
@del Addresses.txt && del foo.csv && del bar.xml
@pause


AddressBook add Addresses.txt "Joe Bloggs" "1 New St., Birmingham, England, B01 3TN, UK"
AddressBook add Addresses.txt "John Doe" "16 S 31st St., Boulder, CO, 80304, USA"
AddressBook add Addresses.txt "Brent Leroy" "Corner Gas, Dog River, SK, S0G 4H0, CANADA"
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