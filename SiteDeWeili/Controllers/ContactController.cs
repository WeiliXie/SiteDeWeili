using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteDeWeili.Filters;
using SendingEmail.Models;


namespace SiteDeWeili.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View();
        }

        [CaptchaValidator]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SendMail(string nom, string email, string message, bool captchaValid)
        {
            if (!captchaValid)
            {
                ViewBag.nom = nom;
                ViewBag.email = email;
                ViewBag.message = message;
                ViewBag.message_erreur = "Les mots de vérification n'ont pas correctement saisis. Reéssayez-vous .";
                return View("Index");
            }
            else
            {

                EMail OEmail = new EMail();
                try
                {
                    OEmail.SendMail(email, "Contact : un message de " + nom, message, nom + " captchavalisd=" + captchaValid);
                    ViewBag.message = "Merci ! Votre message a bien été envoyée.";

                    return View();
                }
                catch (Exception e)
                {
                    ViewBag.message = e.Message;
                    return View();
                }
            }

        }
    }
}
