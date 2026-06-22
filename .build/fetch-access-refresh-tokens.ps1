# Configuration Parameters (Replace with your own)
$clientId = "YOUR_CLIENT_ID"
$clientSecret = "YOUR_CLIENT_SECRET"
$redirectUri = "https://localhost" # Use http://localhost:8080 or matching app config

# 1. Define Authorization Endpoints
# For Yahoo (Uncomment below if doing Yahoo):
$authUrl = "https://api.login.yahoo.com/oauth2/request_auth"
$tokenUrl = "https://yahoo.com"
$scope = "mail-w"

# For Google:
# $authUrl = "https://google.com"
# $tokenUrl = "https://googleapis.com"
# $scope = "https://mail.google.com/"

# 2. Build and Launch the Browser Request Url
$browserUrl = "${authUrl}?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=code&scope=${scope}&access_type=offline&prompt=consent"
Start-Process $browserUrl

# 3. Paste the 'code=' value from the browser's redirected URL below
$authCode = Read-Host "Paste the code query parameter from the browser redirect URL here"