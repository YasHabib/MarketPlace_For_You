# MarketPlace_For_You<br><br>

To start the project, clone the project into your drive. Set Docker Compose as your startup project. <br>
Start debugging<br>
Swagger UI should open automatically upon starting Docker-Compose<br>
If not, to open swagger ui, launch http://localhost:35010/swagger/index.html into your web browser. Make sure docker is installed and marketplaceforyou.api and marketplaceforyou.db is running.<br><br>

**Getting access token to test the endpoints**<br>
Open a cognito window on your browser.
Navigate to: https://marketforyou-upgrade.us.auth0.com/authorize?audience=http://marketforyou.com&scope=profile%20email%20openid&response_type=token&client_id=PcvmnhwQPU55UGLKwEceHsS41UR34EH5&redirect_uri=http://localhost:35010<br>
To test admin endpoints (marked with AP in front of the controller): use the following credential:<br>
Email: yasin.habib1992+admin@gmail.com<br>
Password: Test@123<br>
Click Continue and Accept and copy the access token (this will be either on the address bar or in the browser window<br>
Click on Authorize on Swagger UI and paste the token **without** putting Bearer in-front. <br><br>

All test data are stored in local drive, so upon execuring an API, you will get a 200 response with an empty response.<br>


**important**<br><br>

File upload API is current not working as I have shutdown my AWS account associated with this project and removed all related services to avoid server cost.<br>
Email API is current not working as SendGrid API key is associated with AWS Parameter Store and the AWS account is currently closed.

