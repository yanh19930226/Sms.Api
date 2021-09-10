@echo off
cd %~dp0
dotnet Sms.Api.Bus.dll action:uninstall
pause