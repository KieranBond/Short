set -euxo pipefail

dotnet test ./Short.Tests/ --collect "Code Coverage"