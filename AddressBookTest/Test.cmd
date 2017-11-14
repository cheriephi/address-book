@Rem manual system tests
@SET AddressBook=..\AddressBook\bin\Debug\AddressBook.exe

CALL :Add
CALL :Update
CALL :Remove
CALL :PrintFile foo.csv
CALL :PrintFile bar.xml

GOTO :EOF


@REM ==============================================================================
:Add
@REM ==============================================================================
%AddressBook% add "Joe Bloggs" "1 New St., Birmingham, England, B01 3TN, UK"
%AddressBook% add "John Doe" "16 S 31st St., Boulder, CO, 80304, USA"
%AddressBook% add "Brent Leroy" "Corner Gas, Dog River, SK, S0G 4H0, CANADA"
%AddressBook% add "Michelle Obama" "1600 Pennsylvania Ave, Washington, DC, 20500, USA"

%AddressBook% print
@PAUSE

@GOTO :EOF




@REM ==============================================================================
:Update
@REM ==============================================================================

@Rem Check it updates street
%AddressBook% update "John Doe" street "3645 Marine St."
%AddressBook% print
@PAUSE
@Rem ... and name
%AddressBook% update "John Doe" name "Bob Ball"
%AddressBook% print
@PAUSE

@GOTO :EOF




@REM ==============================================================================
:Remove
@REM ==============================================================================

%AddressBook% remove "Joe Bloggs"
%AddressBook% print
@PAUSE

@GOTO :EOF



@REM ==============================================================================
:PrintFile
@REM ==============================================================================
@SET OutputFile=%1

@Rem Check it prints
@DEL %OutputFile%
%AddressBook% print %OutputFile%
TYPE %OutputFile%
@PAUSE

@Rem Check it overwrites, and does not append, the print file
%AddressBook% print %OutputFile%
TYPE %OutputFile%

@GOTO :EOF