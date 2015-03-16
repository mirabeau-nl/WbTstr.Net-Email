# WbTstr.Net-Email
Simple e-mail client to test e-mail messages with WbTstr.Net or FluentAutomation.


##Sample Usage

	//First create the email config object
	var emailConfig = new EmailConfig
            {
                UserName = "test@gmail.com",
                Password = "topSecret",
                HostName = "pop.gmail.com",
                Port = 995,
                UseSsl = true
            };

	//Create an instance of the Email tool            

	//Create an instance of the Email tool
	var emailTool = new EmailTool(emailConfig);

	//Get the unique e-mail address and use it in your test code
	RegisterTest(emailTool.UniqueEmailAdress);

	//Download the e-mails
	var email = emailTool.GetMail();

	//Navigate to the e-mail html in your browser and perform validations
	I.Open(email.HtmlEmailPath);
	I.Assert.Exists("H1");
	
	//You can also see the attachments
	var attachments =email.Attachment;


