using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InduMovel.Context;
using InduMovel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using InduMovel.Migrations;
using ReflectionIT.Mvc.Paging;

namespace InduMovel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminMovelController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ConfiguraImagem _confImg;
        private readonly IWebHostEnvironment _hostingEnvireoment;


        public AdminMovelController(AppDbContext context, IWebHostEnvironment hostEnvironment, IOptions<ConfiguraImagem> confImg)
        {
            _context = context;
            _confImg = confImg.Value;
            _hostingEnvireoment = hostEnvironment;
        }


        // GET: Admin/AdminMovel
        public async Task<IActionResult> Index(string filtro, int pageindex = 1, string sort = "Nome")
        { 
            var moveislist = _context.Moveis.AsNoTracking().AsQueryable();

            if(filtro != null){
                moveislist = moveislist.Where(p => p.Nome.Contains(filtro));
            }
            var model = await PagingList.CreateAsync(moveislist, 5, pageindex, sort, "Name");
            model.RouteValue = new RouteValueDictionary{{"filtro", filtro }};

            return View(model);
        }

        // GET: Admin/AdminMovel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Moveis == null)
            {
                return NotFound();
            }

            var movel = await _context.Moveis
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.MovelId == id);
            if (movel == null)
            {
                return NotFound();
            }

            return View(movel);
        }

        // GET: Admin/AdminMovel/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: Admin/AdminMovel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovelId,Nome,Descricao,Cor,ImagemUrl,ImagemPequena,Valor,EmProducao,Ativo,CategoriaId")] Movel movel, IFormFile Imagem, IFormFile Imagemcurta)
        {
            
            if (Imagem != null)
            {
                
                string imagemr = await SalvarArquivo(Imagem);
                movel.ImagemUrl = imagemr;
                
            }
            if (Imagemcurta != null)
            {
                string imagemcr = await SalvarArquivo(Imagemcurta);
                movel.ImagemPequena = imagemcr;
            }

            if (ModelState.IsValid)
            {
                _context.Add(movel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome", movel.CategoriaId);
            return View(movel);
        }

        // GET: Admin/AdminMovel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Moveis == null)
            {
                return NotFound();
            }

            var movel = await _context.Moveis.FindAsync(id);
            if (movel == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome", movel.CategoriaId);
            return View(movel);
        }

        // POST: Admin/AdminMovel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovelId,Nome,Descricao,Cor,ImagemUrl,ImagemPequena,Valor,EmProducao,Ativo,CategoriaId")] Movel movel, IFormFile Imagem, IFormFile Imagemcurta)
        {
            if (id != movel.MovelId)
            {
                return NotFound();
            }
            if (Imagem != null)
            {
                Deletefile(movel.ImagemUrl);
                string imagemr = await SalvarArquivo(Imagem);
                movel.ImagemUrl = imagemr;
            }
            if (Imagemcurta != null)
            {
                Deletefile(movel.ImagemPequena);
                string imagemcr = await SalvarArquivo(Imagemcurta);
                movel.ImagemPequena = imagemcr;
            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovelExists(movel.MovelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nome", movel.CategoriaId);
            return View(movel);
        }

        // GET: Admin/AdminMovel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Moveis == null)
            {
                return NotFound();
            }

            var movel = await _context.Moveis
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.MovelId == id);
            if (movel == null)
            {
                return NotFound();
            }

            return View(movel);
        }

        // POST: Admin/AdminMovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Moveis == null)
            {
                return Problem("Entity set 'AppDbContext.Moveis'  is null.");
            }
            var movel = await _context.Moveis.FindAsync(id);
            if (movel != null)
            {
                Deletefile(movel.ImagemPequena);
                Deletefile(movel.ImagemUrl);
                _context.Moveis.Remove(movel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovelExists(int id)
        {
            return _context.Moveis.Any(e => e.MovelId == id);
        }

        public async Task<string> SalvarArquivo(IFormFile Imagem)
        {
            var filePath = Path.Combine(_hostingEnvireoment.WebRootPath, _confImg.NomePastaImagemItem);

            if (Imagem.FileName.Contains(".jpg") || Imagem.FileName.Contains(".gif")
            || Imagem.FileName.Contains(".svg") || Imagem.FileName.Contains(".png"))
            {
                string novoNome =$"{Guid.NewGuid()}.{Path.GetExtension(Imagem.FileName)}";

                var fileNameWithPath = string.Concat(filePath, "\\", novoNome);
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    await Imagem.CopyToAsync(stream);
                }
                return "~/" + _confImg.NomePastaImagemItem + "/" + novoNome;
            }
            return null;
        }
        public void Deletefile(string fname)
        {
            if (fname != null)
            {

                int pi = fname.LastIndexOf("/") + 1;
                int pf = fname.Length - pi;
                string nomearquivo = fname.Substring(pi, pf);
                try
                {
                    string _imagemDeleta = Path.Combine(_hostingEnvireoment.WebRootPath,
                    _confImg.NomePastaImagemItem + "\\", nomearquivo);
                    if ((System.IO.File.Exists(_imagemDeleta)))
                    {
                        System.IO.File.Delete(_imagemDeleta);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
