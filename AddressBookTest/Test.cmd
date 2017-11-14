@Rem manual system tests
@SET AddressBook=..\AddressBook\bin\Debug\AddressBook.exe

%AddressBook% find zip "0G"

%AddressBook% sort city

CALL :Add
CALL :Update
CALL :Remove
CALL :PrintFile foo.csv
CALL :PrintFile bar.xml

GOTO :EOF


@REM ==============================================================================
:Add
@REM ==============================================================================
%AddressBook% add "Michelle Obama, 1600 Pennsylvania Ave, Washington, DC, 20500, USA"

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