using System.Text;
using System.Xml;
using InduMovel.Areas.Admin.Services;
using InduMovel.Context;
using InduMovel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InduMovel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminXmlController : Controller
    {
        private readonly RelatorioVendasServices _relatorio;
        private readonly IWebHostEnvironment _hostingEnvireoment;
        private readonly AppDbContext _context;
        public AdminXmlController(RelatorioVendasServices relatorio,
        IWebHostEnvironment hostingEnvireoment, AppDbContext context)
        {
            _relatorio = relatorio;
            _hostingEnvireoment = hostingEnvireoment;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Lista(DateTime? di, DateTime?
        df)
        {
            if (!di.HasValue)
            {
                di = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (df.HasValue)
            {
                df = DateTime.Now;
            }
            ViewData["di"] = di.Value.ToString("yyyy-mm-dd");
            ViewData["df"] = df.Value.ToString("yyyy-mm-dd");
            var result = await _relatorio.BuscaPorData(di, df);
            return View(result);
        }
        public async Task<IActionResult> GerarXml(DateTime? di, DateTime?
        df)
        {
            if (!di.HasValue)
            {
                di = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (df.HasValue)
            {
                df = DateTime.Now;
            }
            var result = await _relatorio.BuscaPorData(di, df);
            string Caminho = Path.Combine(_hostingEnvireoment.WebRootPath, "XML/Pedidos.xml");

            try
            {
                XmlTextWriter xml = new XmlTextWriter(Caminho, Encoding.UTF8);

                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();
                xml.WriteStartElement("Pedidos");
                foreach (Pedido p in result)
                {
                    xml.WriteStartElement("Pedido");
                    xml.WriteElementString("PedidoId",

                    p.PedidoId.ToString());

                    xml.WriteElementString("ValorTotal",

                    p.PedidoTotal.ToString());

                    xml.WriteStartElement("Moveis");
                    foreach (PedidoMovel pm in p.PedidoMoveis)
                    {
                        xml.WriteStartElement("Movel");
                        xml.WriteElementString("MovelId",

                        pm.MovelId.ToString());

                        xml.WriteElementString("Quantidade",

                        pm.Quantidade.ToString());
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement();
                    xml.WriteEndElement();
                    
                }
                
                xml.WriteEndElement();
                xml.WriteEndDocument();
                xml.Flush();
                xml.Close();
            }
            catch (Exception)
            {
            }
            ViewData["xml"] = "/XML/Pedidos.xml";
            return View();
        }
        public async Task<IActionResult> GerarXmlId(int pedidoId)
        {
            if (pedidoId == 0)
            {
                return NotFound();
            }
            var result = await _context.Pedidos.Include(i =>
            i.PedidoMoveis).ThenInclude(m => m.Movel).FirstOrDefaultAsync(p =>
            p.PedidoId == pedidoId);
            string Caminho =

            Path.Combine(_hostingEnvireoment.WebRootPath, "XML/Pedido.xml");

            try
            {
                XmlTextWriter xml = new XmlTextWriter(Caminho,

                Encoding.UTF8);

                xml.Formatting = Formatting.Indented;
                xml.WriteStartDocument();
                xml.WriteStartElement("Pedidos");
                xml.WriteStartElement("Pedido");
                xml.WriteElementString("PedidoId",
                result.PedidoId.ToString()); 
                xml.WriteElementString("ValorTotal",

                result.PedidoTotal.ToString());

                xml.WriteStartElement("Moveis");
                foreach (PedidoMovel pm in result.PedidoMoveis)
                {
                    xml.WriteStartElement("Movel");
                    xml.WriteElementString("MovelId",

                    pm.MovelId.ToString());

                    xml.WriteElementString("Quantidade",

                    pm.Quantidade.ToString());
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.WriteEndElement();
                xml.WriteEndElement();
                xml.WriteEndDocument();
                xml.Flush();
                xml.Close();
            }
            catch (Exception ex)
            {
                ViewData["erro"] = ex.Message;
            }
            ViewData["xml"] = "/XML/Pedido.xml";
            return View("~/Areas/Admin/Views/AdminXml/GerarXml.cshtml");
        }
    }
}
