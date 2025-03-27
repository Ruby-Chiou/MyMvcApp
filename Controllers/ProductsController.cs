using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;  // 確保這裡的命名空間符合你的專案

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // 列表 (Read)
    public async Task<IActionResult> Index()
    {
        return View(await _context.Products.ToListAsync());
    }

    // 詳細資料 (Read)
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    // 新增 (Create) - 顯示表單
    public IActionResult Create()
    {
        return View();
    }

    // 新增 (Create) - 提交表單
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // 編輯 (Update) - 顯示表單
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    // 編輯 (Update) - 提交表單
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // 刪除 (Delete) - 顯示確認頁
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    // 刪除 (Delete) - 執行刪除
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
