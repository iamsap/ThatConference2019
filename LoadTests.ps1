Add-Type -AssemblyName System.Web

$rpsThreshold = 2

$token = [Convert]::ToBase64String([System.Text.Encoding]::ASCII.GetBytes("username:gtn54zvllxsaajfa4h2irwldti4tinmxucbctop3zzvp27hzq4cq"))

$headers =  @{Authorization=("Basic $token")}
$headers.Add("Accept", "application/json")
$headers.Add("Content-Type", "application/json")

$response = Invoke-RestMethod -Method GET `
                                  -Headers $headers `
                                  -Uri "https://iamsap.vsclt.visualstudio.com/_apis/clt/testruns?status=completed&runType=*&api-version=5.0" 

$testRunRPSCounter = $testRunCounters.value | Where-Object -Property "counterName" -EQ "Requests/Sec"
$testRun = $response | Sort-Object -Property RunNumber -Descending | Select-Object -First 1


$testRunId = $testRun.id

$testRunCounters = Invoke-RestMethod -Method GET `
                                         -Headers $headers `
                                         -Uri "https://iamsap.vsclt.visualstudio.com/_apis/clt/testruns/$testRunId/CounterInstances?groupNames=Throughput&api-version=5.0"

$testRunRPSCounter = $testRunCounters.value | Where-Object -Property "counterName" -EQ "Requests/Sec"


$rpsCounterId = $testRunRPSCounter.counterInstanceId
$rpsResult = Invoke-RestMethod -Method POST `
                               -Headers $headers `
                               -Uri "https://iamsap.vsclt.visualstudio.com/_apis/clt/testruns/$testRunId/countersamples?api-version=5.0" `
                               -Body (@{ count = 1
                                   value=@(@{counterInstanceId=$rpsCounterId})
                                    } | ConvertTo-Json)

$rpsResultValues = $rpsResult.values[0].values

$rpsMeasure = $rpsResultValues | Where-Object -Property "computedValue" -NE 0 | Measure-Object -Property "computedValue" -Average

if($rpsMeasure){ $averageRPS = $rpsMeasure.Average } else { $averageRPS = 0 }

if($averageRPS -le $rpsThreshold )
{
    Write-Host "averageRPS is less or equal than $rpsThreshold"
    exit 1
}else {
    Write-Host "averageRPS is greater than $rpsThreshold"
    exit 0
}
