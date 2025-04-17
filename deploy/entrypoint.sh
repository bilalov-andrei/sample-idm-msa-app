#!/bin/bash

set -e
run_cmd="dotnet IDM.EmployeeService.API.dll --no-build -v d"

dotnet IDM.EmployeeService.Migrator.dll --no-build -v d -- --dryrun

dotnet IDM.EmployeeService.Migrator.dll --no-build -v d

>&2 echo "Employee DB Migrations complete, starting app."
>&2 echo "Run EmployeeService: $run_cmd"
exec $run_cmd