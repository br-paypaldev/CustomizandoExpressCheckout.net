using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal;
using PayPal.Enum;
using PayPal.ExpressCheckout;

namespace CustomizandoexpressCheckout.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SetExpressCheckoutOperation SetExpressCheckout = PayPalApiFactory.instance.ExpressCheckout(
            "usuario.da.api",
            "senha",
            "assinatura"
            ).SetExpressCheckout(
            "http://dominio.com/url/de/sucesso",
            "http://dominio.com/url/de/cancelamento"
            );

            SetExpressCheckout.LocaleCode = LocaleCode.BRAZILIAN_PORTUGUESE;
            SetExpressCheckout.CurrencyCode = CurrencyCode.BRAZILIAN_REAL;
            SetExpressCheckout.HeaderImage = "https://cms.paypal.com/cms_content/US/en_US/images/developer/PP_X_Final_logo_vertical_rgb.gif";
            SetExpressCheckout.BrandName = "Nome da minha loja";
            SetExpressCheckout.SurveyEnable = true;
            SetExpressCheckout.SurveyQuestion = "Onde ficou sabendo de nossa loja?";
            SetExpressCheckout.SurveyChoice = new string[]{
"Um amigo me contou",
"Mecanismo de pesquisa",
"Anúncio em website",
"Outros"
};

            SetExpressCheckout.PaymentRequest(0).addItem("Produto de Teste 1", 1, 10, "Descrição do produto 1");
            SetExpressCheckout.PaymentRequest(0).addItem("Produto de Teste 2", 2, 11, "Descrição do produto 2");
            SetExpressCheckout.PaymentRequest(0).addItem("Produto de Teste 3", 3, 12, "Descrição do produto 3");
            SetExpressCheckout.PaymentRequest(0).TaxAmount = 3; //imposto
            SetExpressCheckout.PaymentRequest(0).ShippingAmount = 10; //custo do frete
            SetExpressCheckout.PaymentRequest(0).HandlingAmount = 3; //custo de manuseio
            SetExpressCheckout.PaymentRequest(0).ShippingDiscountAmount = -2; //desconto de frete

            SetExpressCheckout.sandbox().execute();

            return Redirect(SetExpressCheckout.RedirectUrl);
        }
    }
}
