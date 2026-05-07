Write-Output "Testing /health..."
$health = Invoke-WebRequest -Uri http://localhost:5146/health -UseBasicParsing
Write-Output $health.Content

Write-Output ""
Write-Output "Testing /events..."
$response = Invoke-WebRequest -Uri http://localhost:5146/events `
  -Method POST `
  -UseBasicParsing `
  -ContentType "application/json" `
  -Body '{"userId":"123","action":"watched_ad","adId":"456","timestamp":"2026-05-07"}'

Write-Output $response.Content
