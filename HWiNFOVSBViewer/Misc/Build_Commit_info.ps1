# Get the current date (UTC) at build time and save it in a file.
# This file (BuildDate.txt) is added as a resource which can be
# found at Properties.Resources.BuildDate.

$buildDate = (get-date).ToUniversalTime().ToString("u")
$dateFile = Join-Path -Path $PSScriptRoot -ChildPath BuildDate.txt
Set-Content -Path $dateFile -Value $buildDate

# Get current commit id and save it to a file.

$commitFile = Join-Path -Path $PSScriptRoot -ChildPath CurrentCommit.txt
$gitOutput = git rev-parse --short HEAD
Set-Content -Path $commitFile -Value $gitOutput
