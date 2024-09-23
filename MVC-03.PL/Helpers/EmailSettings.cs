using MVC_03.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_03.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail (Email email)
		{
			var client = new SmtpClient("stmp.gmail.com", 587);
			client.EnableSsl = false;
			client.Credentials = new NetworkCredential("mohabmbelkan@gmail.com", "cwequvdtnmquaezj");
			client.Send("mohabmbelkan@gmail.com",email.Receipints,email.Subject,email.Body);
        }
	}
}
