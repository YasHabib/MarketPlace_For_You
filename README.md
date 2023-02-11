# MarketPlace_For_You<br><br>

To start the project, clone the project into your drive. Set Docker Compose as your startup project. <br>
Start debugging<br>
Swagger UI should open automatically upon starting docker-compose<br>
If not, to open swagger ui, launch http://localhost:35010/swagger/index.html into your web browser. **Swagger UI is not set to automatically upon upon debugging**. Make sure docker is installed and marketplaceforyou.api and marketplaceforyou.db is running.<br><br>

**Getting access token to test the endpoints**<br>
Open a cognito window on your browser.
Navigate to: https://marketforyou-upgrade.us.auth0.com/authorize?audience=http://marketforyou.com&scope=profile%20email%20openid&response_type=token&client_id=PcvmnhwQPU55UGLKwEceHsS41UR34EH5&redirect_uri=http://localhost:35010<br>
To test admin endpoints (marked with AP in front of the controller): use the following credential:<br>
Email: yasin.habib1992+admin@gmail.com<br>
Password: Test@123<br>
Click Accept and copy the access token<br>
Click on Authorize on Swagger UI and paste the token **without** putting Bearer in-front. <br><br>

To test other endpoints, you can use the same credential or a different one: <br>
Email: yasin.habib1992+user1@gmail.com<br>
Password: Test@123<br>


**important**<br><br>

File upload API is current not working as I have shutdown my AWS account associated with this project and removed all related services to avoid server cost.<br>
Email API is current not working as SendGrid API key is associated with AWS Parameter Store and the AWS account is currently closed.

