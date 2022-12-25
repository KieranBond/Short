#!/bin/bash
set -euxo pipefail

dotnet test ./Backend/Short.Tests/ --collect "Xplat Code Coverage"