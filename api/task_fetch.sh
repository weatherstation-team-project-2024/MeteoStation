#!/bin/bash
# cd MeteoStation/api

PID=$(lsof -t -i :5037)

if [ -n "$PID" ]; then
    kill $PID
    echo "Process with PID $PID has been killed."
else
    echo "No process found listening on port 5037."
fi

dotnet watch run
