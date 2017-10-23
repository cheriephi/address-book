@Rem manual system tests
@SET AddressBook=..\AddressBook\bin\Debug\AddressBook.exe

CALL :Add Addresses.txt
CALL :Update Addresses.txt
CALL :Remove Addresses.txt
CALL :PrintFile Addresses.txt foo.csv
CALL :PrintFile Addresses.txt bar.xml

GOTO :EOF




@REM ==============================================================================
:Add
@REM ==============================================================================
@SET AddressFile=%1

@Rem Check it handles when it needs to create the address file
@DEL %AddressFile%
%AddressBook% add %AddressFile% "Joe Bloggs" "1 New St., Birmingham, England, B01 3TN, UK"
@Rem Check when it needs to append to the address file
%AddressBook% add %AddressFile% "John Doe" "16 S 31st St., Boulder, CO, 80304, USA"
%AddressBook% add %AddressFile% "Brent Leroy" "Corner Gas, Dog River, SK, S0G 4H0, CANADA"
%AddressBook% add %AddressFile% "Michelle Obama" "1600 Pennsylvania Ave, Washington, DC, 20500, USA"

%AddressBook% print %AddressFile%
@PAUSE

@GOTO :EOF




@REM ==============================================================================
:Update
@REM ==============================================================================
@SET AddressFile=%1

@Rem Check it updates street
%AddressBook% update %AddressFile% "John Doe" street "3645 Marine St."
%AddressBook% print %AddressFile%
@PAUSE
@Rem ... and name
%AddressBook% update %AddressFile% "John Doe" name "Bob Ball"
%AddressBook% print %AddressFile%
@PAUSE

@Rem Check it handles a missing address file
@DEL DoesNotExist.txt
%AddressBook% update DoesNotExist.txt "John Doe" street "3645 Marine St."
@PAUSE

@GOTO :EOF




@REM ==============================================================================
:Remove
@REM ==============================================================================
@SET AddressFile=%1

@Rem Check it updates of street
%AddressBook% remove %AddressFile% "Joe Bloggs"
%AddressBook% print %AddressFile%
@PAUSE

@Rem Check it handles a missing address file
@DEL DoesNotExist.txt
%AddressBook% remove DoesNotExist.txt "Joe Bloggs"
@PAUSE

@GOTO :EOF



@REM ==============================================================================
:PrintFile
@REM ==============================================================================
@SET AddressFile=%1
@SET OutputFile=%2

@Rem Check it prints
@DEL %OutputFile%
%AddressBook% print %AddressFile% %OutputFile%
TYPE %OutputFile%
@PAUSE

@Rem Check it overwrites, and does not append, the print file
%AddressBook% print %AddressFile% %OutputFile%
TYPE %OutputFile%

@Rem Check it handles a missing address file
@DEL DoesNotExist.txt
%AddressBook% print DoesNotExist.txt %OutputFile%
@PAUSE

@GOTO :EOF