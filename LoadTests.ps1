Write-Host "Load test failed"
exit 1

# Add-Type -AssemblyName System.Web

# $token = [Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes(":$(PersonalAccessToken)"))

# $headers =  @{Authorization=("Basic {0}" -f $token)}
# $headers.Add("Accept", "application/json")
# $headers.Add("Content-Type", "application/json")

# $testRunNameParameter =  [System.Web.HttpUtility]::UrlEncode("$(TestRunName)")

# #at the time of this script, there was no server side ordering of the test runs so need to query all to get the two most recent.
# $response = Invoke-RestMethod -Method GET `
#                                   -Headers $headers `
#                                   -Uri "https://trekbikes.vsclt.visualstudio.com/_apis/clt/testruns?status=completed&name=$testRunNameParameter&runType=*&fromDate=2018-01-01&api-version=5.0-preview.1" 

# $twoMostRecentRuns = $response | Sort-Object -Property RunNumber -Descending | Select-Object -First 2

# #if we don't have two runs just exit
# if ($twoMostRecentRuns.Count -lt 2) {
#     Write-Host "Not enough test runs to run comparison"
#     exit 0
# }

# #query Throughput counter results for each run
# $testRunAverageRPS = @()
# foreach ($testRun in $twoMostRecentRuns){
#     $testRunId = $testRun.id
#     $testRunCounters = Invoke-RestMethod -Method GET `
#                                           -Headers $headers `
#                                           -Uri "https://trekbikes.vsclt.visualstudio.com/_apis/clt/testruns/$testRunId/CounterInstances?groupNames=Throughput&api-version=5.0-preview.1"
#     #get requestPersecond counter id
#     $testRunRPSCounter = $testRunCounters.value | Where-Object -Property "counterName" -EQ "Requests/Sec"

#     #query results for the Requests/Second counter

#     $rpsCounterId = $testRunRPSCounter.counterInstanceId
#     $rpsResult = Invoke-RestMethod -Method POST `
#                                    -Headers $headers `
#                                    -Uri "https://trekbikes.vsclt.visualstudio.com/_apis/clt/testruns/$testRunId/countersamples?api-version=5.0-preview.1" `
#                                    -Body (@{ count = 1
#                                             value=@(@{counterInstanceId=$rpsCounterId})
#                                           } | ConvertTo-Json)

#     $rpsResultValues = $rpsResult.values[0].values
    
#     #get rps values not equal to zero and average 
#     $rpsMeasure = $rpsResultValues | Where-Object -Property "computedValue" -NE 0 | Measure-Object -Property "computedValue" -Average

#     if($rpsMeasure){ $averageRPS = $rpsMeasure.Average } else { $averageRPS = 0 }

#     Write-Host "Average RPS is: $averageRPS for test run:"$testRun.runNumber  
#     $testRunAverageRPS+=$averageRPS
# }
# #if most recent test run had an average rps < threshold * second most recent run then return false
# if($testRunAverageRPS[0] -gt $testRunAverageRPS[1]){
#     # if latest test run did more RPS were gtg
#     Write-Host "Latest test run out performed previous"
#     exit 0
# }

# if(($testRunAverageRPS[1]-$testRunAverageRPS[0]) -gt ($testRunAverageRPS[1] * $performanceThreshold)){
#     Write-Host "Perfomance has dropped more than performance threshold"
#     exit 1
# }
# Write-Host "Performance of latest run is within the threshold of the previous run"