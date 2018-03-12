@ECHO OFF
SETLOCAL

CD samples/EPiServer.ContentApi.MusicFestival.Frontend/

REM Ensure all the node modules are installed, then run the setup task.
CALL npm install
START npm run dev

EXIT /B %ERRORLEVEL%
